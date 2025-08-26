namespace PvP_Game
{
    public abstract class Player(string name)
    {
        protected readonly string name = name ?? throw new ArgumentNullException(nameof(name), "Имя не может быть пустым!");
        protected string className = default!;

        protected double healthPoints;
        protected int healthPotions;
        protected int healingPower;

        protected double defense;
        protected double damageBonus;
        protected byte criticalChance;
        protected int failedCriticalCount;

        protected int moveCount;

        protected Weapon weapon = default!;

        public string Name => name;
        public string ClassName => className;

        public bool IsAlive => healthPoints > 0;
        public double HealthPoints { get => healthPoints; set => healthPoints = value; }

        public double Defense { get => defense; set => defense = value; }
        public double TotalDefense => Defense + weapon.Defense;
        public double HealthWithProtection => HealthPoints + TotalDefense;

        public double DamageBonus { get => damageBonus; set => damageBonus = value; }
        public virtual double TotalDamage => weapon.Damage * (DamageBonus / 100 + 1);

        public byte CriticalChance { get => criticalChance; set => criticalChance = value; }
        public byte TotalCriticalChance => (byte)(CriticalChance + weapon.CriticalChance);
        public double CriticalDamage => TotalDamage * 2.5;
        public int FailedCriticalCount { get => failedCriticalCount; set => failedCriticalCount = value; }

        public int MoveCount => moveCount;

        public virtual void OnTurnStart()
        {
            Console.WriteLine($"Сейчас ходит: {Name}.");
            moveCount += 1;
        }

        public abstract void ShowAvailableWeapons();

        public abstract void TakeWeapon(int option);
            
        public virtual void GetInfo()
        {
            MessageAssistant.BlueMessage($"Имя: {Name}, Класс: {ClassName}, Статус: {(IsAlive ? "Жив" : "Мёртв")}.");
            MessageAssistant.BlueMessage($"Количество здоровья: {HealthPoints:F2}.");
            MessageAssistant.BlueMessage($"Количество зелий здоровья: {(healthPotions != 0 ? healthPotions : "зелья кончились!")}.");
            MessageAssistant.BlueMessage($"Защита: {TotalDefense:F2}, Здоровье с учётом защиты: {HealthWithProtection:F2}.");
            MessageAssistant.BlueMessage($"Критический шанс: {TotalCriticalChance}%, Критический урон: {CriticalDamage:F2}.");
        }

        public virtual BattleManager.Actions ActionMenu()
        {
            Console.WriteLine($"{Name}, выберите действие: ");
            Console.WriteLine("1. Атаковать.");
            Console.WriteLine("2. Попытаться нанести критический удар.");
            Console.WriteLine("3. Лечиться.");

            bool isCorrect;
            byte option;
            do
            {
                isCorrect = byte.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= 3;
                if (!isCorrect)
                {
                    MessageAssistant.RedMessage("Неверно введённое действие, попробуйте ещё раз: ");
                }
            } while (!isCorrect);

            return (BattleManager.Actions)option;
        }

        public virtual void Attack(Player target) => weapon.Attack(target);

        public virtual void TryCriticalAttack(Player target) => weapon.TryCriticalAttack(target);

        public void Heal()
        {
            if (healthPotions <= 0)
            {
                MessageAssistant.RedMessage("Зелья здоровья кончились!");
                return;
            }

            MessageAssistant.GreenMessage($"Вы открыли зелье здоровье, ваше здоровье было: {HealthPoints:F2}.");

            HealthPoints += HealthPoints / healingPower;
            MessageAssistant.GreenMessage($"Зелье было успешно выпито: нынешнее количество здоровья: {HealthPoints:F2}.");

            healthPotions -= 1;
        }

        public void TakeDamage(double damage)
        {
            if (HealthWithProtection - damage < 0)
            {
                HealthPoints = default;
                MessageAssistant.RedMessage($"{Name} умер от полученных ранений!");
                return;
            }

            if (TotalDefense >= damage)
            {
                MessageAssistant.RedMessage($"Урон по {Name} слишком мал!");
                return;
            }

            HealthPoints = HealthWithProtection - damage;
        }
    }
}