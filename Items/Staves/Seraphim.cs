using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Projectiles.StaffProjectiles;

namespace EpicBattleFantasyUltimate.Items.Staves
{
    public class Seraphim : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seraphim");
            Tooltip.SetDefault("A glorious staff used by gorgeous angels.");
        }


        public override void SetDefaults()
        {
            item.width = 100;
            item.height = 100;

            item.damage = 100;
            item.magic = true;
            //item.mana = 100;
            item.noMelee = true;

            item.useTime = 1;
            item.useAnimation = 1;
            item.useStyle = ItemUseStyleID.HoldingOut;

            //item.channel = true; //Channel so that you can held the weapon [Important]

            item.shoot = ModContent.ProjectileType<Judgement>();
            item.shootSpeed = 1f;
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {


            int num233 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
            int num234 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                num234 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
            }
            for (; num234 < Main.maxTilesY && Main.tile[num233, num234] != null && !WorldGen.SolidTile2(num233, num234) && Main.tile[num233 - 1, num234] != null && !WorldGen.SolidTile2(num233 - 1, num234) && Main.tile[num233 + 1, num234] != null && !WorldGen.SolidTile2(num233 + 1, num234); num234++)
            {
            }            
            Projectile.NewProjectile((float)Main.mouseX + Main.screenPosition.X, (float)(num234 * 16), 0f, 0f, ModContent.ProjectileType<Judgement>() , item.damage, 0f, player.whoAmI, 0f, 0f);
            




            return false;
        }


        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<Judgement>()] < 1;
        }






    }
}
