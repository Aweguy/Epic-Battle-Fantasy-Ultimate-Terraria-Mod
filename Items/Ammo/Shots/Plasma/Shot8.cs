using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Plasma;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot8 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasmacross Clip");
            Tooltip.SetDefault("Creates a massive blast of plasma to ensure foes are cooked evenly");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 20));
        }

        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.consumable = true;
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = 999;
            Item.knockBack = 0.5f;
            Item.value = 1000;
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ModContent.ProjectileType<FieldShot>();
            Item.shootSpeed = 7f;
            Item.ammo = ModContent.ItemType<Shot>();
        }

    }
}