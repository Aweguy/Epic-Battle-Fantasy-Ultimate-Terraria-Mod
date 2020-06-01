using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Spears.UltSpears
{
    public class GashClub : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gash Club");
            Tooltip.SetDefault("Made from the trunk of a rare adult Gash Root tree.");
        }

        public override void SetDefaults()
        {
            item.useStyle= ItemUseStyleID.HoldingOut;
            item.useAnimation = 10;
            item.useTime = 10;
            item.shootSpeed = 24f;
            item.knockBack = 7f;
            item.width = 16;
            item.height = 16;
            item.rare= ItemRarityID.Pink;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.autoReuse = true;
            item.melee = true;
            item.damage = 40;

            item.UseSound = SoundID.DD2_MonkStaffSwing;
            item.shoot = mod.ProjectileType("GashClubProj");
        }












        /*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 10000f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            


            return true;
        }*/









        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use item when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
    }
}
