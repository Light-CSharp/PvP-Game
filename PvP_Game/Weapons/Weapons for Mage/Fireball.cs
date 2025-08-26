namespace PvP_Game
{
    public class Fireball : Weapon
    {
        public Fireball(string name, Player owner) : base(name, owner)
        {
            damage = MageWeaponsConstants.Fireball.DAMAGE;
        }

        public override void Attack(Player target)
        {
            base.Attack(target);

            target.Defense -= Math.Abs(target.Defense) * MageWeaponsConstants.Fireball.REDUCED_PROTECTION;
            owner.DamageBonus -= owner.DamageBonus * MageWeaponsConstants.Fireball.DAMAGE_REDUCTION;

            MessageAssistant.RedMessage($"Из-за огненной стихии ваш урон упал на 5%, нынешнее значение: {owner.TotalDamage:F2}.");
            MessageAssistant.GreenMessage($"Однако защита противника стала меньше на 3%: {target.TotalDefense:F2}.");
        }
    }
}