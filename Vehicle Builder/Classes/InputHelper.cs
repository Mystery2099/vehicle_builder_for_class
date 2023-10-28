using Vehicle_Builder.Enums;
using static System.Console;

namespace Vehicle_Builder.Classes;

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
    internal static string GetStringInput(string prompt) => ValidateInput(prompt, s => s.Validate());

    //returns a valid uint input
    internal static uint GetUIntInput(string prompt)
    {
        uint result = 0;
        _ = ValidateInput(prompt, 
                s => s.ValidateAsUint(out result), 
                $"The {prompt} must be a positive whole number!");
        return result;
    }
    //returns a valid byte input
    internal static byte GetByteInput(string prompt)
    {
        byte result = 0;
        _ = ValidateInput(prompt,
                s => s.ValidateAsByte(out result),
                $"The {prompt} must be a positive whole number!");
        return result;
    }
    //returns a valid short input
    internal static short GetShortInput(string prompt)
    {
        short result = 0;
        _ = ValidateInput(prompt,
                s => s.ValidateAsShort(out result),
                $"The {prompt} must be a positive whole number!");
        return result;
    }

    //returns a valid double input
    internal static double GetDoubleInput(string prompt)
    {
        var result = 0.0;
        _ = ValidateInput(prompt, s => s.ValidateAsDouble(out result), $"The {prompt} must be a positive number!");
        return result;
    }


    /*
     * Takes in a custom prompt for the the message to the user
     * validation method is used on the user input to make sure its valid using method defined rules
     * sends a message using errorMsg variable if the user input is invalid according to the validation function
     */
    private static string ValidateInput(string prompt, Func<string?, bool> validate,
                                       string errorMsg = "Invalid input! Please try again.")
    {
        string input;
        var showErrorMsg = false;
        do
        {
            Clear();
            if (showErrorMsg) WriteLine(errorMsg);
            showErrorMsg = true;
            
            WriteLine($"\nPlease enter a valid {prompt} for your vehicle\n");
            
            input = ReadLine() ?? string.Empty;
            if (!validate(input)) continue;
            break;
        } while (true);
        return input;
    }
    
    /*
     * Extension methods to validate strings & numbers
     */
    private static bool Validate(this string? input)
    {
        return input is not (null or " " or "") && 
               //Makes sure that string does not contain any characters which cannot be in a file name
               !Path.GetInvalidFileNameChars().Any(input.Contains);
    }

    private static bool ValidateAsUint(this string? input, out uint parsedInput) => uint.TryParse(input, out parsedInput);

    private static bool ValidateAsByte(this string? input, out byte parsedInput) => byte.TryParse(input, out parsedInput) && parsedInput > 0;

    private static bool ValidateAsShort(this string? input, out short parsedInput) => short.TryParse(input, out parsedInput) && parsedInput > 0;

    private static bool ValidateAsDouble(this string? input, out double paredInput) => double.TryParse(input, out paredInput) && paredInput >= 0;
}