#include "pch.h"

#include "SmartCardClr.h"
#include <vector>
#include <vcclr.h>

namespace SmartCardClr {
SmartCardContext::SmartCardContext() {
	m_native = new NativeData;
}

SmartCardContext::~SmartCardContext() {
	if (m_info != nullptr) {
		SCardDisconnect(m_info->card, SCARD_LEAVE_CARD);
	}
	
	m_info = nullptr;
	delete m_native;
}

array<String^>^ SmartCardContext::GetReaders() {
	auto cch = SCARD_AUTOALLOCATE;
	LPTSTR pmszReaders = NULL;
	std::vector<LPTSTR> readers;

	switch (SCardListReaders(m_native->m_ctx, NULL, (LPTSTR)&pmszReaders, &cch)) {
	case SCARD_E_NO_READERS_AVAILABLE:
		break;
	case SCARD_S_SUCCESS: {
		auto pReader = pmszReaders;
		while ('\0' != *pReader) {
			readers.push_back(pReader);
			pReader += lstrlen(pReader) + 1;
		}
		break;
	}
	default:
		throw gcnew Exception("SCardListReaders failed");
	}

	auto results = gcnew array<String^>(readers.size());
	for (auto i = 0u; i < readers.size(); ++i) {
		results[i] = gcnew String(readers[i]);
	}

	SCardFreeMemory(m_native->m_ctx, pmszReaders);
	return results;
}

void SmartCardContext::OpenReader(String ^ reader) {
	if (m_info != nullptr) {
		throw gcnew Exception("A card is already opened");
	}
	
	pin_ptr<const wchar_t> reader_w = PtrToStringChars(reader);
	SCARDHANDLE card;
	DWORD activeProtocol;
	auto rtn = SCardConnect(m_native->m_ctx, reader_w, SCARD_SHARE_SHARED,
						SCARD_PROTOCOL_T0 | SCARD_PROTOCOL_T1,
						&card, &activeProtocol);
	if (rtn != SCARD_S_SUCCESS) {
		throw gcnew Exception("SCardConnect failed");
	}

	m_info = gcnew SmartCardInfo;
	m_info->card = card;
	m_info->activeProtocol = activeProtocol;
}

void SmartCardContext::CloseReader() {
	if (m_info == nullptr) {
		throw gcnew Exception("No reader is opened");
	}
	
	SCardDisconnect(m_info->card, SCARD_LEAVE_CARD);
	m_info = nullptr;
}

array<Byte>^ SendCommandInternal(SmartCardInfo^ info, BYTE const * cmd, DWORD length) {
	auto activeProtocol = info->activeProtocol;
	auto card = info->card;
	
	SCARD_IO_REQUEST sendPci;
	SCARD_IO_REQUEST recvPci;
	switch (activeProtocol) {
	case SCARD_PROTOCOL_T0:
		sendPci = *SCARD_PCI_T0;
		recvPci = *SCARD_PCI_T0;
		break;
	case SCARD_PROTOCOL_T1:
		sendPci = *SCARD_PCI_T1;
		recvPci = *SCARD_PCI_T1;
		break;
	default:
		throw gcnew Exception("Unexpected active protocol");
	}

	BYTE buffer[258];
	DWORD numRecv = sizeof(buffer);
	auto rtn = SCardTransmit(card, &sendPci, cmd, length, &recvPci, buffer, &numRecv);
	if (rtn != SCARD_S_SUCCESS) {
		throw gcnew Exception("SCardTransmit failed");
	}

	auto result = gcnew array<Byte>(numRecv);
	pin_ptr<BYTE> result_p = &result[0];
	memcpy(result_p, buffer, numRecv);
	return result;
}
	
array<Byte>^ SmartCardContext::SendCommand(array<Byte>^ command) {
	if (m_info == nullptr) {
		throw gcnew Exception("No reader is opened");
	}
	
	pin_ptr<BYTE> cmd = &command[0];
	return SendCommandInternal(m_info, cmd, command->Length);
}

/// <summary>
/// Check if response data ends with 0x90 0x00
/// </summary>
void CheckTransmitSuccess(array<BYTE>^ data) {
	if (data->Length < 2) {
		throw gcnew Exception("SCardTransmit response too short");
	}

	auto sw1 = data[data->Length - 2];
	auto sw2 = data[data->Length - 1];
	if (sw1 != 0x90 || sw2 != 0x00) {
		throw gcnew Exception("SCardTransmit response is error");
	}
}
	
array<Byte>^ SmartCardContext::ReadData() {
	if (m_info == nullptr) {
		throw gcnew Exception("No reader is opened");
	}

	// TODO other smart cards and readers may require different command
	// However I only have ACR122 reader and ntag215 cards
	BYTE readCmd[] = { 0xff, 0xb0, 0x00, 0x00, 0x10 };
	auto result = gcnew array<Byte>(12 * 4);

	// cref. ACR122U API section 5.3
	// In MIFARE ultralight, there are 16 pages with 4 bytes each; data page starts at 4.
	// It is possible to read 4 pages (16 bytes) at a time.
	const auto startPage = 4;
	for (auto page = startPage; page < 16; page += 4) {
		readCmd[3] = page;

		auto data = SendCommandInternal(m_info, readCmd, sizeof(readCmd));
		CheckTransmitSuccess(data);
		if (data->Length != 18) {
			throw gcnew Exception("SCardTransmit read data length unexpected");
		}
		
		pin_ptr<BYTE> result_p = &result[(page - startPage) * 4];
		pin_ptr<BYTE> data_p = &data[0];
		memcpy(result_p, data_p, 16);
	}

	return result;
}

void SmartCardContext::WriteData(array<Byte>^ data) {
	if (m_info == nullptr) {
		throw gcnew Exception("No reader is opened");
	}

	if (data->Length > 12 * 4) {
		throw gcnew Exception("Data is too long");
	}

	// TODO support more cards and readers if possible
	BYTE writeCmd[] = { 0xff, 0xd6, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 };

	// cref. ACR122U API section 5.4
	// It is possible to write 1 page (4 bytes) at a time
	const auto startPage = 4;
	for (auto page = startPage; page < 16; page += 1) {
		writeCmd[3] = page;
		for (int j = 0; j < 4; ++j) {
			auto idx = (page - startPage) * 4 + j;
			auto b = idx >= data->Length ? 0 : data[idx];
			writeCmd[5+j] = b;
		}

		auto resp = SendCommandInternal(m_info, writeCmd, sizeof(writeCmd));
		CheckTransmitSuccess(resp);
	}
}

NativeData::NativeData() {
	m_valid = SCardEstablishContext(SCARD_SCOPE_USER, NULL, NULL, &m_ctx);
}

NativeData::~NativeData() {
	if (m_valid) {
		SCardReleaseContext(m_ctx);
	}
}
}