using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;


namespace EpicBattleFantasyUltimate.Items.Spears.UltSpears
{
    public class GiantSlayer : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Giant Slayer");
            Tooltip.SetDefault("A sizable spear that cannot be consistently wielded.\nIgnores the enemies' defense stat.\nHas big reach.");
        }


        public override void SetDefaults()
        {
            item.damage = 40;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 50;
            item.useTime = 70;
            item.shootSpeed = 3.7f;
            item.knockBack = 6.5f;
            item.width = 32;
            item.height = 32;
            item.scale = 0.7f;
            item.rare= ItemRarityID.Pink;
            item.value = Item.sellPrice(gold : 10);
            item.melee = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.shoot = mod.ProjectileType("GiantSlayerProj");            
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            target.defense -= 43;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 100000000, 100000000, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }



    }
}
