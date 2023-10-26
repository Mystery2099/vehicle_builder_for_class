using Vehicle_Builder.Enums;

namespace Vehicle_Builder.Classes;

internal class Motorcycle : Vehicle, IEquatable<Motorcycle?>
{
    // Default number of wheels for a motorcycle
    private const byte DefaultWheelCount = 2;

    // Properties specific to motorcycles
    private uint EngineDisplacement { get; }
    private string BikeType { get; }

    // Constructor to initialize motorcycle properties and call the base class constructor
    internal Motorcycle() : base(VehicleTypes.Motorcycle, DefaultWheelCount)
    {
        EngineDisplacement = InputHelper.GetUIntInput("engine displacement");
        BikeType = InputHelper.GetStringInput("type of bike");
    }

    // Override the Details property to include motorcycle-specific details
    public override string Details => base.Details + $"{nameof(EngineDisplacement)}: {EngineDisplacement}\n" + $"{nameof(BikeType)}: {BikeType}\n";


    // Implement equality check for motorcycles
    public override bool Equals(object? obj) => Equals(obj as Motorcycle);

    public bool Equals(Motorcycle? other)
    {
        return other is not null &&
               base.Equals(other) && // Check equality of base class properties
               EngineDisplacement == other.EngineDisplacement &&
               BikeType == other.BikeType;
    }

    // Implement a custom hash code calculation 
    public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), EngineDisplacement, BikeType);

    // Implement equality operators for motorcycles. 
    public static bool operator ==(Motorcycle? left, Motorcycle? right) => EqualityComparer<Motorcycle>.Default.Equals(left, right);

    public static bool operator !=(Motorcycle? left, Motorcycle? right) => !(left == right);
}
