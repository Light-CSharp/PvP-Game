namespace PvP_Game
{
    public class Spearman : Player
    {
        public Spearman(string name) : base(name)
        {
            className = CharacterConstants.Spearman.CLASS_NAME;

            healthPoints = CharacterConstants.Spearman.HEALTH_POINTS;
            healthPotions = CharacterConstants.Spearman.HEALTH_POTIONS;
            healingPower = CharacterConstants.Spearman.HEALING_POWER;

            defense = CharacterConstants.Spearman.DEFENSE;
            damageBonus = CharacterConstants.Spearman.DAMAGE_BONUS;
            criticalChance = CharacterConstants.Spearman.CRITICAL_CHANCE;
        }

        public override void ShowAvailableWeapons()
        {
            Console.WriteLine("1. Длинное копьё");
            Console.WriteLine($"(урон {SpearmanWeaponsConstants.LongSpear.DAMAGE}, {SpearmanWeaponsConstants.LongSpear.DEFENSE} защиты, критический шанс {SpearmanWeaponsConstants.LongSpear.CRITICAL_CHANCE}%)");
            Console.WriteLine($"(добавляет ещё один дополнительный удар от критический ударов, но будет слабее в 2 раза, а с учётом класса в {SpearmanWeaponsConstants.LongSpear.SECOND_ATTACK_DAMAGE_DIVISOR} раза)");

            Console.WriteLine("2. Копьё и щит");
            Console.WriteLine($"(урон {SpearmanWeaponsConstants.SpearWithShield.DAMAGE}, {SpearmanWeaponsConstants.SpearWithShield.DEFENSE} защиты, критический шанс {SpearmanWeaponsConstants.SpearWithShield.CRITICAL_CHANCE}%)");
            Console.WriteLine($"(добавляется дополнительный удар, после каждый 3 критический ударов, но при этом критический падает на 100% для персонажа и его снова надо будет набирать)");
        }

        public override void TakeWeapon(int option)
        {
            switch ((SpearmanWeapons)option)
            {
                case SpearmanWeapons.LongSpear:
                    weapon = new LongSpear(SpearmanWeaponsConstants.LongSpear.NAME, this);
                    break;

                case SpearmanWeapons.SpearWithShield:
                    weapon = new SpearWithShield(SpearmanWeaponsConstants.SpearWithShield.NAME, this);
                    break;
            }
        }
            
        public override void TryCriticalAttack(Player target)
        {
            base.TryCriticalAttack(target);

            if (CriticalSystem.CheckDefaultCrit(this))
            {
                MessageAssistant.GreenMessage("Из-за класса копейщика нанесён дополнительный удар!");

                target.TakeDamage(TotalDamage / CharacterConstants.Spearman.SECOND_ATTACK_DAMAGE_DIVISOR);
                MessageAssistant.GreenMessage($"Нынешнее здоровье цели: {target.HealthPoints:F2}");
            }
        }
    }
}