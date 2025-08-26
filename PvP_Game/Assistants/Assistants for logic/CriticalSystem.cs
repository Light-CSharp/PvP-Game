namespace PvP_Game
{
    public static class CriticalSystem
    {
        public enum CriticalResult : byte
        {
            None = 1,
            Default,
            Hyper
        }

        private static readonly Random random = new((int)DateTime.UtcNow.Ticks);

        private const int MINIMUM_CRITICAL_CHANCE = 40;
        private const int MAXIMUM_CRITICAL_CHANCE = 100;

        private static CriticalResult criticalResult;

        public static CriticalResult PreviousCriticalResult { get => criticalResult; set => criticalResult = value; }

        public static void TryCriticalAttack(Player owner, Player target, Weapon weapon)
        {
            if (EnsureMinimumCritical(owner))
            {
                return;
            }

            if (CheckHyperCrit(weapon))
            {
                SuccessHyperCritical(owner, target, weapon);
                PreviousCriticalResult = CriticalResult.Hyper;
            }
            else if (CheckDefaultCrit(owner))
            {
                SuccessDefaultCritical(owner, target, weapon);
                PreviousCriticalResult = CriticalResult.Default;
            }
            else
            {
                FailedCritical(owner, weapon);
                PreviousCriticalResult = CriticalResult.None;
            }
        }

        private static bool EnsureMinimumCritical(Player owner)
        {
            if (owner.TotalCriticalChance < MINIMUM_CRITICAL_CHANCE)
            {
                owner.CriticalChance += 7;
                MessageAssistant.GreenMessage($"Шанс критического удара был слишком низким, он увеличен на 7%. Теперь: {owner.TotalCriticalChance}%.");
                return true;
            }
            return false;
        }

        public static bool CheckDefaultCrit(Player owner) => owner.TotalCriticalChance >= random.Next(MINIMUM_CRITICAL_CHANCE, MAXIMUM_CRITICAL_CHANCE + 1);
        private static void SuccessDefaultCritical(Player owner, Player target, Weapon weapon)
        {
            target.TakeDamage(owner.CriticalDamage);
            MessageAssistant.GreenMessage($"Критический удар поразил цель, здоровье врага: {target.HealthPoints:F2}.");

            owner.DamageBonus -= 2;
            MessageAssistant.RedMessage($"Из-за критического удара урон упал на 2%, нынешнее значение: {owner.TotalDamage:F2}.");

            weapon.CriticalHitsCount += 1;
            owner.FailedCriticalCount = default;
        }

        public static bool CheckHyperCrit(Weapon weapon) => weapon.CriticalHitsCount != 0 && weapon.CriticalHitsCount % 3 == 0 && random.Next(MAXIMUM_CRITICAL_CHANCE + 1) >= 50;
        private static void SuccessHyperCritical(Player owner, Player target, Weapon weapon)
        {
            target.TakeDamage(owner.TotalDamage * owner.TotalDamage);
            owner.HealthPoints -= owner.HealthPoints * 0.25;
            owner.Defense -= owner.Defense * 0.25;

            MessageAssistant.GreenMessage($"Гипер-Крит. прошёл успешно! Здоровье врага: {target.HealthPoints:F2}.");
            MessageAssistant.RedMessage($"Из-за гипер-Крита защита и здоровье упало на 25%, Здоровье: {owner.HealthPoints:F2}, Защита: {owner.TotalDefense:F2}.");

            weapon.CriticalType = CriticalType.Default;
        }

        private static void FailedCritical(Player owner, Weapon weapon)
        {
            MessageAssistant.RedMessage($"{(weapon.CriticalType == CriticalType.Hyper ? "Гипер-Крит." : "Критический удар")} не поразил цель!");

            owner.CriticalChance += (byte)++owner.FailedCriticalCount;
            MessageAssistant.GreenMessage($"Шанс критического удара был увеличен на {owner.FailedCriticalCount}%, теперь: {owner.TotalCriticalChance}%.");

            owner.HealthPoints += owner.HealthPoints / 100;
            MessageAssistant.GreenMessage($"Из-за промаха ваше здоровье стало больше на 1%, нынешнее количество: {owner.HealthPoints:F2}");
        }
    }
}