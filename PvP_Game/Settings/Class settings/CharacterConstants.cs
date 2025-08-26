namespace PvP_Game
{
    public static class CharacterConstants
    {
        public static class Mage
        {
            public const string CLASS_NAME = "Маг";

            public const double HEALTH_POINTS = 300;
            public const int HEALING_POWER = 10;
            public const int ATTACKS_BEFORE_HEAL = 2;

            public const double DEFENSE = -65;
            public const double DAMAGE_BONUS = 50;
        }

        public static class Spearman
        {
            public const string CLASS_NAME = "Копейщик";

            public const double HEALTH_POINTS = 450;
            public const int HEALTH_POTIONS = 2;
            public const int HEALING_POWER = 5;

            public const double DEFENSE = 30;
            public const double DAMAGE_BONUS = 25;
            public const double SECOND_ATTACK_DAMAGE_DIVISOR = 2;
            public const byte CRITICAL_CHANCE = 35;
        }

        public static class Swordsman
        {
            public const string CLASS_NAME = "Мечник";

            public const double HEALTH_POINTS = 500;
            public const int HEALTH_POTIONS = 3;
            public const int HEALING_POWER = 7;

            public const double DEFENSE = 50;
            public const double PASSIVE_ARMOR_INCREASE = 2;

            public const double DAMAGE_BONUS = 10;
            public const byte CRITICAL_CHANCE = 20;
        }
    }
}