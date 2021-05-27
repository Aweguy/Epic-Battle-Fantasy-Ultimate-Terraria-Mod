using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
    public class TargetBadge : Flair
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Target Badge");
            Tooltip.SetDefault("Somehow a target on yourself makes you a better shot, who knew?\n1 defense\nIncreases critical chance by 10% for all types of damage.\nIncreases enemy aggression.");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.rare = ItemRarityID.LightRed;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 1;
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