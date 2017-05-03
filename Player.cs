namespace MonopolyField
{
    using System;
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
