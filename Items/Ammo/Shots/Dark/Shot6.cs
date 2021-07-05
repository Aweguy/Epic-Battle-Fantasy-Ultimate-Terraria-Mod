using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Dark;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot6 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Surge Shells");
            Tooltip.SetDefault("Fires a random spread of unusual blasts. Might just be Dark Flare.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
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
            item.shoot = ModContent.ProjectileType<SurgeShot>();
            item.shootSpeed = 7.5f;
            item.ammo = ModContent.ItemType<Shot>();
        }

    }
}