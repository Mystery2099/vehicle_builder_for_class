using Vehicle_Builder.Classes;
using static System.Console;

namespace Vehicle_Builder;

internal static class VehicleBuilder
{
    private static bool _exit;

    internal static void Main()
    {
        while (!_exit)
        {
            if (_exit) break;
            StartProgram();
        }
    }

    private static void StartProgram()
    {
        do
        {
            Clear();
            Title = "Vehicle Builder";
            WriteLine("Welcome to Vehicle Builder!\n" +
                      "Would You Like to create a new vehicle?\n" +
                      "Type the corresponding number below: \n" +
                      "1 -> Create New Vehicle\n" + "2 -> Exit\n");
            switch (ReadLine()?.ToLower())
            {
                case "1" or "create" or "yes":
                    InputHelper.CreateVehiclePrompt();
                    break;
                case "2" or "no" or "exit":
                    Exit();
                    break;
                default:
                    WriteLine("Invalid input! Please input valid numbers!");
                    ReadKey();
                    Clear();
                    continue;
            }

            break;
        } while (true);
    }


    internal static void Exit()
    {
        while (true)
        {
            Clear();
            WriteLine("Would you like to exit?");

            switch (ReadLine()?.ToLower())
            {
                case "1" or "yes":
                    _exit = true;
                    WriteLine("Ending Application...");
                    break;
                case "2" or "no":
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