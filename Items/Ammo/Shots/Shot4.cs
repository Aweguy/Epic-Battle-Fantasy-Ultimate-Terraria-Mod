using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot4 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Burst Shells");
            Tooltip.SetDefault("A bullet that explodes into more bullets, popular among spin-off lovers.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 5));
        }

        public override void SetDefaults()
        {
            item.damage = 10;
            item.ranged = true;
            item.consumable = true;
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.knockBack = 0.5f;
            item.value = 10000;
            item.rare = ItemRarityID.LightPurple;
            item.shoot = mod.ProjectileType("BurstShot");
            item.shootSpeed = 7.5f;
            item.ammo = mod.ItemType("Shot");
        }

    }
}