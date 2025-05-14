using System;
using System.Collections.Generic;
using System.Globalization;
using HidSharp;

static class CssColorNames
{
    public static readonly Dictionary<string, int> Map = new(StringComparer.OrdinalIgnoreCase) {
        { "black", 0x000000 }, { "silver", 0xC0C0C0 }, { "gray", 0x808080 }, { "white", 0xFFFFFF },
        { "maroon", 0x800000 }, { "red", 0xFF0000 }, { "purple", 0x800080 }, { "fuchsia", 0xFF00FF },
        { "green", 0x008000 }, { "lime", 0x00FF00 }, { "olive", 0x808000 }, { "yellow", 0xFFFF00 },
        { "navy", 0x000080 }, { "blue", 0x0000FF }, { "teal", 0x008080 }, { "aqua", 0x00FFFF },
        { "orange", 0xFFA500 }, { "aliceblue", 0xF0F8FF }, { "antiquewhite", 0xFAEBD7 }, { "aquamarine", 0x7FFFD4 },
        { "azure", 0xF0FFFF }, { "beige", 0xF5F5DC }, { "bisque", 0xFFE4C4 }, { "blanchedalmond", 0xFFEBCD },
        { "blueviolet", 0x8A2BE2 }, { "brown", 0xA52A2A }, { "burlywood", 0xDEB887 }, { "cadetblue", 0x5F9EA0 },
        { "chartreuse", 0x7FFF00 }, { "chocolate", 0xD2691E }, { "coral", 0xFF7F50 }, { "cornflowerblue", 0x6495ED },
        { "cornsilk", 0xFFF8DC }, { "crimson", 0xDC143C }, { "cyan", 0x00FFFF }, { "darkblue", 0x00008B },
        { "darkcyan", 0x008B8B }, { "darkgoldenrod", 0xB8860B }, { "darkgray", 0xA9A9A9 }, { "darkgreen", 0x006400 },
        { "darkgrey", 0xA9A9A9 }, { "darkkhaki", 0xBDB76B }, { "darkmagenta", 0x8B008B }, { "darkolivegreen", 0x556B2F },
        { "darkorange", 0xFF8C00 }, { "darkorchid", 0x9932CC }, { "darkred", 0x8B0000 }, { "darksalmon", 0xE9967A },
        { "darkseagreen", 0x8FBC8F }, { "darkslateblue", 0x483D8B }, { "darkslategray", 0x2F4F4F }, { "darkslategrey", 0x2F4F4F },
        { "darkturquoise", 0x00CED1 }, { "darkviolet", 0x9400D3 }, { "deeppink", 0xFF1493 }, { "deepskyblue", 0x00BFFF },
        { "dimgray", 0x696969 }, { "dimgrey", 0x696969 }, { "dodgerblue", 0x1E90FF }, { "firebrick", 0xB22222 },
        { "floralwhite", 0xFFFAF0 }, { "forestgreen", 0x228B22 }, { "gainsboro", 0xDCDCDC }, { "ghostwhite", 0xF8F8FF },
        { "gold", 0xFFD700 }, { "goldenrod", 0xDAA520 }, { "greenyellow", 0xADFF2F }, { "grey", 0x808080 },
        { "honeydew", 0xF0FFF0 }, { "hotpink", 0xFF69B4 }, { "indianred", 0xCD5C5C }, { "indigo", 0x4B0082 },
        { "ivory", 0xFFFFF0 }, { "khaki", 0xF0E68C }, { "lavender", 0xE6E6FA }, { "lavenderblush", 0xFFF0F5 },
        { "lawngreen", 0x7CFC00 }, { "lemonchiffon", 0xFFFACD }, { "lightblue", 0xADD8E6 }, { "lightcoral", 0xF08080 },
        { "lightcyan", 0xE0FFFF }, { "lightgoldenrodyellow", 0xFAFAD2 }, { "lightgray", 0xD3D3D3 }, { "lightgreen", 0x90EE90 },
        { "lightgrey", 0xD3D3D3 }, { "lightpink", 0xFFB6C1 }, { "lightsalmon", 0xFFA07A }, { "lightseagreen", 0x20B2AA },
        { "lightskyblue", 0x87CEFA }, { "lightslategray", 0x778899 }, { "lightslategrey", 0x778899 }, { "lightsteelblue", 0xB0C4DE },
        { "lightyellow", 0xFFFFE0 }, { "limegreen", 0x32CD32 }, { "linen", 0xFAF0E6 }, { "magenta", 0xFF00FF },
        { "mediumaquamarine", 0x66CDAA }, { "mediumblue", 0x0000CD }, { "mediumorchid", 0xBA55D3 }, { "mediumpurple", 0x9370DB },
        { "mediumseagreen", 0x3CB371 }, { "mediumslateblue", 0x7B68EE }, { "mediumspringgreen", 0x00FA9A }, { "mediumturquoise", 0x48D1CC },
        { "mediumvioletred", 0xC71585 }, { "midnightblue", 0x191970 }, { "mintcream", 0xF5FFFA }, { "mistyrose", 0xFFE4E1 },
        { "moccasin", 0xFFE4B5 }, { "navajowhite", 0xFFDEAD }, { "oldlace", 0xFDF5E6 }, { "olivedrab", 0x6B8E23 },
        { "orangered", 0xFF4500 }, { "orchid", 0xDA70D6 }, { "palegoldenrod", 0xEEE8AA }, { "palegreen", 0x98FB98 },
        { "paleturquoise", 0xAFEEEE }, { "palevioletred", 0xDB7093 }, { "papayawhip", 0xFFEFD5 }, { "peachpuff", 0xFFDAB9 },
        { "peru", 0xCD853F }, { "pink", 0xFFC0CB }, { "plum", 0xDDA0DD }, { "powderblue", 0xB0E0E6 },
        { "rosybrown", 0xBC8F8F }, { "royalblue", 0x4169E1 }, { "saddlebrown", 0x8B4513 }, { "salmon", 0xFA8072 },
        { "sandybrown", 0xF4A460 }, { "seagreen", 0x2E8B57 }, { "seashell", 0xFFF5EE }, { "sienna", 0xA0522D },
        { "skyblue", 0x87CEEB }, { "slateblue", 0x6A5ACD }, { "slategray", 0x708090 }, { "slategrey", 0x708090 },
        { "snow", 0xFFFAFA }, { "springgreen", 0x00FF7F }, { "steelblue", 0x4682B4 }, { "tan", 0xD2B48C },
        { "thistle", 0xD8BFD8 }, { "tomato", 0xFF6347 }, { "turquoise", 0x40E0D0 }, { "violet", 0xEE82EE },
        { "wheat", 0xF5DEB3 }, { "whitesmoke", 0xF5F5F5 }, { "yellowgreen", 0x9ACD32 }
    };
}

