using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Items.Ammo.Shots;
using static Terraria.ModLoader.ModContent;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.ClassTypes;



namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
    public class FlameTitan : EpicLauncher
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame Titan");
            Tooltip.SetDefault("A mythical flamethrower capable of scouring ravening hordes. Untold magical power lies within.");
        }


        public override void SetSafeDefaults()
        {
            item.width = 108;
            item.height = 64;

            item.useTime = 80;
            item.useAnimation = 80;


            item.damage = 150;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Purple;

            item.UseSound = SoundID.Item38;
            item.shootSpeed = 11f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 34f;
            //Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
            muzzleOffset += new Vector2(0, -7f * player.direction).RotatedBy(muzzleOffset.ToRotation());
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }



        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-36, -8);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts, 3);
            recipe.AddIngredient(ItemType<VolcanicRuby>(), 5);
            recipe.AddIngredient(ItemType<SteelPlate>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
