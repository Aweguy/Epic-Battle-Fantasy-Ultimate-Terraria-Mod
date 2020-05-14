using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot3 : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Shot");
            Tooltip.SetDefault("A new kind of bullet modeled after Cosmic Monolith tech. Resuable.");
        }

        public override void SetDefaults()
        {
            item.damage = 10;
            item.ranged = true;
            item.width = 20;
            item.height = 20;
            item.maxStack = 1;
            item.crit = 4;
            item.knockBack = 1f;
            item.value = 1000;
            item.rare = 4;
            item.shoot = mod.ProjectileType("DarkShot");
            item.shootSpeed = 7f;
            item.ammo = mod.ItemType("Shot");
        }


        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            if (weapon.type == mod.ItemType("ShadowBlaster"))
                damage += 25;
        }






    }
}