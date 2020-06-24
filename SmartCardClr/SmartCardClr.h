#pragma once
#include <winscard.h>

using namespace System;

namespace SmartCardClr {
	struct NativeData {
		bool m_valid;
		SCARDCONTEXT m_ctx;

		NativeData();
		~NativeData();
	};

	public ref class SmartCardInfo sealed {
	public:
		SCARDHANDLE card;
		DWORD activeProtocol;
	};
	
	public ref class SmartCardContext sealed : IDisposable {
	public:
		SmartCardContext();
		~SmartCardContext();

		/// <summary>
		/// List available card reader names.
		/// </summary>
		array<String^>^ GetReaders();

		/// <summary>
		/// Open a reader for subsequent commands and IOs.
		/// Only one reader may be opened at a time.
		/// </summary>
		void OpenReader(String^ reader);

		/// <summary>
		/// Close the opened reader.
		/// </summary>
		void CloseReader();

		/// <summary>
		/// Send a command to the opened reader. <seealso>ISO 7816-4</seealso>
		/// </summary>
		array<Byte>^ SendCommand(array<Byte>^ command);

		/// <summary>
		/// Read all data from card via opened reader.
		/// </summary>
		/// <returns></returns>
		array<Byte>^ ReadData();

		/// <summary>
		/// Write data to card via opened reader. Remaining bytes are set to zero.
		/// </summary>
		void WriteData(array<Byte>^ data);
		
	private:
		NativeData* m_native;
		SmartCardInfo^ m_info = nullptr;
	};
}
