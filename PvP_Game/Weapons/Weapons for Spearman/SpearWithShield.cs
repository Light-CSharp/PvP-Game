namespace PvP_Game
{
    public class SpearWithShield : Weapon
    {
        private int extraHit;
        public SpearWithShield(string name, Player owner) : base(name, owner)
        {
            damage = SpearmanWeaponsConstants.SpearWithShield.DAMAGE;
            defense = SpearmanWeaponsConstants.SpearWithShield.DEFENSE;
            criticalChance = SpearmanWeaponsConstants.SpearWithShield.CRITICAL_CHANCE;
        }

        public override void Attack(Player target)
        {
            for (int i = 0; i <= extraHit; i++)
            {
                base.Attack(target);
            }
        }

        public override void TryCriticalAttack(Player target)
        {
            if (CriticalHitsCount != 0 && CriticalHitsCount % 3 == 0)
            {
                extraHit += 1;
                owner.CriticalChance = default;
            }

            for (int i = 0; i <= extraHit; i++)
            {
                base.TryCriticalAttack(target);
            }
        }
    }
}