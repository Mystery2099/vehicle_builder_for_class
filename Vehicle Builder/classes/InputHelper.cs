using Vehicle_Builder.Enums;
using static System.Console;

//File Scoped namespace therefore doesn't need curly brackets
namespace Vehicle_Builder.classes;

internal static class InputHelper
{
    internal static void CreateVehiclePrompt()
    {
        var message = "Would you like to create a new car, truck, or motorcycle:\n";
        var possibleTypes = Enum.GetValues(typeof(VehicleTypes));
        for (var i = 0; i < possibleTypes.Length; i++)
        {
            message += $"{i + 1} -> {possibleTypes.GetValue(i)}\n";
        }

        Vehicle? vehicle;
        while (true)
        {
            WriteLine($"{message}4 -> Cancel\n");
            var input = ReadLine();
            if (input is "cancel" or "exit" or "4")
            {
                Clear();
                return;
            }
            VehicleTypes? type = input switch
            {
                "car" or "1" => VehicleTypes.Car,
                "truck" or "2" => VehicleTypes.Truck,
                "motorcycle" or "3" => VehicleTypes.Motorcycle,
                _ => null
            };
            if (type is null)
            {
                WriteLine("Invalid input! Please input valid numbers!");
                ReadKey();
                Clear();
            }
            else
            {
                vehicle = Vehicle.Create((VehicleTypes)type);
                break;
            }
        }
        vehicle.AskToSave();
    }
    
    //returns a valid string input
    internal static string GetStringInput(string prompt)
    {
        return ValidateInput(prompt, s => s.IsValidString(), $"The {prompt} must me greater than 1 character long!");
    }

    //returns a valid uint input
    internal static uint GetUIntInput(string prompt)
    {
        _ = uint.TryParse(ValidateInput(prompt, 
                s => s.IsValidUInt(), 
                $"The {prompt} must be a positive whole number!"), 
            out var result);
        return result;
    }
    //returns a valid byte input
    internal static byte GetByteInput(string prompt)
    {
        _ = byte.TryParse(ValidateInput(prompt,
                s => s.IsValidByte(),
                $"The {prompt} must be a positive whole number!"),
            out var result);
        return result;
    }
    //returns a valid short input
    internal static short GetShortInput(string prompt)
    {
        _ = short.TryParse(ValidateInput(prompt,
                s => s.IsValidShort(),
                $"The {prompt} must be a positive whole number!"),
            out var result);
        return result;
    }

    //returns a valid double input
    internal static double GetDoubleInput(string prompt)
    {
        _ = double.TryParse(ValidateInput(prompt, s => s.IsValidDouble(), $"The {prompt} must be a positive number!"),
            out var result);
        return result;
    }


    //Takes in a custom prompt for the the message to the user
    //validation function is used on the user input to make sure its valid using method defined rules
    //sends a message using errorMsg variable if the user input is invalid according to the validation function
    private static string ValidateInput(string prompt, Func<string, bool> validation,
                                       string errorMsg = "Invalid input! Please try again.")
    {
        string? input;
        while (true)
        {
            WriteLine($"\nPlease enter a valid {prompt} for your vehicle\n");
            var isValid = false;
            input = ReadLine();
            if (input != null)
            {
                isValid = validation(input);
            }
            if (!isValid)
            {
                WriteLine(errorMsg);
            }
            else { break; }
        }
        return input ?? string.Empty;
    }
}

// Extension methods to validate strings and numbers
internal static class ExtensionMethods
{
    internal static bool IsValidString(this string str)
    {
        return str.Length > 1 && !Path.GetInvalidFileNameChars().Any(str.Contains);
    }

    internal static bool IsValidUInt(this string input)
    {
        var tryParse = uint.TryParse(input, out _);
        return tryParse;
    }
    internal static bool IsValidByte(this string input)
    {
        var tryParse = byte.TryParse(input, out var parsedInput);
        return tryParse && parsedInput > 0;
    }
    internal static bool IsValidShort(this string input)
    {
        var tryParse = short.TryParse(input, out var parsedInput);
        return tryParse && parsedInput > 0;
    }
    internal static bool IsValidDouble(this string input)
    {
        var tryParse = double.TryParse(input, out var val);
        return tryParse && val >= 0;
    }
}