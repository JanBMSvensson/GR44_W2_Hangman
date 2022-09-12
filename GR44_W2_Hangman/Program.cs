
HangmanVisuals Visuals = new();

int VisualsBaseRow = 1;
int WordBaseRow = VisualsBaseRow + 7;
int BaseCol = 10;
var file = AppDomain.CurrentDomain.BaseDirectory + "FruitsAndBerries.txt";
string[] Words = File.ReadAllLines(file);
int randomWorkIndex = new Random().Next(Words.Length);
string TheWord = Words[randomWorkIndex].ToUpper();
List<char> Guesses = new();

WriteLine("HANGMAN - Fruits and Berries");
WriteAtPosition(WordBaseRow, 0, "Word:");
WriteAtPosition(WordBaseRow, BaseCol, GetHiddenWord(TheWord, Guesses));
WriteAtPosition(WordBaseRow + 1, 0, "Guesses:  ");

int Counter = 0;
string HiddenWord;

do
{
    char Guess;
    do { Guess = ReadKey(true).KeyChar.ToString().ToUpper()[0];} 
    while (Guesses.Contains(Guess));

    if (!TheWord.Contains(Guess))
        WriteVisualPart(Counter++);

    Guesses.Add(Guess);
    HiddenWord = GetHiddenWord(TheWord, Guesses);
    WriteAtPosition(WordBaseRow, BaseCol, HiddenWord);
    WriteAtPosition(WordBaseRow + 1, BaseCol + Guesses.Count -1, Guess.ToString());
} while (Counter < Visuals.PartsCount && HiddenWord.Contains("_"));

if (Counter < Visuals.PartsCount)
    WriteAtPosition(WordBaseRow + 3, 0, "Great, you got it!\n");
else
    WriteAtPosition(WordBaseRow + 3, 0, $"Sorry, you lost! (the word was {TheWord})\n");





void WriteAtPosition(int row, int col, string str)
{
    SetCursorPosition(col, row);
    Write(str);
}

void WriteVisualPart(int part)
{
    foreach (var item in Visuals.GetPart(part))
    {
        SetCursorPosition(item.col + BaseCol, item.row + VisualsBaseRow);
        Write(item.chr);
    }
}

string GetHiddenWord(string word, List<char> guesses)
{
    StringBuilder sb = new();
    foreach (char c in word)
        if (guesses.Contains(c))
            sb.Append(c);
        else
            sb.Append("_");

    return sb.ToString();
}
