﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot6 : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Surge Shells");
            Tooltip.SetDefault("Fires a random spread of unusual blasts. Might just be Dark Flare.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(7, 8));
        }

        public override void SetDefaults()
        {
            item.damage = 10;
            item.ranged = true;
            item.width = 12;
            item.height = 12;
            item.maxStack = 1;
            item.knockBack = 0.5f;
            item.value = 10000;
            item.rare = 6;
            item.shoot = mod.ProjectileType("SurgeShot");
            item.shootSpeed = 15f;
            item.ammo = mod.ItemType("Shot");
        }



        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            if (weapon.type == mod.ItemType("ShadowBlaster"))
                damage += 30;
        }








    }
}