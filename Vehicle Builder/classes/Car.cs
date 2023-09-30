using Vehicle_Builder.Enums;

//File Scoped namespace therefore doesn't need curly brackets
namespace Vehicle_Builder.classes;

internal class Car : Vehicle, IEquatable<Car?>
{
    // Default number of wheels for a car
    public const byte DefaultWheelCount = 4;

    // Properties specific to cars
    private byte NumberOfDoors { get; }
    private string FuelType { get; }

    // Constructor to initialize car properties and call base class constructor
    internal Car(string make, string model, short year, string color, double price, double topSpeed, byte numberOfDoors, string fuelType) : base(VehicleTypes.Car, make, model, year, color, price, topSpeed, DefaultWheelCount)
    {
        NumberOfDoors = numberOfDoors;
        FuelType = fuelType;
    }

    // Override the Details property to include car-specific details
    public override string Details =>
        base.Details + $"Number of Doors: {NumberOfDoors}\n" +
        $"Fuel Type: {FuelType}\n";

    public override bool Equals(object? obj) => Equals(obj as Car);

    public bool Equals(Car? other)
    {
        return other is not null &&
               base.Equals(other) &&
               NumberOfDoors == other.NumberOfDoors &&
               FuelType == other.FuelType;
    }

    public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), NumberOfDoors, FuelType);

    public static bool operator ==(Car? left, Car? right) => EqualityComparer<Car>.Default.Equals(left, right);

    public static bool operator !=(Car? left, Car? right) => !(left == right);
}
