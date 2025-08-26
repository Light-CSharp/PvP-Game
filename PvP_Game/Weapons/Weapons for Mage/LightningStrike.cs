namespace PvP_Game
{
    public class LightningStrike : Weapon
    {
        private int charge;
        public LightningStrike(string name, Player owner) : base(name, owner)
        {
            damage = MageWeaponsConstants.LightningStrike.DAMAGE;
        }

        public override void Attack(Player target)
        {
            charge += 1;

            bool isFirstSuccessful = DefaultHitCount == 0 && charge == MageWeaponsConstants.LightningStrike.QUANTITY_FOR_FIRST_ATTACK;
            bool isSubsequentSuccessful = DefaultHitCount > 0 && charge == MageWeaponsConstants.LightningStrike.QUANTITY_FOR_FOLLOWING_ATTACKS;

            if (isFirstSuccessful || isSubsequentSuccessful)
            {
                base.Attack(target);
                SuccessAttack(target);
            }
            else
            {
                FailureAttack();
            }
        }

        private void SuccessAttack(Player target)
        {
            target.Defense -= Math.Abs(Defense) * MageWeaponsConstants.LightningStrike.REDUCED_PROTECTION;
            charge = default;

            MessageAssistant.GreenMessage($"После удара молнии защита противника упала на 33%: {target.TotalDefense:F2}");
        }

        private static void FailureAttack()
        {
            MessageAssistant.RedMessage("Маг накапливает заряд молнии в своей руке...");
            return;
        }
    }
}