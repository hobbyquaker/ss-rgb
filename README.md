# ss-rgb

> Tiny command line utility for Windows to control Steelseries keyboard and mouse LEDs

Totally opinonated and lazy piece of software: code generated with Copilot, hardcoded product/vendor ids for Steelseries Apex 3 TKL and Steelseries Aerox 3 Wired.

I was utterly annoyed by the availabe software to control RGB equipment. Most of them are bloated (e.g. SignalRGB, Artemis, Steelseries GG) or just not working reliably with my hardware
(e.g. OpenRGB). I wanted something simple, fast and reliable that does not need a fat daemon in background and I have no need for game integration, ambilight or fancy animated effects, so I created this.

# Prerequisites

* [.NET 8 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) is required to run this tool.


# Available commands

* `ss-rgb.exe <target> color <rgb1> [rgb2 ... rgb8]`  
  Set up to 8 colors (hex value or CSS color name) for the selected target(s).  
  Example: `ss-rgb.exe mouse color FF0000 00FF00 blue orange`

* `ss-rgb.exe <target> all <rgb>`  
  Set all LEDs to the same color (hex value or CSS color name).  
  Example: `ss-rgb.exe both all red`

* `ss-rgb.exe mouse reaction off`  
  Disable mouse reactive lighting.

* `ss-rgb.exe mouse reaction <rgb>`  
  Enable mouse reactive lighting with the given color (hex value or CSS color name).  
  Example: `ss-rgb.exe mouse reaction lightblue`

**Targets:**  
- `mouse`  
- `keyboard`  
- `both`

**Color values:**  
- 6-digit hex value (e.g. `FF0000` for red)  
- CSS/HTML color names (e.g. `red`, `orange`, `lightblue`, `darkgreen`, ...)

# License

MIT (c) 2025 Sebastian "hobbyquaker" Raff
