namespace PvP_Game
{
    public static class SwordsmanWeaponsConstants
    {
        public static class LongSword
        {
            public const string NAME = "Длинный меч";

            public const double DAMAGE = 90;
            public const double DAMAGE_DIVIDER = 1.5;

            public const double DEFENSE = -10;
            public const double DEFENSE_DECREASE_RATE = 0.03;

            public const byte CRITICAL_CHANCE = 25;
        }

        public static class SwordWithShield
        {
            public const string NAME = "Меч с щитом";

            public const double DAMAGE = 80;
            public const double DEFENSE = 10;
            public const int MOVE_BEFORE_GET_TEMPORARY_PROTECTION = 2;
            public const int TEMPORARY_PROTECTION_POWER = 10;

            public const byte CRITICAL_CHANCE = 10;
        }
    }
}