﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Projectiles.SwordProjectiles;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class GaiaAxe : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gaia's Axe");
            Tooltip.SetDefault("'The woodsman slices the wood of trees by day, and the bones of fiends by night.'\n Right-Click to throw a small axe. Cannot be used while the axe is thrown.");
        }

        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;

            item.damage = 100;
            item.knockBack = 5f;
            item.melee = true;

            item.useTime = 30;
            item.useAnimation = 30;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            

            item.value = Item.sellPrice(gold: 10);
            item.rare = 10;
            item.autoReuse = true;
        }

        public override bool OnlyShootOnSwing => true;


        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                item.width = 64;
                item.height = 64;

                item.damage = 100;
                item.knockBack = 5f;
                item.melee = true;
                item.shoot = ModContent.ProjectileType<GaiaAxeProj>();
                item.shootSpeed = 10f;

                item.useTime = 60;
                item.useAnimation = 60;
                item.UseSound = SoundID.Item1;
                item.useStyle = ItemUseStyleID.SwingThrow;


                item.value = Item.sellPrice(gold: 10);
                item.rare = 10;
                item.autoReuse = true;

            }
            else
            {
                item.width = 64;
                item.height = 64;

                item.damage = 79;
                item.knockBack = 5f;
                item.melee = true;
                item.shoot = ProjectileID.None;
                item.shootSpeed = 0f;

                item.useTime = 30;
                item.useAnimation = 30;
                item.UseSound = SoundID.Item1;
                item.useStyle = ItemUseStyleID.SwingThrow;


                item.value = Item.sellPrice(gold: 10);
                item.rare = 9;
                item.autoReuse = true;
            }


            return player.ownedProjectileCounts[ModContent.ProjectileType<GaiaAxeProj>()] < 1;

        }





    }
}