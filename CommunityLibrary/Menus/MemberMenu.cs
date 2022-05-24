﻿// MEMBER SUB-MENU
// All displays/user-inputs for the sub-menu and each option
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

				// if authenticated, break twice (from both loops) to go to sub-menu, otherwise try again/exit
				bool authenticated = false;
				IMember memberRef = Records.reg.Find(new Member(first, last));

				if (memberRef != null)
                {
					if (memberRef.Pin.Equals(password)) // seperate in-case memberRef is null, cannot access pin property
                    {
						loggedInMember = Records.reg.Find(new Member(first, last));
						authenticated = true;
						break; // first break from this loop
					}
				}
				if (authenticated) break; // second break from bigger loop
				
				Console.Write("\n! Invalid credentials - Enter any key to try again, 0 to cancel: ");
				if (Console.ReadLine().Equals("0")) return; // return to MAIN MENU
			}

			// must be called outside the above loop so when the user wants to navigates back to the MAIN MENU...
			// this method will end and they will be returned to the MAIN MENU's method
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

				if (choice.Equals("1")) DisplayAllMovies();
				else if (choice.Equals("2")) DisplayMovieInfo();
				else if (choice.Equals("3")) BorrowDVD();
				else if (choice.Equals("4")) ReturnDVD();
				else if (choice.Equals("5")) DisplayCurrentBorrowings();
				else if (choice.Equals("6")) TopThree();
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
		// Display all movies in the library for user to browse
		private static void DisplayAllMovies()
		{
			while (true)
			{
				Header();
				Console.WriteLine("DISPLAYING INFO ON ALL MOVIES...\n");
				MemberFunctions.DisplayAllMovies();
				Console.Write($"\nEnter any key to continue: ");
				Console.ReadLine();
				return;
			}
		}



		// OPTION 2
		// Get user input to display all info on a particular movie
		private static void DisplayMovieInfo()
		{
			while (true)
			{
				Header();
				Console.WriteLine("DISPLAYING A MOVIE'S INFO...");

				Console.Write("Movie title: ");
				string t = Console.ReadLine();

				try // TRY TO DISPLAY ALL INFO FOR THIS MOVIE
				{
					Console.WriteLine("");
					MemberFunctions.DisplayMovieInfo(t);
					Console.Write($"\nEnter any key to continue: ");
					Console.ReadLine();
					return;
				}
				catch (CustomException x)
				{
					Console.Write($"! {x.Message} - Enter any key to try again, 0 to cancel: ");
					if (Console.ReadLine().Equals("0")) return;
				}
			}
		}



		// OPTION 3
		// Get user input to borrow a movie tied to this logged-in registered member
		private static void BorrowDVD()
		{
			while (true)
			{
				Header();
				Console.WriteLine("BORROW A MOVIE DVD...");
				Console.Write("Movie title: ");
				IMovie movie = new Movie(Console.ReadLine()); // Get user input

				// Boilerplate to confirm action
				Console.Write($"\nEnter any key to borrow ({movie.Title}), 0 to cancel: ");
				if (Console.ReadLine().Equals("0")) return;

				try // TRY TO BORROW MOVIE
				{
					if (!MemberFunctions.BorrowDVD(movie, loggedInMember)) Console.Write($"\n! At max borrowers for this movie - Enter any key to continue: ");
					else Console.Write($"\nBorrowed ({movie.Title}) - Enter any key to continue: ");

					Console.ReadLine();
					return;
				}
				catch (CustomException x)
				{
					Console.Write($"\n! {x.Message} - Enter any key to try again, 0 to cancel: ");
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
				Console.WriteLine("RETURN A MOVIE DVD...");
				Console.Write("Movie title: ");
				IMovie movie = new Movie(Console.ReadLine()); // Get user input

				// Boilerplate to confirm action
				Console.Write($"\nEnter any key to return ({movie.Title}), 0 to cancel: ");
				if (Console.ReadLine().Equals("0")) return;

				try // TRY TO RETURN MOVIE
				{
					if (!MemberFunctions.ReturnDVD(movie, loggedInMember)) Console.Write($"\n! Not currently borrowing ({movie.Title}) - Enter any key to continue: ");
					else Console.Write($"\nReturned ({movie.Title}) - Enter any key to continue: ");

					Console.ReadLine();
					return;
				}
				catch (CustomException x)
				{
					Console.Write($"\n! {x.Message} - Enter any key to try again, 0 to cancel: ");
					if (Console.ReadLine().Equals("0")) return;
				}
			}
		}



		// OPTION 5
		// Display all movies this logged in user is currently borrowing
		private static void DisplayCurrentBorrowings()
		{
			while (true)
			{
				Header();
				Console.WriteLine("DISPLAYING YOUR CURRENT BORROWINGS...\n");
				MemberFunctions.DisplayCurrentBorrowings(loggedInMember);
				Console.Write($"\nEnter any key to continue: ");
				Console.ReadLine();
				return;
			}
		}



		// OPTION 6
		// Display the top 3 movies rented by other members
		private static void TopThree()
		{
			while (true)
			{
				Header();
				Console.WriteLine("DISPLAYING TOP 3 MOVIES...\n");
				MemberFunctions.TopThree();
				Console.Write("\nEnter any key to continue: ");
				Console.ReadLine();
				return;
			}
		}
	}
}

