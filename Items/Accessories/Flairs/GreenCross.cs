using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
    public class GreenCross : Flair
    {
        private int timer = 60 * 10;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Cross");
            Tooltip.SetDefault("A Geneva-Convention-friendly pin which boasts regenerative properties.\nRegenerates 5% of your maximum health every 10 seconds");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 19;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            timer--;
            if (timer == 0)
            {
                int regen = (player.statLifeMax2 / 100) * 5;
                player.statLife += regen;
                player.HealEffect(regen);
                timer = 60 * 10;
            }
            player.lifeRegen += 5;
        }

        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.RegenerationPotion, 3)
                .AddIngredient(ItemID.LifeCrystal, 5)
                .AddIngredient(ItemID.PixieDust, 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}