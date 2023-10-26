using static Vehicle_Builder.VehicleBuilder;
using static System.Console;
using Vehicle_Builder.Interfaces;
using Vehicle_Builder.Enums;

namespace Vehicle_Builder.Classes;

internal abstract class Vehicle : IEquatable<Vehicle?>, IVehicles
{
    // Common properties for all vehicles
    public VehicleTypes VehicleType { get; }
    public string Make { get; }
    public string Model { get; }
    public short Year { get; }
    public string Color { get; }
    public double Price { get; }
    public double TopSpeed { get; }
    public byte WheelCount { get; }
    
    private readonly string _name;

    public virtual string Details =>
        $"{nameof(VehicleType)}: {VehicleType}\n" +
        $"{nameof(Make)}: {Make}\n" +
        $"{nameof(Model)}: {Model}\n" +
        $"{nameof(Year)}: {Year}\n" +
        $"{nameof(Color)}: {Color}\n" +
        $"{nameof(Price)}: ${Price}\n" +
        $"{nameof(TopSpeed)}: {TopSpeed}mph\n";

    internal Vehicle(VehicleTypes vehicleType, byte wheelCount)
    {
        Title = $"{vehicleType} Builder";
        VehicleType = vehicleType;
        Make = InputHelper.GetStringInput("make");
        Model = InputHelper.GetStringInput("model");
        Year = InputHelper.GetShortInput("year");
        Color = InputHelper.GetStringInput("color");
        Price = InputHelper.GetDoubleInput("price");
        TopSpeed = InputHelper.GetDoubleInput("maximum speed (in mph)");
        WheelCount = wheelCount;
        
        _name = $"{Color} {Year} {Make} {Model}";

    }

    public void AskToSave()
    {
        var displayInputError = false;
        while (true)
        {
            Clear();
            if (displayInputError) {
                WriteLine("\nYour input was invalid!\n" + "Please input '1', '2', 'yes', or 'no'");
                displayInputError = false;
            }
            else
            {
                Write(Details);
                WriteLine($"\nWould you like to save your {_name} as a text file?\n" + "1 -> Yes\n" + "2 -> No\n");
            }
            
            switch (ReadLine())
            {
                case "yes" or "1":
                    SaveDetails();
                    break;
                case "no" or "2":
                    WriteLine("Are you sure?");
                    switch (ReadLine())
                    {
                        case "2" or "no": continue;
                        case "yes" or "1":
                            Exit();
                            break;
                    }

                    break;
                default:
                    displayInputError = true;
                    continue;
            }

            break;
        }
    }

    //Saves the details of the vehicle to a text file
    public void SaveDetails()
    {
        var fileName = Path.Combine(@"..\..\..\", "Saved-Vehicles", $"{_name}.txt");
        var fileContents = $"{_name} Profile\n{Details}";
        File.WriteAllText(fileName, fileContents);
    }

    public static Vehicle Create(VehicleTypes vehicleType) => vehicleType switch
    {
        VehicleTypes.Car => new Car(),
        VehicleTypes.Truck => new Truck(),
        VehicleTypes.Motorcycle => new Motorcycle(),
        _ => throw new ArgumentOutOfRangeException(nameof(vehicleType), vehicleType, null)
    };

    // Override ToString to return vehicle details
    public override string ToString() => Details;

    // Implement equality check for vehicles
    public override bool Equals(object? obj) => Equals(obj as Vehicle);

    public bool Equals(Vehicle? other)
    {
        return other is not null &&
               _name == other._name &&
               VehicleType == other.VehicleType &&
               Make == other.Make &&
               Model == other.Model &&
               Year == other.Year &&
               Color == other.Color &&
               Math.Abs(Price - other.Price) == 0 &&
               Math.Abs(TopSpeed - other.TopSpeed) == 0 &&
               WheelCount == other.WheelCount &&
               Details == other.Details;
    }

    // Implement a custom hash code calculation
    public override int GetHashCode()
    {
        HashCode hash = new();
        hash.Add(_name);
        hash.Add(VehicleType);
        hash.Add(Make);
        hash.Add(Model);
        hash.Add(Year);
        hash.Add(Color);
        hash.Add(Price);
        hash.Add(TopSpeed);
        hash.Add(WheelCount);
        hash.Add(Details);
        return hash.ToHashCode();
    }

    // Implement equality operators for vehicles 
    // I know that they aren't really needed for this small project but I figured I'd add em anyway
    public static bool operator ==(Vehicle? left, Vehicle? right) => EqualityComparer<Vehicle>.Default.Equals(left, right);

    public static bool operator !=(Vehicle? left, Vehicle? right) => !(left == right);
}
