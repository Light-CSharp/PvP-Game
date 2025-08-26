namespace PvP_Game
{
    public static class SpearmanWeaponsConstants
    {
        public static class LongSpear
        {
            public const string NAME = "Длинное копьё";

            public const double DAMAGE = 95;
            public const int SECOND_ATTACK_DAMAGE_DIVISOR = 4;
            public const double DEFENSE = -15;
            public const byte CRITICAL_CHANCE = 35;
        }

        public static class SpearWithShield
        {
            public const string NAME = "Копьё с щитом";

            public const double DAMAGE = 65;
            public const double DEFENSE = 10;
            public const byte CRITICAL_CHANCE = 10;
        }
    }
}