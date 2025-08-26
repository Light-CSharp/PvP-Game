namespace PvP_Game
{
    public static class BattleManager
    {
        public enum Actions
        {
            Attack = 1,
            TryCriticalAttack,
            Heal
        }

        private static Player currentPlayer = default!;
        private static Player opponent = default!;

        private static int moveCount;
        private static Actions action;

        public static void StartBattle(Player firstPlayer, Player secondPlayer)
        {
            currentPlayer = firstPlayer;
            opponent = secondPlayer;
            moveCount = 1;

            while (firstPlayer.IsAlive && secondPlayer.IsAlive)
            {
                GetPlayersStatistics(firstPlayer, secondPlayer);

                currentPlayer.OnTurnStart();
                action = currentPlayer.ActionMenu();

                Console.Clear();
                switch (action)
                {
                    case Actions.Attack:
                        currentPlayer.Attack(opponent);

                        Logger.AddAction(moveCount, "Атака", currentPlayer, opponent);
                        break;

                    case Actions.TryCriticalAttack:
                        currentPlayer.TryCriticalAttack(opponent);

                        Logger.AddAction(moveCount, "Попытаться нанести критический удар", currentPlayer, opponent);
                        break;

                    case Actions.Heal:
                        currentPlayer.Heal();

                        Logger.AddAction(moveCount, "Лечение", currentPlayer, opponent);
                        break;
                }
                Console.WriteLine();

                ChangePlayers();
                moveCount += 1;
            }

            Console.Clear();
            GetWinner(firstPlayer, secondPlayer);
        }

        private static void GetPlayersStatistics(Player firstPlayer, Player secondPlayer)
        {
            MessageAssistant.WriteCentered("Статистика игроков", 35);
            Console.WriteLine("===================================");

            firstPlayer.GetInfo();
            Console.WriteLine("===================================");

            secondPlayer.GetInfo();
            Console.WriteLine("===================================");
            Console.WriteLine();
        }

        private static void ChangePlayers() => (currentPlayer, opponent) = (opponent, currentPlayer);

        private static void GetWinner(Player firstPlayer, Player secondPlayer)
        {
            if (!firstPlayer.IsAlive)
            {
                MessageAssistant.BlueMessage($"{firstPlayer.Name} умер, победитель: {secondPlayer.Name}.");
                MessageAssistant.BlueMessage("Статистика победителя: ");
                secondPlayer.GetInfo();
            }
            else
            {
                MessageAssistant.BlueMessage($"{secondPlayer.Name} умер, победитель: {firstPlayer.Name}.");
                MessageAssistant.BlueMessage("Статистика победителя: ");
                firstPlayer.GetInfo();
            }

            Console.WriteLine();
            MessageAssistant.BlueMessage("Лог игры: ");
            Logger.GetLog();
        }
    }
}