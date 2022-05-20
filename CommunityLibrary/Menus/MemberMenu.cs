﻿// MEMBER SUB-MENU
// All display/user-inputs for the sub-menu and each option
// Options utilise corresponding functions (see MemberFunctions)
using System;

namespace CommunityLibrary
{
	public class MemberMenu
	{
		// STORE LOGGED IN MEMBER FOR SESSION ONLY (set to null when exiting with "0" from menu)
		public static IMember loggedInMember;



		// HEADER
		// Reproducable header (clears console)
		private static void Header()
        {
			Console.Clear();
			Console.Write("===================================================================\n" +
				"Johnny n Jamie's\n" +
				"█▀▀ █▀█ █▀▄▀█ █▀▄▀█ █░█ █▄░█ █ ▀█▀ █▄█	█░░ █ █▄▄ █▀█ ▄▀█ █▀█ █▄█\n" +
				"█▄▄ █▄█ █░▀░█ █░▀░█ █▄█ █░▀█ █ ░█░ ░█░	█▄▄ █ █▄█ █▀▄ █▀█ █▀▄ ░█░\n" +
				"\n=============================Member Menu===========================\n\n");
		}



		// OPTIONS
		// Reproducable options
		private static void Options()
        {
			Console.WriteLine("1. Browse all the movies");
			Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
			Console.WriteLine("3. Borrow a movie DVD");
			Console.WriteLine("4. Return a movie DVD");
			Console.WriteLine("5. List current borrowing movies");
			Console.WriteLine("6. Display the top 3 movies rented by the members");
			Console.WriteLine("0. Return to the main menu");

			Console.Write("\nEnter your choice ==> (1/2/3/4/5/6/0): ");
		}



		// LOGIN
		// Display member login page to get user input and verify credentials
		public static void DisplayMemberLogin()
		{
			while (true)
			{
				Header();
				Console.WriteLine("Please login with a registered member account...");

				Console.Write("\nFirst name: ");
				string first = Console.ReadLine();
				Console.Write("Last name: ");
				string last = Console.ReadLine();
				Console.Write("Password (pin): ");
				string password = Console.ReadLine();

				// if authenticated, break from loop to go to sub-menu, otherwise try again/exit
				bool authenticated = false;
				foreach (IMember member in Records.reg)
				{
					if (member.FirstName == first && member.LastName == last && member.Pin == password)
					{
						loggedInMember = member;
						authenticated = true;
						break;
					}
				}
				if (authenticated) break;
				Console.Write("\nInvalid credentials...\nEnter any key to try again, enter 0 to return to main menu: ");
				if (Console.ReadLine().Equals("0")) return; // return to MAIN MENU
			}
			DisplayMemberMenu();
		}



		// SUB-MENU
		// Display member sub-menu and await user's choice
		private static void DisplayMemberMenu()
        {
			Header();
			Options();

			// loop so the user can select options...
			while (true)
			{
				string choice = Console.ReadLine();

				if (choice.Equals("1")) /* todo */;
				else if (choice.Equals("2")) /* todo */;
				else if (choice.Equals("3")) BorrowDVD();
				else if (choice.Equals("4")) ReturnDVD();
				else if (choice.Equals("5")) /* todo */;
				else if (choice.Equals("6")) /* todo */;
				else if (choice.Equals("0"))
				{
					loggedInMember = null;
					return; // return to end of DISPLAY MEMBER LOGIN which then returns to MAIN MENU
				}

				Header();
				Options();

				if (!choice.Equals("1") && !choice.Equals("2") && !choice.Equals("3") && !choice.Equals("4") &&
					!choice.Equals("5") && !choice.Equals("6")) Console.Write("Invalid choice, please try again: ");

			}
		}



		// OPTION 1
		// todo



		// OPTION 2
		// todo
		


		// OPTION 3
		// Get user input to borrow a movie tied to this logged-in registered member
		private static void BorrowDVD()
		{
			while (true)
			{
				Header();
				Console.WriteLine("BORROWING MOVIE DVD...");
				Console.Write("Movie title: ");
				IMovie movie = new Movie(Console.ReadLine()); // Get user input

				// Boilerplate to confirm action
				Console.Write($"\nEnter any key to borrow ({movie.Title}), 0 to cancel: ");
				if (Console.ReadLine().Equals("0")) return;

				try // TRY TO BORROW MOVIE
				{
					if (!MemberFunctions.BorrowDVD(movie, loggedInMember)) Console.Write($"\nFailed - At max borrowers for this movie, enter any key to continue: ");
					else Console.Write($"\nBorrowed ({movie.Title}), enter any key to continue: ");

					Console.ReadLine();
					return;
				}
				catch (CustomException x)
				{
					Console.Write($"\nFailed - {x.Message}, enter any key to try again, 0 to cancel: ");
					if (Console.ReadLine().Equals("0")) return;
				}
			}
		}



		// OPTION 4
		// Get user input to return a movie tied to this logged-in registered member
		private static void ReturnDVD()
		{
			while (true)
			{
				Header();
				Console.WriteLine("RETURNING MOVIE DVD...");
				Console.Write("Movie title: ");
				IMovie movie = new Movie(Console.ReadLine()); // Get user input

				// Boilerplate to confirm action
				Console.Write($"\nEnter any key to borrow ({movie.Title}), 0 to cancel: ");
				if (Console.ReadLine().Equals("0")) return;

				try // TRY TO RETURN MOVIE
				{
					if (!MemberFunctions.ReturnDVD(movie, loggedInMember)) Console.Write($"\nNot currently borrowing ({movie.Title}), enter any key to continue: ");
					else Console.Write($"\nReturned ({movie.Title}), enter any key to continue: ");

					Console.ReadLine();
					return;
				}
				catch (CustomException x)
				{
					Console.Write($"\nFailed - {x.Message}, enter any key to try again, 0 to cancel: ");
					if (Console.ReadLine().Equals("0")) return;
				}
			}
		}



		// OPTION 5
		// todo



		// OPTION 6
		// todo
	}
}

