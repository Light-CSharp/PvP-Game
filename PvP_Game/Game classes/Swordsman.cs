namespace PvP_Game
{
    public class Swordsman : Player
    {
        public Swordsman(string name) : base(name)
        {
            className = CharacterConstants.Swordsman.CLASS_NAME;

            healthPoints = CharacterConstants.Swordsman.HEALTH_POINTS;
            healthPotions = CharacterConstants.Swordsman.HEALTH_POTIONS;
            healingPower = CharacterConstants.Swordsman.HEALING_POWER;

            defense = CharacterConstants.Swordsman.DEFENSE;
            damageBonus = CharacterConstants.Swordsman.DAMAGE_BONUS;
            criticalChance = CharacterConstants.Swordsman.CRITICAL_CHANCE;
        }

        public override void OnTurnStart()
        {
            base.OnTurnStart();

            Defense += CharacterConstants.Swordsman.PASSIVE_ARMOR_INCREASE;
            MessageAssistant.GreenMessage($"Защита мечника возросла на {CharacterConstants.Swordsman.PASSIVE_ARMOR_INCREASE}, текущее значение: {TotalDefense:F2}.");
        }

        public override void ShowAvailableWeapons()
        {
            Console.WriteLine("1. Длинный меч");
            Console.WriteLine($"урон {SwordsmanWeaponsConstants.LongSword.DAMAGE}, {SwordsmanWeaponsConstants.LongSword.DEFENSE} защиты, критический шанс {SwordsmanWeaponsConstants.LongSword.CRITICAL_CHANCE}%");
            Console.WriteLine($"(обычные удары имеют формулу: урон = урон / 1.5 + урон * защиту / 100, после каждого хода защита падает на {SwordsmanWeaponsConstants.LongSword.DEFENSE_DECREASE_RATE * 100}%)");

            Console.WriteLine("2. Меч и щит");
            Console.WriteLine($"(урон {SwordsmanWeaponsConstants.SwordWithShield.DAMAGE}, {SwordsmanWeaponsConstants.SwordWithShield.DEFENSE} защиты, критический шанс {SwordsmanWeaponsConstants.SwordWithShield.CRITICAL_CHANCE}%)");
            Console.WriteLine($"(каждый {SwordsmanWeaponsConstants.SwordWithShield.MOVE_BEFORE_GET_TEMPORARY_PROTECTION} ход мечника даёт 1/{SwordsmanWeaponsConstants.SwordWithShield.TEMPORARY_PROTECTION_POWER} нынешнего значения защиты как временной защиты, которая спадет после любого действия противника)");
            Console.WriteLine($"(временная защита = даётся каждые 2 хода игрока, действует пока противник не сходит свой ход, а потом спадает)");
        }

        public override void TakeWeapon(int option)
        {
            switch ((SwordsmanWeapons)option)
            {
                case SwordsmanWeapons.LongSword:
                    weapon = new LongSword(SwordsmanWeaponsConstants.LongSword.NAME, this);
                    break;

                case SwordsmanWeapons.SwordWithShield:
                    weapon = new SwordWithShield(SwordsmanWeaponsConstants.SwordWithShield.NAME, this);
                    break;
            }
        }
    }
}