namespace MonopolyField
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        private static char[][] monopolyField;
        private static int rows;
        private static int cols;

        private const char hotel = 'H';
        private const char jail = 'J';
        private const char free = 'F';
        private const char shop = 'S';

        public static void Main()
        {
            int[] dimensionsOfTheField = Console.ReadLine()
                                            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                            .Select(int.Parse)
                                            .ToArray();

            rows = dimensionsOfTheField[0];
            cols = dimensionsOfTheField[1];

            monopolyField = new char[rows][];
            FillTheMonopolyField();

            Player player = new Player();

            string result = IterateThroughTheFieldAndExcecuteTheCommands(player);

            Console.Write(result);
            Console.WriteLine(player.GetPlayerInfo());
        }

        private static string IterateThroughTheFieldAndExcecuteTheCommands(Player player)
        {
            StringBuilder result = new StringBuilder();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int colToCheck = col;
                    if (row % 2 == 1)
                    {
                        colToCheck = cols - 1 - col;
                    }

                    char cellObject = monopolyField[row][colToCheck];

                    switch (cellObject)
                    {
                        case free:
                            //nothing happens
                            break;

                        case hotel:
                            result.AppendLine(player.BuyAHotel());
                            break;

                        case jail:
                            result.AppendLine(player.GoToJail());
                            break;

                        case shop:
                            result.AppendLine(player.GoToShop(row, colToCheck));
                            break;
                            
                        default:
                            throw new ArgumentException("There is no such object in the monopoly field");
                    }

                    player.TakeATurn();
                }
            }

            return result.ToString();
        }

        private static void FillTheMonopolyField()
        {
            for (int row = 0; row < rows; row++)
            {
                string currentMonopolyLine = Console.ReadLine();

                monopolyField[row] = new char[currentMonopolyLine.Length];

                for (int col = 0; col < cols; col++)
                {
                    monopolyField[row][col] = currentMonopolyLine[col];
                }
            }
        }
    }
}
