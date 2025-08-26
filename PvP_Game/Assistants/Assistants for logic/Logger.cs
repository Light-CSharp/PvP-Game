namespace PvP_Game
{
    public static class Logger
    {
        private static readonly List<string> moves = [];

        public static void AddAction(int moveCount, string action, Player currentPlayer, Player opponent)
        {
            moves.Add($"Ход {moveCount}: {action}.\n" +
                      $"Игрок: {FormatPlayer(currentPlayer)}.\n" +
                      $"Противник: {FormatPlayer(opponent)}.\n");
        }

        private static string FormatPlayer(Player player)
        {
            return $"Имя: {player.Name}, " +
                   $"Класс: {player.ClassName}, " +
                   $"Здоровье: {player.HealthPoints:F2}, " +
                   $"Защита: {player.TotalDefense:F2}, " +
                   $"Крит. шанс: {player.TotalCriticalChance}, " +
                   $"Крит. урон: {player.CriticalDamage:F2}";
        }

        public static void GetLog()
        {
            Console.WriteLine("======== Журнал боя ========");
            foreach (string move in moves)
            {
                Console.WriteLine(move);
            }
        }
    }
}