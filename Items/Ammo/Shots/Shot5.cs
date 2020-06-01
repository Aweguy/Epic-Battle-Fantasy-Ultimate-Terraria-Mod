using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot5 : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wave Shells");
            Tooltip.SetDefault("Fires a spread of blasts to fry all your devices. Not shotgun friendly.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 20));
        }

        public override void SetDefaults()
        {
            item.damage = 10;
            item.ranged = true;
            item.consumable = true;
            item.width = 12;
            item.height = 12;
            item.maxStack = 1;
            item.knockBack = 0.5f;
            item.value = 10000;
            item.rare = ItemRarityID.LightPurple;
            item.shoot = mod.ProjectileType("WaveShot");
            item.shootSpeed = 5f;
            item.ammo = mod.ItemType("Shot");
        }



        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            if (weapon.type == mod.ItemType("ThunderCore"))
                damage += 15;
        }








    }
}
