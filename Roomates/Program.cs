using Roommates.Models;
using Roommates.Repositories;
using System;
using System.Collections.Generic;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;TrustServerCertificate=true;";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);

            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);

            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}.");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all unassigned chores"):
                        List<Chore> choreList = choreRepo.GetUnassignedChores();
                        foreach (Chore c in choreList)
                        {
                            Console.WriteLine($"{c.Name} is not assigned to a roommate");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for chore"):
                        Console.Write("Chore Id: ");
                        int ChoreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(ChoreId);
                        
                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for roommate"):
                        Console.Write("Roommate Id: ");
                        int RoommateId = int.Parse(Console.ReadLine());

                        Roommate roommate = roommateRepo.GetById(RoommateId);

                        Console.WriteLine($"{roommate.Id} - {roommate.FirstName} - {roommate.RentPortion} - {roommate.Room.Name}");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string choreName = Console.ReadLine();

                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName,
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Assign a chore"):
                        List<Chore> choreAssign = choreRepo.GetAll();
                        foreach (Chore c in choreAssign)
                        {
                            Console.WriteLine($"{c.Name} - {c.Id}");
                        }
                        Console.Write("Please select a chore number to assign a chore: ");
                        int assignedChoreId = int.Parse(Console.ReadLine());

                        List<Roommate> roommateList = roommateRepo.GetAll();
                        foreach (Roommate r in roommateList)
                        {
                            Console.WriteLine($"{r.Id} - {r.FirstName}");
                        }
                        Console.Write("Assign chore to a Roommate: ");
                        int assignedRoommateId = int.Parse(Console.ReadLine());

                        choreRepo.AssignChore(assignedRoommateId, assignedChoreId);

                        Console.WriteLine("Chore has been assigned");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();

                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;

                }

                   
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Show all chores",
                "Show all unassigned chores",
                "Search for room",
                "Search for chore",
                "Search for roommate",
                "Add a room",
                "Add a chore",
                "Assign a chore",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}
