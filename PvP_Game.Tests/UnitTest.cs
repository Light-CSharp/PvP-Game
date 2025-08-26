namespace PvP_Game.Tests
{
    public class UnitTest
    {
        private static class TestData
        {
            public static TheoryData<Player, double> PlayerHealthData => new()
            {
                { new Mage("TestMage"), CharacterConstants.Mage.HEALTH_POINTS / 2 },
                { new Spearman("TestSpearman"), CharacterConstants.Spearman.HEALTH_POINTS / 2 },
                { new Swordsman("TestSwordsman"), CharacterConstants.Swordsman.HEALTH_POINTS / 2 }
            };

            public static TheoryData<Player> PlayerData => new()
            {
                { new Mage("TestMage") },
                { new Spearman("TestSpearman") },
                { new Swordsman("TestSwordsman") }
            };

            public static TheoryData<Player> PlayerHealData => new()
            {
                { new Spearman("TestSpearman") },
                { new Swordsman("TestSwordsman") }
            };
        }

        [Theory]
        [MemberData(nameof(TestData.PlayerHealthData), MemberType = typeof(TestData))]
        public void Player_TakeDamage_ShouldReduceHealth(Player player, double damage)
        {
            player.TakeWeapon(1);

            double beforeHealthPoints = player.HealthPoints;
            player.TakeDamage(damage);

            Assert.True(player.HealthPoints < beforeHealthPoints);
        }

        [Theory]
        [MemberData(nameof(TestData.PlayerData), MemberType = typeof(TestData))]
        public void Player_TakeDamage_ShouldDie(Player player)
        {
            player.TakeWeapon(1);

            player.TakeDamage(int.MaxValue);

            Assert.False(player.IsAlive);
        }

        [Theory]
        [MemberData(nameof(TestData.PlayerHealData), MemberType = typeof(TestData))]
        public void Player_Heal_ShouldIncreaseHealthPoints(Player player)
        {
            player.TakeWeapon(1);

            double beforeHealthPoints = player.HealthPoints;
            player.Heal();

            Assert.True(player.HealthPoints > beforeHealthPoints);
        }

        [Fact]
        public void Mage_Attacks_ShouldHealAfterTwoSuccessfulHits()
        {
            Mage mage = new("TestMage");
            mage.TakeWeapon((int)MageWeapons.Fireball);

            Swordsman swordsman = new("TestSwordsman");
            swordsman.TakeWeapon((int)SwordsmanWeapons.SwordWithShield);

            double beforeHealthPoints = mage.HealthPoints;
            mage.Attack(swordsman);
            mage.Attack(swordsman);

            Assert.True(mage.HealthPoints > beforeHealthPoints);
        }

        [Fact]
        public void Player_CriticalAttack_SuccessHit()
        {
            Swordsman swordsman = new("TestSwordsman");
            swordsman.TakeWeapon((int)SwordsmanWeapons.LongSword);

            Spearman spearman = new("TestSpearman");
            spearman.TakeWeapon((int)SpearmanWeapons.LongSpear);

            double beforeHealthPoints = spearman.HealthPoints;

            swordsman.CriticalChance = 100;
            swordsman.TryCriticalAttack(spearman);

            Assert.True(spearman.HealthPoints < beforeHealthPoints);
        }
    }
}