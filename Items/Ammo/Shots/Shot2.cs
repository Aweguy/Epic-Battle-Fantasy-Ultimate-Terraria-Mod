﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;




namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot2 : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasma Shot");
            Tooltip.SetDefault("You can make plasma by cutting open grapes and microwaving them! Don't do this at home, OR EVER. Reusable.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 20));
        }

        public override void SetDefaults()
        {
            item.damage = 4;
            item.ranged = true;
            item.width = 10;
            item.height = 10;
            item.maxStack = 1;
            item.knockBack = 0.5f;
            item.value = 1000;
            item.rare = 4;
            item.shoot = mod.ProjectileType("PlasmaShot");
            item.shootSpeed = 7f;
            item.ammo = mod.ItemType("Shot");
        }







        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            if (weapon.type == mod.ItemType("ThunderCore"))
                damage += 10;
        }




    }
}
