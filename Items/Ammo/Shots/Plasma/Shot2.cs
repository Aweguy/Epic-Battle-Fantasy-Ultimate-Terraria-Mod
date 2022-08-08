using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Plasma;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasma Shot");
            Tooltip.SetDefault("You can make plasma by cutting open grapes and microwaving them! Don't do this at home, OR EVER. Reusable.");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 20));
        }

        public override void SetDefaults()
        {
            Item.damage = 4;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = 1;
            Item.knockBack = 0.5f;
            Item.value = 100;
            Item.consumable = true;
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.LightRed;
            Item.shoot = ModContent.ProjectileType<PlasmaShot>();
            Item.shootSpeed = 7f;
            Item.ammo = ModContent.ItemType<Shot>();
        }

    }
}