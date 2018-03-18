using Lab04Magur.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Lab04Magur
{
    internal class DBAdapter
    {
        internal static List<Person> People { get; set; }

        private static string filename = "Users.dat";
        private static string peopleDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "People");

        private static Random rd = new Random();

        static DBAdapter()
        {
            var filepath = Path.Combine(GetAndCreateDataPath(), filename);
            if (File.Exists(filepath))
            {
                try
                {
                    People = SerializeHelper.Deserialize<List<Person>>(filepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to get users from file. {Environment.NewLine}{ex.Message}" + peopleDirectory);
                    throw;
                }
            }
            else
            {
                People = new List<Person>();
                FillPeopleList();
            }
        }

        internal static Person AddPerson(Person newPerson)
        {
            People.Add(newPerson);
            return newPerson;
        }

        internal static void DeletePerson(Person personToDelete)
        {
            People.RemoveAll(person => person.FirstName == personToDelete.FirstName && person.LastName == personToDelete.LastName &&
            person.Email == personToDelete.Email && person.DateOfBirth.Equals(personToDelete.DateOfBirth));
        }

        internal static void SaveData()
        {
            SerializeHelper.Serialize(People, Path.Combine(GetAndCreateDataPath(), filename));
        }

        private static void FillPeopleList()
        {
            string firstName = String.Empty;
            string lastName = String.Empty;
            DateTime start = new DateTime(DateTime.Today.Year - 135, DateTime.Today.Month, DateTime.Today.Day);
            int range = (DateTime.Today - start).Days;

            for (int i = 0; i < 50; i++)
            {
                firstName = Person.AvailableNames[rd.Next(0, Person.AvailableNames.Length)];
                lastName = Person.AvailableLastNames[rd.Next(0, Person.AvailableLastNames.Length)];

                People.Add(new Person(firstName, lastName, GenerateEmail(firstName, lastName), start.AddDays(rd.Next(range))));
            }
        }

        private static string GenerateEmail(string firstName, string lastName)
        {
            return firstName + "." + lastName + "@gmail.com";
        }

        private static string GetAndCreateDataPath()
        {
            string dir = peopleDirectory;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }
}
