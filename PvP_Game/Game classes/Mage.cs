namespace PvP_Game
{
    public class Mage : Player
    {
        private double numberOfAttacks;

        public Mage(string name) : base(name)
        {
            className = CharacterConstants.Mage.CLASS_NAME;

            healthPoints = CharacterConstants.Mage.HEALTH_POINTS;
            healingPower = CharacterConstants.Mage.HEALING_POWER;

            defense = CharacterConstants.Mage.DEFENSE;
            damageBonus = CharacterConstants.Mage.DAMAGE_BONUS;
        }

        public override void ShowAvailableWeapons()
        {
            Console.WriteLine("1. Огненный шар");
            Console.WriteLine($"(урон {MageWeaponsConstants.Fireball.DAMAGE})");
            Console.WriteLine($"(каждый удар пробивает {MageWeaponsConstants.Fireball.REDUCED_PROTECTION * 100} защиты противника, но атака мага становится слабее на {MageWeaponsConstants.Fireball.DAMAGE_REDUCTION * 100}% после атаки)");

            Console.WriteLine($"2. Удар молнии");
            Console.WriteLine($"(урон {MageWeaponsConstants.LightningStrike.DAMAGE})");
            Console.WriteLine($"(каждый удар уменьшает защиту противника на {MageWeaponsConstants.LightningStrike.REDUCED_PROTECTION * 100}%, но первый атака требует {MageWeaponsConstants.LightningStrike.QUANTITY_FOR_FIRST_ATTACK} заряда, а последующие: {MageWeaponsConstants.LightningStrike.QUANTITY_FOR_FOLLOWING_ATTACKS} заряда)");
            Console.WriteLine("(Заряд = попытка атаки)");
        }

        public override void TakeWeapon(int option)
        {
            switch ((MageWeapons)option)
            {
                case MageWeapons.Fireball:
                    weapon = new Fireball(MageWeaponsConstants.Fireball.NAME, this);
                    break;

                case MageWeapons.LightningStrike:
                    weapon = new LightningStrike(MageWeaponsConstants.LightningStrike.NAME, this);
                    break;
            }
        }

        public override void GetInfo()
        {
            MessageAssistant.BlueMessage($"Имя: {Name}, Класс: {ClassName}, Статус: {(IsAlive ? "Жив" : "Мёртв")}.");
            MessageAssistant.BlueMessage($"Количество здоровья: {HealthPoints:F2}.");
            MessageAssistant.BlueMessage($"Защита: {TotalDefense:F2}, Здоровье с учётом защиты: {HealthWithProtection:F2}.");
        }

        public override BattleManager.Actions ActionMenu()
        {
            Console.WriteLine($"{Name}, выберите действие: ");
            Console.WriteLine("1. Атаковать.");

            bool isCorrect;
            int option;
            do
            {
                isCorrect = int.TryParse(Console.ReadLine(), out option) && option == 1;
                if (!isCorrect)
                {
                    MessageAssistant.RedMessage("Неверно введённое действие, попробуйте ещё раз: ");
                }
            } while (!isCorrect);

            return (BattleManager.Actions)option;
        }

        public override void Attack(Player target)
        {
            double beforeHealthPoints = target.HealthPoints;
            base.Attack(target);

            // Проверка на то, что удар прошёл, нужна для Lightning Strike, которая имеет заряды.
            if (target.HealthPoints == beforeHealthPoints)
            {
                return;
            }

            numberOfAttacks += 1;
            if (numberOfAttacks != 0 && numberOfAttacks % CharacterConstants.Mage.ATTACKS_BEFORE_HEAL == 0)
            {
                HealthPoints += HealthPoints / healingPower;
                MessageAssistant.GreenMessage($"Маг восстановил здоровье своими яростными атаками! Теперь: {HealthPoints:F2}.");
            }
        }
    }
}