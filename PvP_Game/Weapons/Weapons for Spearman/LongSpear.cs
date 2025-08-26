namespace PvP_Game
{
    public class LongSpear : Weapon
    {
        public LongSpear(string name, Player owner) : base(name, owner)
        {
            damage = SpearmanWeaponsConstants.LongSpear.DAMAGE;
            defense = SpearmanWeaponsConstants.LongSpear.DEFENSE;
            criticalChance = SpearmanWeaponsConstants.LongSpear.CRITICAL_CHANCE;
        }

        public override void TryCriticalAttack(Player target)
        {
            base.TryCriticalAttack(target);

            if (CriticalSystem.PreviousCriticalResult == CriticalSystem.CriticalResult.Default)
            {
                MessageAssistant.GreenMessage("Из-за длинного копья нанесён дополнительный удар!");

                target.TakeDamage(owner.TotalDamage / SpearmanWeaponsConstants.LongSpear.SECOND_ATTACK_DAMAGE_DIVISOR);
                MessageAssistant.GreenMessage($"Нынешнее здоровье цели: {target.HealthPoints:F2}.");
            }
        }
    }
}