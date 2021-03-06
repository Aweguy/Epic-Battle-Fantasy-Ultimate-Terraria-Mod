﻿using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot5 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wave Shells");
            Tooltip.SetDefault("Fires a spread of blasts to fry all your devices. Not shotgun friendly.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 20));
        }

        public override void SetDefaults()
        {
            item.damage = 10;
            item.ranged = true;
            item.consumable = true;
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.knockBack = 0.5f;
            item.value = 10000;
            item.rare = ItemRarityID.LightPurple;
            item.shoot = mod.ProjectileType("WaveShot");
            item.shootSpeed = 5f;
            item.ammo = mod.ItemType("Shot");
        }

    }
}