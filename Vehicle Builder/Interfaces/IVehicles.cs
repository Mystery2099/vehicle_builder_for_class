using Vehicle_Builder.Enums;

namespace Vehicle_Builder.Interfaces;

internal interface IVehicles
{
    // Common properties for all vehicles
    public string Name { get; }
    public VehicleTypes VehicleType { get; }
    public string Make { get; }
    public string Model { get; }
    public short Year { get; }
    public string Color { get; }
    public double Price { get; }
    public double TopSpeed { get; }
    public byte WheelCount { get; }

    // property to provide vehicle details
    public string Details { get; }

    //Method for saving the details to a text file
    public void SaveDetails();

    //Method for asking whether the user would like to save the details
    public void AskToSave();
}
