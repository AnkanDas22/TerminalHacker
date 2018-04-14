using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
    string[] level1passwords = { "books","aisle","shelf","borrow","silence","librarian" };
    string[] level2passwords = { "jail", "bail", "cops", "framed", "hellinacell","holster" };
    string[] level3passwords = { "america", "probe", "moonlanding", "astronaut", "nocosmonaut" };

    int level;
    enum Screen {MainMenu, Password, Win};
    Screen currentScreen = Screen.MainMenu;
    string password;
    int score = 0;
    bool q1 = false, q2 = false, q3 = false, msgdisp = false;

	void Start () {
        ShowMainMenu();
	}

    void OnUserInput(string input)
    {
        if (input.ToUpper() == "MENU")
        {
            currentScreen = Screen.MainMenu;
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
        else if (currentScreen == Screen.Win)
        {
            currentScreen = Screen.MainMenu;
            ShowMainMenu();
        }

    }

    void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("No easter eggs here, sorry");
            ShowMainMenu();
        }
        else
        {
            Terminal.WriteLine("Enter a valid level number");
        }
    }

    void ShowMainMenu()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("What do you want to hack?\nPress 1 for the local library\nPress 2 for the police station\nPress 3 for NASA\nEnter your choice:");
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Enter password, hint: " + password.Anagram());
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1passwords[Random.Range(0, level1passwords.Length)];
                break;
            case 2:
                password = level2passwords[Random.Range(0, level2passwords.Length)];
                break;
            case 3:
                password = level3passwords[Random.Range(0, level3passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid Level");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Sorry, wrong password");
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                if (!q1)
                {
                    score++;
                    q1 = true;
                }
                Terminal.WriteLine("You have found the book...\n");
                Terminal.WriteLine("    /      //\n   /      //\n  /______//\n (______(/");
                break;
            case 2:
                if (!q2)
                {
                    score++;
                    q2 = true;
                }
                Terminal.WriteLine("You avoided an honest conversation and found the prison key...\n");
                Terminal.WriteLine(@"
    ____
   /    \_________
   \____/~~~~~||||");
                break;
            case 3:
                if (!q3)
                {
                    score++;
                    q3 = true;
                }
                Terminal.WriteLine("Now you can threaten NASA, sitting in the comfort of your Russian home..\n");
                Terminal.WriteLine(@"
| '_ \ / _` / __|/ _` |
| | | | (_| \__ \ (_| |
|_| |_|\__,_|___/\__,_|");
                break;
            default:
                Terminal.WriteLine("Nice try breaching into the game...");
                break;
        }
        if (score == 3 && !msgdisp)
        {
            msgdisp = true;
            Terminal.WriteLine("\nYou have successfully completed the game! You can keep playing to your heart's content though!");
        }
    }
}