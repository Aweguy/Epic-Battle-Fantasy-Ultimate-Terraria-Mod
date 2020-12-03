using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.Items.Ammo.Shots;
using EpicBattleFantasyUltimate.Projectiles.Bullets;
using static Terraria.ModLoader.ModContent;


namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class HellfireRevolver : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Revolver");
            Tooltip.SetDefault("For hunting small game like rabbits and haggis.\nSets enemies on fire. Shoots 3 bullets when using musket balls.");
        }




        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 30;

            item.useTime = 25;
            item.useAnimation = 25;

            item.damage = 8;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 2);
            item.rare= ItemRarityID.Pink;

            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item36;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 9f;
            item.useStyle = ItemUseStyleID.HoldingOut;

        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            if(type == ProjectileID.Bullet)
            {
                int degrees = Main.rand.Next(10);
                float numberProjectiles = 3; // 3 shots
                float rotation = MathHelper.ToRadians(30); //30 degrees spread
                position += Vector2.Normalize(new Vector2(speedX, speedY)) * 40f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .75f; // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
                return false;
            }
            else
            {
                return true;
            }


        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(mod.ItemType("LeckoBrick"));
            recipe.AddIngredient(mod.ItemType("VolcanicRuby"));
            recipe.AddIngredient(ItemID.HellstoneBar, 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }




    }
}
