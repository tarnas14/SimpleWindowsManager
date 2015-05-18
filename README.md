# SimpleWindowsManager

Planned functionality:
  - [x] switching windows using only keyboard (kinda sorta working)
  - [ ] moving active window around a pre-configured grid

TODO:
  - [ ] load binding from file (and check if hotkey is registered)
  - [x] add notifyIcon with contextmenu (bring to front and exit)
  - [ ] start without window - with only notify icon
  - [ ] remove WindowSwitcher from alt-tab list

FOUND ISSUES:
  - [x] slow response for the global hotkey (maybe try catching it closer to the OS API)
  - [x] the combobox text gets cleared randomly as you type
