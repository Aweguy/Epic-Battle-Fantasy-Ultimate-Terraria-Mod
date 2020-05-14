﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class KingsGuardShield : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("King's Guard Shield");
            Tooltip.SetDefault("'All hail the King! Only... the king is dead, and so are the guards...' \nA strong and heavy shield made of the finest materials.\nGives immunity to knockback\nSlows movement speed by 25%");
        }

        public override void SetDefaults()
        {
            item.width = 23;
            item.height = 32;
            item.defense = 65;
            item.accessory = true;
            item.rare = 11;
        }



        public override void UpdateAccessory(Player player, bool hideVisual)
        {         
            player.noKnockback = true;
        }



        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane});
        }



        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LightShard, 2);
            recipe.AddIngredient(ItemID.DarkShard, 2);
            recipe.AddIngredient(ItemID.SoulofLight, 4);
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();



        }*/
    }
}
