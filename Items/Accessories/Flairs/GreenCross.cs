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
            Tooltip.SetDefault("A Geneva-Convention-friendly pin which boasts regenerative properties.\nRegenerates 10 health every 10 seconds");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 19;
            item.accessory = true;
            item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            timer--;
            if (timer == 0)
            {
                player.statLife += 10;
                player.HealEffect(10);
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
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RegenerationPotion, 30);
            recipe.AddIngredient(ItemID.LifeCrystal, 2);
            recipe.AddIngredient(ItemID.PixieDust, 5);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}