
using System.Text;

namespace GR44_W2_Hangman
{
    internal class HangmanVisuals
    {
        /*
            0   __
            1  |  |
            2  |  O
            3  | /|\
            4  | / \
            5  |
            6----------
             0123456789
        */

        char[,] Visuals;
        int[][,] VisualParts;

        public HangmanVisuals()
        {
            Visuals = new char[7, 10]; // 7 rows, 10 columns

            for (int row = 0; row <= Visuals.GetUpperBound(0); row++)
                for (int col = 0; col <= Visuals.GetUpperBound(1); col++)
                    Visuals[row, col] = ' ';

            InitRow(0, "   __");
            InitRow(1, "  |  |");
            InitRow(2, "  |  O");
            InitRow(3, "  | /|\\");
            InitRow(4, "  | / \\");
            InitRow(5, "  |");
            InitRow(6, "----------");

            VisualParts = new int[10][,];

            VisualParts[0] = new int[,] { { 6, 0 }, { 6, 1 }, { 6, 2 }, { 6, 3 }, { 6, 4 },
                                          { 6, 5 }, { 6, 6 }, { 6, 7 }, { 6, 8 }, { 6, 9 } };
            VisualParts[1] = new int[,] { { 5, 2 }, { 4, 2 }, { 3, 2 }, { 2, 2 }, { 1, 2 } };
            VisualParts[2] = new int[,] { { 0, 3 }, { 0, 4 } };
            VisualParts[3] = new int[,] { { 1, 5 } };
            VisualParts[4] = new int[,] { { 2, 5 } };
            VisualParts[5] = new int[,] { { 3, 5 } };
            VisualParts[6] = new int[,] { { 3, 4 } };
            VisualParts[7] = new int[,] { { 3, 6 } };
            VisualParts[8] = new int[,] { { 4, 4 } };
            VisualParts[9] = new int[,] { { 4, 6 } };
        }

        public int PartsCount { get { return VisualParts.Length; } }

        public (int row, int col, char chr)[] GetPart(int part)
        {
            List<(int row, int col, char chr)> Ut = new();

            if (part > VisualParts.GetUpperBound(0))
                throw new ArgumentException("Too high part-arg");

            int[,] theParts = VisualParts[part];

            for (int i = 0; i <= theParts.GetUpperBound(0); i++)
            {
                var row = theParts[i, 0];
                var col = theParts[i, 1];

                Ut.Add((row, col, Visuals[row,col]));
            }

            return Ut.ToArray();
        }

        private void InitRow(int row, string rowString)
        {
            if (row -1 > Visuals.GetUpperBound(0))
                throw new ArgumentException("Too high row number");

            if (rowString.Length -1 > Visuals.GetUpperBound(1))
                throw new ArgumentException("Too many chars in row");

            for (int index = 0; index < rowString.Length; index++)
                if (rowString[index] != ' ')
                    Visuals[row, index] = rowString[index];
        }

    }
}
