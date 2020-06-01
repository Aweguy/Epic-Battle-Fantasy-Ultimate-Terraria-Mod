using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Accessories
{
    public class AngryFaic : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angry Faic");
            Tooltip.SetDefault("An emblem so angry, you want something to be BLAMMED!\nIncreases critical chance by 8% for all types of damage.\nIncreases damage by 8%.\nIncreases enemy aggression.");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.defense = 2;
            item.rare = ItemRarityID.LightPurple;
            item.accessory = true;
        }



        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicCrit += 8;
            player.meleeCrit += 8;
            player.rangedCrit += 8;
            player.thrownCrit += 8;
            player.allDamage += 0.08f;
            player.aggro += 450;
        }



        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddIngredient(mod.ItemType("TargetBadge"));
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
