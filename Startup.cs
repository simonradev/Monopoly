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

    public class Player
    {
        private int totalMoney;
        private int moneyIncomePerTurn;
        private int turnsTook;

        public Player()
        {
            this.totalMoney = 50;
            this.moneyIncomePerTurn = 0;
            this.turnsTook = 0;
        }

        public void TakeATurn()
        {
            this.turnsTook++;

            this.totalMoney += moneyIncomePerTurn;
        }

        public string BuyAHotel()
        {
            this.moneyIncomePerTurn += 10;

            string messageResult = $"Bought a hotel for {this.totalMoney}. Total hotels: {this.moneyIncomePerTurn / 10}.";

            this.totalMoney = 0;

            return messageResult;
        }

        public string GoToJail()
        {
            this.TakeATurn();
            this.TakeATurn();

            return $"Gone to jail at turn {this.turnsTook - 2}.";
        }

        public string GoToShop(int row, int col)
        {
            row++;
            col++;

            int moneyToSpend = this.totalMoney >= row * col ? row * col : this.totalMoney;

            this.totalMoney -= moneyToSpend;

            return $"Spent {moneyToSpend} money at the shop.";
        }

        public string GetPlayerInfo()
        {
            return $"Turns {this.turnsTook}{Environment.NewLine}Money {this.totalMoney}";
        }
    }
}
