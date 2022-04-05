using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Dark;
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
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.crit = 4;
            Item.knockBack = 1f;
            Item.value = 1000;
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ModContent.ProjectileType<DarkShot>();
            Item.shootSpeed = 7f;
            Item.ammo = ModContent.ItemType<Shot>();
        }

    }
}