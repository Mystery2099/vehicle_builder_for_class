using Vehicle_Builder.Enums;

namespace Vehicle_Builder.Classes;

internal class Truck : Vehicle, IEquatable<Truck?>
{
    // Properties specific to trucks
    private double PayloadCapacity { get; }
    private string TransmissionType { get; }

    // Constructor to initialize truck properties and call the base class constructor
    internal Truck() : base(VehicleTypes.Truck, InputHelper.GetByteInput("number of wheels"))
    {
        PayloadCapacity = InputHelper.GetDoubleInput("payload displacement");
        TransmissionType = InputHelper.GetStringInput("type of transmission");
    }

    // Override the Details property to include truck-specific details
    public override string Details => base.Details + $"{nameof(PayloadCapacity)}: {PayloadCapacity}\n" +
                                      $"{nameof(TransmissionType)}: {TransmissionType}\n";


    // Implement equality check for trucks
    public override bool Equals(object? obj) => Equals(obj as Truck);

    public bool Equals(Truck? other)
    {
        return other is not null &&
               base.Equals(other) && // Check equality of base class properties
               Math.Abs(PayloadCapacity - other.PayloadCapacity) == 0 &&
               TransmissionType == other.TransmissionType;
    }

    // Implement a custom hash code calculation
    public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), PayloadCapacity, TransmissionType);

    // Implement equality operators for trucks
    public static bool operator ==(Truck? left, Truck? right) => EqualityComparer<Truck>.Default.Equals(left, right);

    public static bool operator !=(Truck? left, Truck? right) => !(left == right);
}
