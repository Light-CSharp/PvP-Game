namespace PvP_Game
{
    public class SwordWithShield : Weapon
    {
        private bool isTemporaryProtectionActivity;

        public SwordWithShield(string name, Player owner) : base(name, owner)
        {
            damage = SwordsmanWeaponsConstants.SwordWithShield.DAMAGE;
            defense = SwordsmanWeaponsConstants.SwordWithShield.DEFENSE;
            criticalChance = SwordsmanWeaponsConstants.SwordWithShield.CRITICAL_CHANCE;
        }

        public override void OnTurnStart()
        {
            base.OnTurnStart();
            
            if (isTemporaryProtectionActivity && owner.MoveCount % SwordsmanWeaponsConstants.SwordWithShield.MOVE_BEFORE_GET_TEMPORARY_PROTECTION != 0)
            {
                owner.Defense -= Math.Abs(Defense) / SwordsmanWeaponsConstants.SwordWithShield.TEMPORARY_PROTECTION_POWER;
                return;
            }

            if (owner.MoveCount != 0 && owner.MoveCount % SwordsmanWeaponsConstants.SwordWithShield.MOVE_BEFORE_GET_TEMPORARY_PROTECTION == 0)
            {
                owner.Defense += Defense / SwordsmanWeaponsConstants.SwordWithShield.TEMPORARY_PROTECTION_POWER;
                isTemporaryProtectionActivity = true;

                MessageAssistant.BlueMessage($"Временная защита активна, общая защита: {owner.TotalDefense:F2}.");
            }
        }
    }
}