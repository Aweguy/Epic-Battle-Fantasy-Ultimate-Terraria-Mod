using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class Visecracker : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Visecracker");
            Tooltip.SetDefault("Puts the bullet's weight into every shot\nDeals melee damage.");
        }


        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 28;

            item.damage = 25;
            item.useTime = 25;
            item.useAnimation = 25;
            item.useStyle = ItemUseStyleID.HoldingOut;

            item.melee = true;
            item.noMelee = true;

            item.rare = ItemRarityID.Pink;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 10f;
            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item41;


        }








    }
}
