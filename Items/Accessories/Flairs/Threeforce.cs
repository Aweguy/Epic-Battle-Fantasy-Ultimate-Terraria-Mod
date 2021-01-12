using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using EpicBattleFantasyUltimate.ClassTypes;


namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
    public class Threeforce : Flair
    {


        int regentimer = 60 * 5;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Threeforce");
            Tooltip.SetDefault("This amulet is fueled by...uh...'Strength', 'Fearlessness' and...uhhhh 'smartness'.");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.rare = ItemRarityID.Lime;
        }




        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var epicPlayer = EpicPlayer.ModPlayer(player);

            regentimer--;

            epicPlayer.Tryforce = true;


            if(regentimer <= 0 && epicPlayer.LimitCurrent < 100)
            {
                epicPlayer.LimitCurrent++;

                regentimer = 60 * 5;
            }


        }



        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
        }




        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Titanium", 10);
            recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Gold", 20);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();



        }*/









    }
}
