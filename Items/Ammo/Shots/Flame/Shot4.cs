using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Fire;
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
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.consumable = true;
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.knockBack = 0.5f;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightPurple;
            Item.shoot = ModContent.ProjectileType<BurstShot>();
            Item.shootSpeed = 7.5f;
             Item.ammo = ModContent.ItemType<Shot>();
        }

    }
}