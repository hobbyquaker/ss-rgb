using System;
using System.Collections.Generic;
using System.Globalization;
using HidSharp;

class SSRGB
{
    static void Main(string[] args)
    {
        if (!ValidateArgs(args, out string target, out string command)) return;

        if (!TryBuildCommandData(args, command, target, out byte[] dataMouse, out byte[] dataKeyboard)) return;

        var (hidDeviceKeyboard, hidDeviceMouse) = FindDevices(target);
        if (!ValidateDevices(target, hidDeviceKeyboard, hidDeviceMouse)) return;

        SendCommand(target, command, dataMouse, dataKeyboard, hidDeviceKeyboard, hidDeviceMouse);
    }

    static bool ValidateArgs(string[] args, out string target, out string command)
    {
        target = null;
        command = null;
        if (args.Length < 2)
        {
            PrintUsage();
            return false;
        }
        target = args[0].ToLower();
        command = args[1].ToLower();
        if (target != "mouse" && target != "keyboard" && target != "both")
        {
            Console.WriteLine($"Unknown target: {target}");
            return false;
        }
        return true;
    }

    static void PrintUsage()
    {
        Console.WriteLine("usage: ss-rgb <target> <command> [rgb1 rgb2 ... rgb8]");
        Console.WriteLine("target: mouse | keyboard | both");
        Console.WriteLine("command:");
        Console.WriteLine("  color <up to 8 RGB hex values or color names, e.g. FF0000 red orange>");
        Console.WriteLine("  all <one RGB hex value or color name, e.g. FF0000 or blue>");
        Console.WriteLine("  reaction <off|RGB hex value|color name, e.g. FF0000 or lightblue> (mouse only)");
    }

    static bool TryBuildCommandData(string[] args, string command, string target, out byte[] dataMouse, out byte[] dataKeyboard)
    {
        dataMouse = null;
        dataKeyboard = null;
        switch (command)
        {
            case "color":
                return TryBuildColorData(args, out dataMouse, out dataKeyboard);
            case "all":
                return TryBuildAllData(args, out dataMouse, out dataKeyboard);
            case "reaction":
                return TryBuildReactionData(args, target, out dataMouse);
            default:
                Console.WriteLine($"Unknown command: {command}");
                return false;
        }
    }

    static bool TryBuildColorData(string[] args, out byte[] dataMouse, out byte[] dataKeyboard)
    {
        dataMouse = null;
        dataKeyboard = null;
        if (args.Length < 3)
        {
            Console.WriteLine("Please specify at least one RGB value or color name for 'color'.");
            return false;
        }
        if (args.Length > 10)
        {
            Console.WriteLine("A maximum of 8 RGB values or color names is allowed.");
            return false;
        }
        int rgbCount = args.Length - 2;
        int[] rgbs = new int[rgbCount];
        for (int i = 0; i < rgbCount; i++)
        {
            if (!TryParseColor(args[2 + i], out rgbs[i]))
            {
                Console.WriteLine($"Invalid RGB value or color name: {args[2 + i]}");
                return false;
            }
        }
        dataMouse = BuildRgbCommand(0x07, rgbs);
        dataKeyboard = BuildRgbCommand(0xFF, rgbs);
        return true;
    }

    static bool TryBuildAllData(string[] args, out byte[] dataMouse, out byte[] dataKeyboard)
    {
        dataMouse = null;
        dataKeyboard = null;
        if (args.Length != 3)
        {
            Console.WriteLine("Please specify exactly one RGB value or color name for 'all'.");
            return false;
        }
        if (!TryParseColor(args[2], out int rgb))
        {
            Console.WriteLine($"Invalid RGB value or color name: {args[2]}");
            return false;
        }
        dataMouse = BuildRgbCommand(0x07, rgb, rgb, rgb);
        dataKeyboard = BuildRgbCommand(0xFF, rgb, rgb, rgb, rgb, rgb, rgb, rgb, rgb);
        return true;
    }

    static bool TryBuildReactionData(string[] args, string target, out byte[] dataMouse)
    {
        dataMouse = null;
        if (target != "mouse")
        {
            Console.WriteLine("The 'reaction' command can only be used with 'mouse' as target.");
            return false;
        }
        if (args.Length != 3)
        {
            Console.WriteLine("Please specify 'off', an RGB hex value or a color name for 'reaction'.");
            return false;
        }
        string reactionParam = args[2].ToLower();
        if (reactionParam == "off")
        {
            dataMouse = new byte[] { 0x00, 0x26, 0x00, 0x00, 0x00, 0x00, 0x00 };
            return true;
        }
        if (TryParseColor(reactionParam, out int rgb))
        {
            dataMouse = new byte[] { 0x00, 0x26, 0x01, 0x00, (byte)((rgb >> 16) & 0xFF), (byte)((rgb >> 8) & 0xFF), (byte)(rgb & 0xFF) };
            return true;
        }
        Console.WriteLine("Invalid parameter for 'reaction'. Expected 'off', a 6-digit RGB hex value or a color name.");
        return false;
    }

    static (HidDevice hidDeviceKeyboard, HidDevice hidDeviceMouse) FindDevices(string target)
    {
        int vendorId = 0x1038;
        int productIdKeyboard = 0x1622;
        int productIdMouse = 0x1836;
        var deviceList = DeviceList.Local;
        HidDevice hidDeviceKeyboard = null;
        HidDevice hidDeviceMouse = null;

        var keyboardDevices = deviceList.GetHidDevices(vendorId, productIdKeyboard);
        var mouseDevices = deviceList.GetHidDevices(vendorId, productIdMouse);

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
        return (hidDeviceKeyboard, hidDeviceMouse);
    }

    static bool ValidateDevices(string target, HidDevice hidDeviceKeyboard, HidDevice hidDeviceMouse)
    {
        if ((target == "mouse" && hidDeviceMouse == null) ||
            (target == "keyboard" && hidDeviceKeyboard == null) ||
            (target == "both" && hidDeviceMouse == null && hidDeviceKeyboard == null))
        {
            Console.WriteLine("HID device not found.");
            return false;
        }
        return true;
    }

    static void SendCommand(
        string target,
        string command,
        byte[] dataMouse,
        byte[] dataKeyboard,
        HidDevice hidDeviceKeyboard,
        HidDevice hidDeviceMouse
    )
    {
        if ((target == "mouse" || target == "both") && hidDeviceMouse != null)
        {
            byte[] data = dataMouse;
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
            byte[] data = dataKeyboard;
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