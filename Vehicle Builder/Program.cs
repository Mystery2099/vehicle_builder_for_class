using Vehicle_Builder.classes;
using static System.Console;

//File Scoped namespace therefore doesn't need curly brackets
namespace Vehicle_Builder;

internal static class VehicleBuilder
{
    private static bool _exit;

    internal static void Main()
    {
        while (!_exit)
        {
            if (_exit)
            {
                break;
            }
            StartProgram();
        }
    }

    private static void StartProgram()
    {
        while (true)
        {
            Title = "Vehicle Builder";
            WriteLine("Welcome to Vehicle Builder!\n" + "Would You Like to create a new vehicle?\n" + "Type the corresponding number below: \n" + "1 -> Create New Vehicle\n" + "2 -> Exit\n");

            switch (ReadLine()?.ToLower())
            {
                case "1":
                case "create":
                case "yes":
                    VehicleUtil.CreateNewVehicle();
                    break;
                case "2":
                case "no":
                case "exit":
                    Exit();
                    break;
                default:
                    WriteLine("Invalid input! Please input valid numbers!");
                    ReadKey();
                    Clear();
                    continue;
            }

            break;
        }
    }


    internal static void Exit()
    {
        while (true)
        {
            Clear();
            WriteLine("Would you like to exit?");
            switch (ReadLine()?.ToLower())
            {
                case "1":
                case "yes":
                    _exit = true;
                    break;
                case "2":
                case "no":
                    Clear();
                    return;
                case "force close":
                    Environment.Exit(0);
                    break;
                default:
                    continue;
            }

            break;
        }
    }
}