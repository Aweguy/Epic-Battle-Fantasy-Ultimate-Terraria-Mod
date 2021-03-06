﻿using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
    public class GungnirRifle : EpicLauncher
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gungnir Rifle");
            Tooltip.SetDefault("An ergonomic, high crit-rate gun that, shockingly, is not a spear.\nHigh damage and velocity bullets. Slow fire rate.\nHaving Regnir in your inventory increases its critical rate by 30%");
        }

        public override void SetSafeDefaults()
        {
            item.width = 100;
            item.height = 52;

            item.useTime = 65;
            item.useAnimation = 65;
            item.reuseDelay = 20;

            item.damage = 135;
            item.crit = 25;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Purple;

            item.UseSound = SoundID.Item40;
            item.shootSpeed = 24f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 30f;
            //Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
            muzzleOffset += new Vector2(0, -9f * player.direction).RotatedBy(muzzleOffset.ToRotation());
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-38, -5);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts, 3);
            recipe.AddIngredient(mod.ItemType("PlutoniumCore"), 2);
            recipe.AddIngredient(mod.ItemType("LeckoBrick"), 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}