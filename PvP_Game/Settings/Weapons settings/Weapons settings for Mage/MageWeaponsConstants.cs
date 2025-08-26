namespace PvP_Game
{
    public static class MageWeaponsConstants
    {
        public static class Fireball
        {
            public const string NAME = "Огненный шар";

            public const double DAMAGE = 120;
            public const double DAMAGE_REDUCTION = 0.05;

            public const double REDUCED_PROTECTION = 0.03;
        }

        public static class LightningStrike
        {
            public const string NAME = "Удар молнии";

            public const double REDUCED_PROTECTION = 0.33;
            public const double DAMAGE = 250;

            public const int QUANTITY_FOR_FIRST_ATTACK = 2;
            public const int QUANTITY_FOR_FOLLOWING_ATTACKS = 3;
        }
    }
}