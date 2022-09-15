

// Some variable initalization
HangmanVisuals Visuals = new(); // Contains stuff to draw the visual hangman
int VisualsBaseRow = 1;
int WordBaseRow = VisualsBaseRow + 7;
int BaseCol = 10;
var file = AppDomain.CurrentDomain.BaseDirectory + "FruitsAndBerries.txt";
string[] Words = File.ReadAllLines(file);
int randomWordIndex = new Random().Next(Words.Length);
string TheWordToGuess = Words[randomWordIndex].ToUpper();
string TheHiddenWord; // The word to guess but only showing correctly guessed letters: "_O_A_O" (A and O has been correct guesses in the word "TOMATO")
List<char> Guesses = new();
int CounterOfWrongGuesses = 0;

// Write the "start screen" to the Console
WriteLine("HANGMAN - Fruits and Berries");
WriteAtPosition(WordBaseRow, 0, "Word:");
WriteAtPosition(WordBaseRow, BaseCol, BuidHiddenWord(TheWordToGuess, Guesses));
WriteAtPosition(WordBaseRow + 1, 0, "Guesses:  ");


do
{
    char Guess;
    do { Guess = ReadKey(true).KeyChar.ToString().ToUpper()[0];} 
    while (Guesses.Contains(Guess) || !char.IsLetter(Guess) ); // Ignoring characters allready guessed (guesses contained in the list of guesses) AND ALSO chars that aren't letters

    if (!TheWordToGuess.Contains(Guess))
        WriteVisualPart(CounterOfWrongGuesses++); // if the guess is wrong (the word to guess doesn't contain the guessed char), draw a bit more of the visuals

    Guesses.Add(Guess);
    TheHiddenWord = BuidHiddenWord(TheWordToGuess, Guesses);
    WriteAtPosition(WordBaseRow, BaseCol, TheHiddenWord);
    WriteAtPosition(WordBaseRow + 1, BaseCol + Guesses.Count -1, Guess.ToString());
} while (CounterOfWrongGuesses < Visuals.PartsCount && TheHiddenWord.Contains("_")); // Loop until the wrong number of guesses = the number of visual parts OR the hidden word doesn't contain any hidden chars

if (CounterOfWrongGuesses < Visuals.PartsCount)
    WriteAtPosition(WordBaseRow + 3, 0, "Great, you got it!\n");
else
    WriteAtPosition(WordBaseRow + 3, 0, $"Sorry, you lost! (the word was {TheWordToGuess})\n");




// Some help methods

void WriteAtPosition(int row, int col, string str)
{
    SetCursorPosition(col, row);
    Write(str);
}

void WriteVisualPart(int part)
{
    foreach (var item in Visuals.GetPart(part)) // a varial part may contain several characters
    {
        SetCursorPosition(item.col + BaseCol, item.row + VisualsBaseRow);
        Write(item.chr);
    }
}

string BuidHiddenWord(string word, List<char> guesses)
{
    StringBuilder sb = new();
    foreach (char c in word) // Loop each char in the word to be guessed
        if (guesses.Contains(c)) // If the list of guesses (guessed chars) contains that char then show it to the user, otherwise keep it hidden
            sb.Append(c);
        else
            sb.Append("_");

    return sb.ToString();
}
