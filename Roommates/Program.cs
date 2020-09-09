using System;
using System.Collections.Generic;
using Roommates.Models;
using Roommates.Repositories;

namespace Roommates
{
    class Program
    {
        ///  This is the address of the database.
        ///  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            
            // ROOM CALLS
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            
            // GET ALL ROOMS
            Console.WriteLine("Getting All Rooms:");
            Console.WriteLine();

            List<Room> allRooms = roomRepo.GetAll();
            foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }
            
            // GET ROOM BY ID
            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Room with Id 1");
            Room singleRoom = roomRepo.GetById(1);
            Console.WriteLine($"{singleRoom.Id} {singleRoom.Name} {singleRoom.MaxOccupancy}");

            // CREATE NEW ROOM
            Room bathroom = new Room
            {
                Name = "Bathroom",
                MaxOccupancy = 1
            };

            roomRepo.Insert(bathroom);
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Added the new Room with id {bathroom.Id}");

            // UPDATE ROOM
            bathroom.MaxOccupancy = 3;
            roomRepo.Update(bathroom);
            Room bathroomFromDb = roomRepo.GetById(bathroom.Id);
            Console.WriteLine($"{bathroomFromDb.Id} {bathroomFromDb.Name} {bathroomFromDb.MaxOccupancy}");

            //DELETE ROOM
            Console.WriteLine("-------------------------------");
            roomRepo.Delete(bathroom.Id);
            allRooms = roomRepo.GetAll();
            foreach (Room room in allRooms)
            {
                Console.WriteLine($"{room.Id} {room.Name} {room.MaxOccupancy}");
            }


            // ROOMMATE CALLS
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            // GET ALL ROOMMATES
            Console.WriteLine("Getting All Roommates:");
            Console.WriteLine();

            List<Roommate> allRoommates = roommateRepo.GetAll();
            foreach (Roommate roommate in allRoommates)
            {
                Console.WriteLine($"{roommate.Id}. {roommate.FirstName} {roommate.LastName} moved in on {roommate.MoveInDate} and pays {roommate.RentPortion} per month");
            }

            // GET ROOMMATE BY ID
            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Roommate with Id 1");
            Roommate singleRoommate = roommateRepo.GetById(1);
            Console.WriteLine($"{singleRoommate.Id}. {singleRoommate.FirstName} {singleRoommate.LastName}");
            
            // GET ROOMMATE WITH ROOM
            Console.WriteLine("----------------------------");
            Console.WriteLine("Getting Roommates with Room");
            List<Roommate> roommatesRooms = roommateRepo.GetAllWithRoom(5);
            foreach (Roommate roommate in roommatesRooms)
            {
                Console.WriteLine($"{roommate.Id}. {roommate.FirstName} {roommate.LastName} lives in room {roommate.Room.Id}");
            }
            
            // CREATE NEW ROOMMATE
            Roommate rick = new Roommate
            {
                FirstName = "Big",
                LastName = "Rick",
                RentPortion = 10,
                MoveInDate = DateTime.Now,
                Room = roomRepo.GetById(4)
            };

            roommateRepo.Insert(rick);
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Added the new Roommate with id {rick.Id}");
            
            // UPDATE ROOMMATE
            rick.LastName = "Ricky";
            roommateRepo.Update(rick);

            Roommate rickFromDb = roommateRepo.GetById(rick.Id);
            Console.WriteLine($"{rickFromDb.Id}. {rickFromDb.FirstName} {rickFromDb.LastName}");

            //DELETE ROOMMATE
            Console.WriteLine("-------------------------------");
            roommateRepo.Delete(4);

            allRoommates = roommateRepo.GetAll();

            foreach (Roommate roommate in allRoommates)
            {
                Console.WriteLine($"{roommate.Id}. {roommate.FirstName} {roommate.LastName}");
            }
            
        }
    }
}