class SSRGB
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("usage: ss-rgb <target> <command> [rgb1 rgb2 ... rgb8]");
            Console.WriteLine("target: mouse | keyboard | both");
            Console.WriteLine("command:");
            Console.WriteLine("  color <up to 8 RGB hex values or color names, e.g. FF0000 red orange>");
            Console.WriteLine("  all <one RGB hex value or color name, e.g. FF0000 or blue>");
            Console.WriteLine("  reaction <off|RGB hex value|color name, e.g. FF0000 or lightblue> (mouse only)");
            return;
        }

        string target = args[0].ToLower();
        string command = args[1].ToLower();

        if (target != "mouse" && target != "keyboard" && target != "both")
        {
            Console.WriteLine($"Unknown target: {target}");
            return;
        }

        bool isColorCommand = command == "color";
        bool isAllCommand = command == "all";
        bool isReactionCommand = command == "reaction";
        byte[] dataMouse = null;
        byte[] dataKeyboard = null;

        if (isColorCommand)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Please specify at least one RGB value or color name for 'color'.");
                return;
            }
            if (args.Length > 10)
            {
                Console.WriteLine("A maximum of 8 RGB values or color names is allowed.");
                return;
            }

            int rgbCount = args.Length - 2;
            int[] rgbs = new int[rgbCount];
            for (int i = 0; i < rgbCount; i++)
            {
                if (!TryParseColor(args[2 + i], out rgbs[i]))
                {
                    Console.WriteLine($"Invalid RGB value or color name: {args[2 + i]}");
                    return;
                }
            }
            // Mouse: flags 0x07, Keyboard: flags 0xFF
            dataMouse = BuildRgbCommand(0x07, rgbs);
            dataKeyboard = BuildRgbCommand(0xFF, rgbs);
        }
        else if (isAllCommand)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Please specify exactly one RGB value or color name for 'all'.");
                return;
            }
            if (!TryParseColor(args[2], out int rgb))
            {
                Console.WriteLine($"Invalid RGB value or color name: {args[2]}");
                return;
            }
            // Mouse: 3x, Keyboard: 8x
            dataMouse = BuildRgbCommand(0x07, rgb, rgb, rgb);
            dataKeyboard = BuildRgbCommand(0xFF, rgb, rgb, rgb, rgb, rgb, rgb, rgb, rgb);
        }
        else if (isReactionCommand)
        {
            if (target != "mouse")
            {
                Console.WriteLine("The 'reaction' command can only be used with 'mouse' as target.");
                return;
            }
            if (args.Length != 3)
            {
                Console.WriteLine("Please specify 'off', an RGB hex value or a color name for 'reaction'.");
                return;
            }
            string reactionParam = args[2].ToLower();
            if (reactionParam == "off")
            {
                dataMouse = new byte[] { 0x00, 0x26, 0x00, 0x00, 0x00, 0x00, 0x00 };
            }
            else if (TryParseColor(reactionParam, out int rgb))
            {
                dataMouse = new byte[] { 0x00, 0x26, 0x01, 0x00, (byte)((rgb >> 16) & 0xFF), (byte)((rgb >> 8) & 0xFF), (byte)(rgb & 0xFF) };
            }
            else
            {
                Console.WriteLine("Invalid parameter for 'reaction'. Expected 'off', a 6-digit RGB hex value or a color name.");
                return;
            }
        }
        else
        {
            Console.WriteLine($"Unknown command: {command}");
            return;
        }

        int vendorId = 0x1038;
        int productIdKeyboard = 0x1622;
        int productIdMouse = 0x1836;

        var deviceList = DeviceList.Local;

        var keyboardDevices = deviceList.GetHidDevices(vendorId, productIdKeyboard);
        var mouseDevices = deviceList.GetHidDevices(vendorId, productIdMouse);
        HidDevice hidDeviceKeyboard = null;
        HidDevice hidDeviceMouse = null;

        foreach (var dev in keyboardDevices)
        {
            if (dev.DevicePath.Contains("&mi_01"))
            {
                Console.WriteLine($"Keyboard DevicePath: {dev.DevicePath}");
                hidDeviceKeyboard = dev;
                break;
            }
        }

        foreach (var dev in mouseDevices)
        {
            if (dev.DevicePath.Contains("&mi_03"))
            {
                Console.WriteLine($"Mouse DevicePath: {dev.DevicePath}");
                hidDeviceMouse = dev;
                break;
            }
        }

        if ((target == "mouse" && hidDeviceMouse == null) ||
            (target == "keyboard" && hidDeviceKeyboard == null) ||
            (target == "both" && hidDeviceMouse == null && hidDeviceKeyboard == null))
        {
            Console.WriteLine("HID device not found.");
            return;
        }

        if ((target == "mouse" || target == "both") && hidDeviceMouse != null)
        {
            byte[] data = (isColorCommand || isAllCommand || isReactionCommand) ? dataMouse : null;
            if (data != null)
            {
                using (var stream = hidDeviceMouse.Open())
                {
                    try
                    {
                        stream.Write(data);
                        Console.WriteLine("Command " + command + " sent to mouse");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while sending to mouse: " + ex.Message);
                    }
                }
            }
        }
        if ((target == "keyboard" || target == "both") && hidDeviceKeyboard != null)
        {
            byte[] data = (isColorCommand || isAllCommand) ? dataKeyboard : null;
            if (data != null)
            {
                using (var stream = hidDeviceKeyboard.Open())
                {
                    try
                    {
                        stream.Write(data);
                        Console.WriteLine("Command " + command + " sent to keyboard");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error while sending to keyboard: " + ex.Message);
                    }
                }
            }
        }
    }

    static bool TryParseColor(string input, out int rgb)
    {
        // Hex value (6 digits)
        if (input.Length == 6 && int.TryParse(input, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out rgb))
        {
            return true;
        }
        // CSS color name
        if (CssColorNames.Map.TryGetValue(input, out rgb))
        {
            return true;
        }
        rgb = 0;
        return false;
    }

    static byte[] BuildRgbCommand(byte flags, params int[] rgbs)
    {
        var cmd = new List<byte> { 0x00, 0x21, flags };
        foreach (var color in rgbs)
        {
            cmd.Add((byte)((color >> 16) & 0xFF)); // Red
            cmd.Add((byte)((color >> 8) & 0xFF));  // Green
            cmd.Add((byte)(color & 0xFF));         // Blue
        }
        return cmd.ToArray();
    }
}