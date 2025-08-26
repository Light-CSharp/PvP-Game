namespace PvP_Game.App
{
    public enum ClassSelection
    {
        Mage = 1,
        Swordsman,
        Spearman
    }

    public class Game
    {
        static Player firstPlayer = default!;
        static Player secondPlayer = default!;

        static void GetPlayer(ref Player player, byte position)
        {
            Console.Write($"Введите имя для {position} игрока: ");

            bool isCorrect;
            string name;
            do
            {
                name = Console.ReadLine()!.Trim();
                isCorrect = !string.IsNullOrEmpty(name);
                if (!isCorrect)
                {
                    MessageAssistant.RedMessage("Неверное имя, попробуйте ещё раз: ");
                }     
            } while (!isCorrect);

            GetClass(ref player, name);
        }

        static void GetClass(ref Player player, string name)
        {
            Console.WriteLine("Выберите игровой класс: ");

            Console.WriteLine("1. Маг");
            Console.WriteLine("(300 хп, -65 защиты, урон +50%)");
            Console.WriteLine("(нет лечения, как и критических атак, лечение на 1/10 нынешнего здоровья каждые 2 атаки)");

            Console.WriteLine("2. Мечник");
            Console.WriteLine("(500 хп, 50 защиты, урон +10%. 3 лечения (1/7 нынешнего здоровья), критический шанс 20%)");
            Console.WriteLine("(+2 защиты после хода любого игрока)");

            Console.WriteLine("3. Копейщик");
            Console.WriteLine("(450хп, 30 защиты, урон +25%. 2 лечения (1/5 нынешнего здоровья), критический шанс 35%)");
            Console.WriteLine("(при удачном критическом ударе производится доп. удар, который в 2 раза слабее обычной атаки)");

            bool isCorrect;
            byte classChoice;
            do
            {
                isCorrect = byte.TryParse(Console.ReadLine(), out classChoice) && classChoice >= 1 && classChoice <= 3;
                if (!isCorrect)
                {
                    MessageAssistant.RedMessage("Неверное значение, попробуйте снова: ");
                }
            } while (!isCorrect);

            switch ((ClassSelection)classChoice)
            {
                case ClassSelection.Mage:
                    player = new Mage(name);
                    break;

                case ClassSelection.Swordsman:
                    player = new Swordsman(name);
                    break;

                case ClassSelection.Spearman:
                    player = new Spearman(name);
                    break;
            }

            Console.Clear();

            GetWeapon(player);
        }

        static void GetWeapon(Player player)
        {
            Console.WriteLine("Выберите оружие: ");
            player.ShowAvailableWeapons();

            bool isCorrect;
            byte option;
            do
            {
                isCorrect = byte.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= 2;
                if (!isCorrect)
                {
                    MessageAssistant.RedMessage("Неверное значение, попробуйте ещё раз: ");
                }
            } while (!isCorrect);

            player.TakeWeapon(option);

            Console.Clear();
        }

        static void Instruction()
        {
            MessageAssistant.WriteCentered("Общие сведения", 135);
            MessageAssistant.BlueMessage("1. Все отрицательные и положительные эффекты действуют только на игрока, оружие не будет изменяться!");
            MessageAssistant.BlueMessage("2. Игра продолжается, пока один из игроков не умрёт!");
            MessageAssistant.BlueMessage("3.1. Критический удар = урон обычного удар * 2.5. Шанс срабатывания от 40% критического шанса.");
            MessageAssistant.BlueMessage("3.2. Гипер-критический удар = урон обычного удара * урон обычного удара. Шанс срабатывания: 50% и каждые 3 успешных критических удара.");
            Console.WriteLine();
        }

        static void Main()
        {
            Instruction();

            GetPlayer(ref firstPlayer, 1);
            GetPlayer(ref secondPlayer, 2);

            BattleManager.StartBattle(firstPlayer, secondPlayer);    
        }
    }
}