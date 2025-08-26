namespace PvP_Game
{
    public enum CriticalType : byte
    {
        Default = 1,
        Hyper
    }

    public abstract class Weapon(string name, Player owner)
    {
        protected readonly string name = name ?? throw new ArgumentNullException(nameof(name), "Имя не может быть пустым!");
        protected Player owner = owner;

        protected double damage;
        protected double defense;
        protected byte criticalChance;
        protected CriticalType criticalType = CriticalType.Default;

        protected int criticalHitsCount = 0;
        protected int defaultHitsCount = 0;

        public double Damage => damage;
        public double Defense => defense;
        public byte CriticalChance => criticalChance;
        public CriticalType CriticalType { get => criticalType; set => criticalType = value; }

        public int CriticalHitsCount { get => criticalHitsCount; set => criticalHitsCount = value; }
        public int DefaultHitCount { get => defaultHitsCount; set => defaultHitsCount = value; }

        public virtual void OnTurnStart()
        {
            owner.OnTurnStart();
        }

        public virtual void Attack(Player target)
        {
            target.TakeDamage(owner.TotalDamage);
            DefaultHitCount += 1;
        }

        public virtual void TryCriticalAttack(Player target)
        {
            CriticalSystem.TryCriticalAttack(owner, target, this);
        }
    }
}