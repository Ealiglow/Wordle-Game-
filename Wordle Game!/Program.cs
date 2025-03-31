
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Xml.Linq;
int left = 10; //position to the left 
int top = 17; //position from top so it doesnt break what is currently written
int number= 0; //the number of chances that it should dislay in text
char userinput; //checks what level you wanna play
bool game= true; //the bool that holds the game
char restart; //to take in if you wanna restart
bool won = false; //to stop the guessing if you win and let you know
int gamesWon = 0; //checks how many games you have won to display
Random rnd = new();
string[] words = new string[] { "HELLO","SKIBIDI", "SIGMA", "ALPHA", "INDEX" };//Words are all that computer can choose from
int[] chances= new int[] {4,7,10};//Chances are how many rows exists, incase there will be different dificulties
List<char> lettersGuessed= new List<char>(); //list of all letters you guess echa round
int maxAttempts= chances[1]; //checks how many # it should write acording to chances
while (game == true)
{
    int windex = rnd.Next(words.Length); //randomises the word you get
    Console.WriteLine("--------------------------------------------------------");
    Console.Write("\x1b[38;2;255;105;180m");
    Console.WriteLine("Wordle");
    Console.Write("\x1b[0m");
    Console.ResetColor();
    Console.WriteLine("");
    Console.WriteLine("__________________________");
    //Console.BackgroundColor = ConsoleColor.DarkMagenta;
    // Console.ForegroundColor = ConsoleColor.Magenta;
    Console.Write("\x1b[48;2;94;0;52m"); //Background hex; 48;2;R;G;Bm
    Console.Write("\x1b[38;2;255;105;180m"); //Foreground hex; 38;2;R;G;Bm
    Console.WriteLine("Games won;");
    Console.WriteLine($"{gamesWon}");
    Console.Write("\x1b[0m"); //Resets colour back
    Console.WriteLine("__________________________");
    Console.WriteLine("");
    Console.WriteLine("Colour guide;");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write($"Green");
    Console.ResetColor();
    Console.Write(" = right letter on right place");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("");
    Console.Write("yellow");
    Console.ResetColor();
    Console.Write("= right letter but wrong placement");
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine("");
    Console.Write("Red");
    Console.ResetColor();
    Console.Write("= The letter isnt a part of the word");
    Console.WriteLine("");
    Console.WriteLine("Difficulties;");
    
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Easy[E]");
    Console.ResetColor();
    
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Medium[M]");
    Console.ResetColor();
   
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.Write("Hard[H]");
    Console.ResetColor();
   
    Console.WriteLine("");
    userinput = char.ToLower(Console.ReadKey().KeyChar);
    Console.WriteLine("");
    //checks and switches which difficulty you wanna play
    if (userinput == 'h')
    {
        maxAttempts = chances[0];
        number = maxAttempts;
    }
    else if (userinput == 'm')
    {
        maxAttempts = chances[1];
        number = maxAttempts;

    }
    else if (userinput == 'e')
    {
        maxAttempts = chances[2];
        number = maxAttempts;

    }
    else
    {
        Console.WriteLine("Please enter a valid letter");
        userinput = char.ToLower(Console.ReadKey().KeyChar);
    }

    Console.WriteLine($"you have {number} chances right now, if you guess the right word then you win and if you do not then you loose");
    Console.WriteLine("");
    string[] guesses = new string[maxAttempts];
    //Adds the right amount of # into an array and then transforms it into a string
    for (int i = 0; i < guesses.Length; i++)
    {
        guesses[i] = new string('#', words[windex].Length);

    }
  //positions the coursor right and writes up everything from the string above 
    for (int i = 0; i < guesses.Length; i++)
    {

        Console.SetCursorPosition(left, top + i);
        Console.Write(guesses[i]);
    }
    Console.WriteLine();
    Console.WriteLine("");
 
    Console.WriteLine("--------------------------------------------------------");
    
    //all mechanics in the game
    for (int attempt = 0; attempt < maxAttempts; attempt++)
    {

        string wordGuessed = "";

        do
        {

            {
                //changes coursor position everytime you guess
                Console.SetCursorPosition(left + wordGuessed.Length, top + attempt); 
                //adds all the letters as chars into a list
                char newChar = char.ToUpper(Console.ReadKey().KeyChar);
                if (!newChar.Equals(lettersGuessed))
                {
                    lettersGuessed.Add(newChar);
                }
                //adds all leters from wordsGuessed int lettersGuessed
                wordGuessed = string.Concat(lettersGuessed);
            }

        }
        //limits how much you can write on one line
        while (wordGuessed.Length != words[windex].Length);

        guesses[attempt] = wordGuessed;
        //fixes with the letters colours so that it changes acordingly
        for (int i = 0; i < lettersGuessed.Count; i++)
        {
            Console.SetCursorPosition(left + i, top + attempt);
            if (lettersGuessed[i] == words[windex][i])
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (words[windex].Contains(lettersGuessed[i]))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.WriteLine(lettersGuessed[i]);
        }
        //clears everything so that the next line wont have a colour or keep lettersGueesed filled
        Console.ResetColor();
        lettersGuessed.Clear();

        //checks if youve guessed the correct word
        if (wordGuessed == words[windex])
        {
            won = true;
        }
        //stops it from countinuing to loop after youve guessed correctly
        if (won == true)
        {
            break;
        }
    }

    Console.ResetColor(); //removes colour
    Console.SetCursorPosition(0, 26); //fixes coursor position for where we want end text to go
    //lets you know if you have won or lost
   
    if (won == true)
    {
        Console.WriteLine($"congrats! you guessed corectly, the word was {words[windex]}");
        gamesWon = gamesWon + 1;
    }
    else
    {
        Console.WriteLine($"Game Over! The correct word was;{words[windex]}");

    }
    Console.WriteLine("");
    Console.WriteLine("would you like to restart?");
    Console.WriteLine("Yes[1]          No[2]");
    Console.WriteLine("");
    restart = char.ToLower(Console.ReadKey().KeyChar);
    //it lets you restart the game but keeps score or let you exit it properly
    if (restart == '1')
    {
        won = false;
        game = true;
        Console.Clear();
        lettersGuessed = new List<char>();
    }
    else if (restart == '2')
    {
        game = false;
    }
    Console.WriteLine("");
}
Console.WriteLine("ending game now");
Console.WriteLine("");
Console.WriteLine("");
