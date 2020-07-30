using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame Shot");
            Tooltip.SetDefault("Reusable bullets that explode on impact! Don't ask how we get them back.");
        }

        public override void SetDefaults()
        {
            item.damage = 5;
            item.ranged = true;
            item.width = 12;
            item.height = 12;
            item.maxStack = 1;
            item.value = 1000;
            item.rare = 4;
            item.shoot = mod.ProjectileType("FlameShot");
            item.shootSpeed = 8f;
            item.ammo = item.type;
        }



        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            if (weapon.type == mod.ItemType("HellfireShotgun"))
                damage += 15;
        }








    }
}
