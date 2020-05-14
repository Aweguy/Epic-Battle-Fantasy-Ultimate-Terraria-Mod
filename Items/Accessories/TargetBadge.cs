using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Accessories
{
    public class TargetBadge : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Target Badge");
            Tooltip.SetDefault("Somehow a target on yourself makes you a better shot, who knew?\nIncreases critical chance by 10% for all types of damage.\nIncreases enemy aggression.");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.defense = 1;
            item.accessory = true;
            item.rare = 4;
        }



        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicCrit += 10;
            player.meleeCrit += 10;
            player.rangedCrit += 10;
            player.thrownCrit += 10;
            player.aggro += 400;
        }



        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("AbyssalSapphire"));
            recipe.AddIngredient(mod.ItemType("VolcanicRuby"));
            recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Silver", 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();


        }
    }
}
