using Vehicle_Builder.Enums;
using static System.Console;

//File Scoped namespace therefore doesn't need curly brackets
namespace Vehicle_Builder.classes;

internal static class VehicleUtil
{
    internal static void CreateVehiclePrompt()
    {
        var message = "Would you like to create a new car, truck, or motorcycle: \n";
        var possibleTypes = Enum.GetValues(typeof(VehicleTypes));
        for (var i = 0; i < possibleTypes.Length; i++)
        {
            message += $"{i + 1} -> {possibleTypes.GetValue(i)}\n";
        }
        
        while (true)
        {
            WriteLine($"{message}4 -> Cancel\n");
            switch (ReadLine())
            {
                case "car":
                case "1":
                    CreateVehicle(VehicleTypes.Car);
                    break;
                case "truck":
                case "2":
                    CreateVehicle(VehicleTypes.Truck);
                    break;
                case "motorcycle":
                case "3":
                    CreateVehicle(VehicleTypes.Motorcycle);
                    break;
                case "cancel":
                case "exit":
                case "4":
                    Clear();
                    return;
                default:
                    WriteLine("Invalid input! Please input valid numbers!");
                    ReadKey();
                    Clear();
                    continue;
            }

            break;
        }
    }

    private static void CreateVehicle(VehicleTypes vehicleType)
    {
        Title = $"{vehicleType} Builder";

        var data = CreateBasicProperties(vehicleType);
        Vehicle? vehicle;
        switch (vehicleType)
        {
            case VehicleTypes.Car:
                {
                    var doorCount = GetByteInput("number of doors");
                    var fuelType = GetStringInput("fuel type");

                    vehicle = new Car(data.Make, data.Model, data.Year, data.Color, data.Price, data.TopSpeed, doorCount, fuelType);
                    break;
                }

            case VehicleTypes.Truck:
                {
                    var payloadDisplacement = GetDoubleInput("payload displacement");
                    var transmissionType = GetStringInput("type of transmission");

                    vehicle = new Truck(data.Make,
                        data.Model,
                        data.Year,
                        data.Color,
                        data.Price,
                        data.TopSpeed,
                        data.Wheels,
                        payloadDisplacement,
                        transmissionType);
                    break;
                }

            case VehicleTypes.Motorcycle:
                {
                    var bikeType = GetStringInput("type of bike");
                    var engineDisplacement = GetUIntInput("engine displacement");

                    vehicle = new Motorcycle(data.Make,
                        data.Model,
                        data.Year,
                        data.Color,
                        data.Price,
                        data.TopSpeed,
                        engineDisplacement,
                        bikeType);
                    break;
                }
            default:
                throw new ArgumentOutOfRangeException(nameof(vehicleType), vehicleType, null);
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

    // Helper method to create basic vehicle properties common to all vehicle types
    private static BasicVehicleProperties CreateBasicProperties(VehicleTypes vehicleType)
    {
        var make = GetStringInput("make");
        var model = GetStringInput("model");
        var year = GetShortInput("year");
        var color = GetStringInput("color");
        var price = GetDoubleInput("price");
        var topSpeed = GetDoubleInput("maximum speed");

        var wheelCount = vehicleType switch
        {
            VehicleTypes.Car or VehicleTypes.Motorcycle => (vehicleType == VehicleTypes.Car) ? 
                Car.DefaultWheelCount : Motorcycle.DefaultWheelCount,
            _ => GetByteInput("wheel count"),
        };
        return new BasicVehicleProperties(make, model, year, color, price, topSpeed, wheelCount);
    }

    // Data structure to store basic vehicle properties
    private record BasicVehicleProperties(string Make, string Model, short Year, string Color, double Price, double TopSpeed, byte Wheels);
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