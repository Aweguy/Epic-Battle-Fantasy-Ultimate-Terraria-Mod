using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Ammo.Shots;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
    public class HeavyClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavy Claw");
            Tooltip.SetDefault("Can hold three tons of metal in its grip, at least until your back gives out.\n Deals melee damage");
        }


        public override void SetDefaults()
        {
            item.width = 100;
            item.height = 52;

            item.useTime = 65;
            item.useAnimation = 65;
            item.reuseDelay = 20;


            item.damage = 102;
            item.melee = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 7);
            item.rare = ItemRarityID.Purple;

            item.shoot = 10;
            item.useAmmo = ModContent.ItemType<Shot>();
            item.UseSound = SoundID.Item40;
            item.shootSpeed = 15f;
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
            return true;
        }



        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-38, -5);
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
            recipe.AddIngredient(ModContent.ItemType<SteelPlate>(), 2);
            recipe.AddIngredient(mod.ItemType("LeckoBrick"), 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }







    }
}
