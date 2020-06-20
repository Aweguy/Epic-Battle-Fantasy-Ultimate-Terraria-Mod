using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.ClassTypes;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
    public class ShieldMedal : Flair
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield Medal");
            Tooltip.SetDefault("Aid others where you can. Let all be aided and loved in all lands.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 19;
            item.defense = 40;
            item.accessory = true;
            item.rare = 7;
        }


        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Titanium", 5);
            recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Gold", 15);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();


            
        }









    }
}
