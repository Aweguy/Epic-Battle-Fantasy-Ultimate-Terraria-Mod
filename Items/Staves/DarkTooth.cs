﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace EpicBattleFantasyUltimate.Items.Staves
{
    public class DarkTooth : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Tooth");
            Tooltip.SetDefault("Ancient black magic staff used for Dark elemental magic.\nIt will summon a slowly growing black hole that will explode when stopped channeling.\n");
        }

        public override void SetDefaults()
        {
            item.damage = 1;
            item.width = 40;
            item.height = 40;
            item.useStyle = 1;
            item.useTime = 50;
            item.useAnimation = 50;
            item.mana = 10;
            item.rare = 9;
            item.value = Item.sellPrice(platinum: 1);
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Pulsar");
            item.shootSpeed = 0f;
			item.noMelee = true;
			item.magic = true;
            item.channel = true;
        }




        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;


            






            return true;


            


        }



        public override void HoldItem(Player player)
        {

            Color drawColor = Color.Black;
            if (Main.rand.Next(2) == 0)
            {
                drawColor = Color.Red;
            }


            if (player.channel)
            {
                
                Dust.NewDustDirect(player.position, player.width, player.height, 302, 0f, 0f, 0, drawColor, 1f);
            }
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddIngredient(ItemID.DarkShard, 2);
            recipe.AddIngredient(mod.ItemType("DarkMatter"), 10);
            recipe.AddIngredient(ItemID.FragmentNebula);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }




    }
}