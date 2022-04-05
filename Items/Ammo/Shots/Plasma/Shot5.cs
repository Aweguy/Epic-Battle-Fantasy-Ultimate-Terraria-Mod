using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Plasma;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot5 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wave Shells");
            Tooltip.SetDefault("Fires a spread of blasts to fry all your devices. Not shotgun friendly.");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 20));
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
            Item.shoot = ModContent.ProjectileType<WaveShot>();
            Item.shootSpeed = 5f;
            Item.ammo = ModContent.ItemType<Shot>();
        }

    }
}