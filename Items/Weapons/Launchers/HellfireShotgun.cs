using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.Items.Ammo.Shots;
using EpicBattleFantasyUltimate.Projectiles.Bullets;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
    public class HellfireShotgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Shotgun");
            Tooltip.SetDefault("A hunting weapon capable of firing in bursts, but has finicky compatibility with some...proprietary ammunition.\nFires 5 shots simutenuously but very slow fire rate.\nSets enemies on fire\nFire lasts longer when you have the Hellfire Revolver in the inventory.");
        }


        public override void SetDefaults()
        {
            item.width = 110;
            item.height = 56;

            item.useTime = 30;
            item.useAnimation = 30;
            item.reuseDelay = 100;


            item.damage = 45;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 10);
            item.rare= ItemRarityID.Purple;

            item.shoot = 10;
            item.useAmmo = ItemType<Shot>();
            item.UseSound = SoundID.Item36;
            item.shootSpeed = 7f;
            item.useStyle = ItemUseStyleID.HoldingOut;
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


            int degrees = Main.rand.Next(10);
            float numberProjectiles = 5; // 5 shots
            float rotation = MathHelper.ToRadians(45); //30 degrees spread
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 40f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .75f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }





            return false;
        }



        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30, 0);
        }





        public override bool CanUseItem(Player player)
        {
            int buff = mod.BuffType("Overheat");
            return !player.HasBuff(buff);
        }




        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts, 3);
            recipe.AddIngredient(mod.ItemType("LeckoBrick"), 3);
            recipe.AddIngredient(mod.ItemType("VolcanicRuby"), 2);
            recipe.AddIngredient(ItemID.HellstoneBar, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }











    }
}

