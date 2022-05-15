﻿using System;
namespace CommunityLibrary
{
	public class StaffMenu
	{
		public static void DisplayStaffLogin()
		{
			Console.Clear();
			Console.Write("===================================================================\n" +
				"Johnny n Jamie's\n" +
				"█▀▀ █▀█ █▀▄▀█ █▀▄▀█ █░█ █▄░█ █ ▀█▀ █▄█	█░░ █ █▄▄ █▀█ ▄▀█ █▀█ █▄█\n" +
				"█▄▄ █▄█ █░▀░█ █░▀░█ █▄█ █░▀█ █ ░█░ ░█░	█▄▄ █ █▄█ █▀▄ █▀█ █▀▄ ░█░\n" +
				"\n=============================Staff Menu============================\n");

			bool auth = false;
			string username;
			string password;

			while (!auth)
			{
				Console.Write("\nUsername: ");
				username = Console.ReadLine();
				Console.Write("Password: ");
				password = Console.ReadLine();

				if (username.Equals("staff") && password.Equals("today123")) auth = true;
				else
				{
					Console.WriteLine("\nInvalid credentials...\n" +
					"Enter any key to try again\n" +
					"Enter 0 to return to main menu\n");

					if (Console.ReadLine().Equals("0")) return; // return to MAINMENU
				}
			}

			DisplayStaffMenu();
		}

		public static void DisplayStaffMenu()
        {
			Console.Clear();
			Console.Write("===================================================================\n" +
				"Johnny n Jamie's\n" +
				"█▀▀ █▀█ █▀▄▀█ █▀▄▀█ █░█ █▄░█ █ ▀█▀ █▄█	█░░ █ █▄▄ █▀█ ▄▀█ █▀█ █▄█\n" +
				"█▄▄ █▄█ █░▀░█ █░▀░█ █▄█ █░▀█ █ ░█░ ░█░	█▄▄ █ █▄█ █▀▄ █▀█ █▀▄ ░█░\n" +
				"\n=============================Staff Menu============================\n\n");

			Console.WriteLine("1. Add new DVDs of a new movie to the system");
			Console.WriteLine("2. Remove DVDs of a movie from the system");
			Console.WriteLine("3. Register a new member with the system");
			Console.WriteLine("4. Remove a registered member from the system");
			Console.WriteLine("5. Display a member's contact phone number, given the member's name");
			Console.WriteLine("6. Display all members who are currently renting a particular movie");
			Console.WriteLine("0. Return to the main menu");

			Console.Write("\nEnter your choice ==> (1/2/3/4/5/6/0): ");

			string choice = "";

			while (!choice.Equals("1") || !choice.Equals("2") || !choice.Equals("3") || !choice.Equals("4") ||
				!choice.Equals("5") || !choice.Equals("6") || !choice.Equals("0"))
			{
				choice = Console.ReadLine();

				if (choice.Equals("0")) return; // return to end of DISPLAYSTAFFLOGIN which then returns to MAINMENU
				else Console.Write("Invalid choice, please try again: ");
			}
		}
	}
}
