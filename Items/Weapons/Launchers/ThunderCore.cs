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
    public class ThunderCore : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunder Core");
            Tooltip.SetDefault("A portable powerhouse that is yellow despite all color schemes. Runs on clean Plutonium just below critical mass.\nShoots in 2 shot bursts.\nIncreases your speed by 20% when having Thunder Revolver in your inventory.");
        }


        public override void SetDefaults()
        {
            item.width = 100;
            item.height = 52;

            item.useTime = 10;
            item.useAnimation = 20;


            item.damage = 50;
            item.crit = 1;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 50);
            item.rare = 11;

            item.shoot = 10;
            item.useAmmo = ItemType<Shot>();
            item.UseSound = SoundID.Item38;
            item.shootSpeed = 11f;
            item.useStyle = 5;
        }





        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 34f;
            //Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
            muzzleOffset += new Vector2(0, -13.5f * player.direction).RotatedBy(muzzleOffset.ToRotation());
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }



        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30, -9);
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
            recipe.AddIngredient(mod.ItemType("PlutoniumCore"), 3);
            recipe.AddIngredient(mod.ItemType("VoltaicTopaz"), 2);
            recipe.AddIngredient(ItemID.Glass, 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }









    }
}
