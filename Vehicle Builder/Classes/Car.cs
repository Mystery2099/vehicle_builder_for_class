using Vehicle_Builder.Enums;

namespace Vehicle_Builder.Classes;

internal class Car : Vehicle, IEquatable<Car?>
{
    // Default number of wheels for a car
    private const byte DefaultWheelCount = 4;

    // Properties specific to cars
    private byte NumberOfDoors { get; }
    private string FuelType { get; }

    // Constructor to initialize car properties and call base class constructor
    internal Car() : base(VehicleTypes.Car, DefaultWheelCount)
    {
        NumberOfDoors = InputHelper.GetByteInput("number of doors");
        FuelType = InputHelper.GetStringInput("fuel type");
    }

    // Override the Details property to include car-specific details
    public override string Details => base.Details + $"Number of Doors: {NumberOfDoors}\n" +
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
