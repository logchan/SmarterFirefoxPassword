Smarter Firefox Password
---

Enter Firefox master password saved in smart card.

Created by logchan, who is tired of typing, and happens to have dozens of spare tags.

## What's this?

Every second, the program (SFP) checks if Firefox is prompting for your master password. If so, it tries to read it from a smart card and enters it for you.

SFP starts minimized in tray. Double click the icon open the window. You can write your master password to a smart card.

![Screenshot](https://raw.githubusercontent.com/logchan/SmarterFirefoxPassword/master/assets/screenshot.png)

The FG title and FG file text are for debug purpose.

## Requirements

### Hardware

- A SC/PC compatible card reader.
- A smart card.

(Only tested with certain reader and some tags. If it does not work with your card, check command in [SmartCardClr.cpp](https://github.com/logchan/SmarterFirefoxPassword/blob/master/SmartCardClr/SmartCardClr.cpp))

### Software

- Windows 10
- .Net framework 4.7.1
- Visual Studio 2019

## TODOs

- Support different (configurable) Firefox install locations?
- Support non-English Firefox prompts?
- Use an event-based approach rather than querying active window every second?
- Add more logs?

## License

[DBAD license](https://dbad-license.org/)