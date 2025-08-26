namespace PvP_Game
{
    public class LongSword : Weapon
    {
        public LongSword(string name, Player owner) : base(name, owner)
        {
            damage = SwordsmanWeaponsConstants.LongSword.DAMAGE;
            defense = SwordsmanWeaponsConstants.LongSword.DEFENSE;
            criticalChance = SwordsmanWeaponsConstants.LongSword.CRITICAL_CHANCE;
        }

        public override void Attack(Player target)
        {
            double longSwordDamage = Damage / SwordsmanWeaponsConstants.LongSword.DAMAGE_DIVIDER + Damage * owner.TotalDefense / 100;
            target.TakeDamage(longSwordDamage);

            if (owner.Defense != 0)
            {
                owner.Defense -= owner.Defense * SwordsmanWeaponsConstants.LongSword.DEFENSE_DECREASE_RATE;
                MessageAssistant.RedMessage($"Из-за оружия ваша защита упала на 3%, нынешнее значение: {owner.TotalDefense:F2}.");
            }
        }
    }
}