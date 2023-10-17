﻿using Vehicle_Builder.Classes;
using static Vehicle_Builder.VehicleBuilder;
using static System.Console;
using Vehicle_Builder.Interfaces;
using Vehicle_Builder.Enums;

namespace Vehicle_Builder.Classes;

internal abstract class Vehicle : IEquatable<Vehicle?>, IVehicles
{
    // Common properties for all vehicles
    public string Name => $"{Color} {Year} {Make} {Model}";
    public VehicleTypes VehicleType { get; }
    public string Make { get; }
    public string Model { get; }
    public short Year { get; }
    public string Color { get; }
    public double Price { get; }
    public double TopSpeed { get; }
    public byte WheelCount { get; }

    public virtual string Details =>
        $"{nameof(VehicleType)}: {VehicleType}\n" +
        $"{nameof(Make)}: {Make}\n" +
        $"{nameof(Model)}: {Model}\n" +
        $"{nameof(Year)}: {Year}\n" +
        $"{nameof(Color)}: {Color}\n" +
        $"{nameof(Price)}: ${Price}\n" +
        $"{nameof(TopSpeed)}: {TopSpeed}mph\n";

    internal Vehicle(VehicleTypes vehicleType, string make, string model, short year, string color, double price, double topSpeed, byte wheelCount)
    {
        VehicleType = vehicleType;
        Make = make;
        Model = model;
        Year = year;
        Color = color;
        Price = price;
        TopSpeed = topSpeed;
        WheelCount = wheelCount;
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
                WriteLine($"\nWould you like to save your {Name} as a text file?\n" + "1 -> Yes\n" + "2 -> No\n");
            }
            var input = ReadLine();
            switch (input)
            {
                case "yes":
                case "1":
                    SaveDetails();
                    break;
                case "no":
                case "2":
                    WriteLine("Are you sure?");
                    switch (ReadLine())
                    {
                        case "2":
                        case "no":
                            continue;
                        case "yes":
                        case "1":
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
        var fileName = Path.Combine(@"..\..\..\", "Saved-Vehicles", $"{Name}.txt");
        var fileContents = $"{Name} Profile\n{Details}";
        File.WriteAllText(fileName, fileContents);
    }

    public static Vehicle Create(VehicleTypes vehicleType)
    {
        Title = $"{vehicleType} Builder";
        var make = InputHelper.GetStringInput("make");
        var model = InputHelper.GetStringInput("model");
        var year = InputHelper.GetShortInput("year");
        var color = InputHelper.GetStringInput("color");
        var price = InputHelper.GetDoubleInput("price");
        var topSpeed = InputHelper.GetDoubleInput("maximum speed (in mph)");
        return vehicleType switch
        {
            VehicleTypes.Car => new Car(make,
                model,
                year,
                color,
                price,
                topSpeed, 
                InputHelper.GetByteInput("number of doors"),
                InputHelper.GetStringInput("fuel type")
            ),
            VehicleTypes.Truck => new Truck(make,
                model,
                year,
                color,
                price,
                topSpeed,
                InputHelper.GetByteInput("number of wheels"),
                InputHelper.GetDoubleInput("payload displacement"),
                InputHelper.GetStringInput("type of transmission")
            ),
            VehicleTypes.Motorcycle => new Motorcycle(make,
                model,
                year,
                color,
                price,
                topSpeed,
                InputHelper.GetUIntInput("engine displacement"),
                InputHelper.GetStringInput("type of bike")
            ),
            _ => throw new ArgumentOutOfRangeException(nameof(vehicleType), vehicleType, null)
        };
    }

    // Override ToString to return vehicle details
    public override string ToString() => Details;

    // Implement equality check for vehicles
    public override bool Equals(object? obj) => Equals(obj as Vehicle);

    public bool Equals(Vehicle? other)
    {
        return other is not null &&
               Name == other.Name &&
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
        hash.Add(Name);
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