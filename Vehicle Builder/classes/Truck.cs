using Vehicle_Builder.Enums;

//File Scoped namespace therefore doesn't need curly brackets
namespace Vehicle_Builder.classes;

internal class Truck : Vehicle, IEquatable<Truck?>
{
    // Properties specific to trucks
    private double PayloadCapacity { get; }
    private string TransmissionType { get; }

    // Constructor to initialize truck properties and call the base class constructor
    internal Truck(string make, string model, short year, string color, double price, double topSpeed, byte wheels, double payloadCapacity, string transmissionType) : base(VehicleTypes.Truck, make, model, year, color, price, topSpeed, wheels)
    {
        PayloadCapacity = payloadCapacity;
        TransmissionType = transmissionType;
    }

    // Override the Details property to include truck-specific details
    public override string Details =>
        base.Details + $"{nameof(PayloadCapacity)}: {PayloadCapacity}\n" +
        $"{nameof(TransmissionType)}: {TransmissionType}\n";


    // Implement equality check for trucks
    public override bool Equals(object? obj) => Equals(obj as Truck);

    public bool Equals(Truck? other) =>
        other is not null &&
        base.Equals(other) && // Check equality of base class properties
        Math.Abs(PayloadCapacity - other.PayloadCapacity) == 0 &&
        TransmissionType == other.TransmissionType;

    // Implement a custom hash code calculation
    public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), PayloadCapacity, TransmissionType);

    // Implement equality operators for trucks
    public static bool operator ==(Truck? left, Truck? right) => EqualityComparer<Truck>.Default.Equals(left, right);

    public static bool operator !=(Truck? left, Truck? right) => !(left == right);
}
