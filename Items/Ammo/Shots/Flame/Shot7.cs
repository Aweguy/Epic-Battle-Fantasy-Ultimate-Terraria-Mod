using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Fire;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot7 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Canisters");
            Tooltip.SetDefault("Makes foes dance between scorching lead raindrops.");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 2));
        }

        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.consumable = true;
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.knockBack = 0.5f;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightPurple;
            Item.shoot = ModContent.ProjectileType<HellShot>();
            Item.shootSpeed = 7.5f;
             Item.ammo = ModContent.ItemType<Shot>();
        }

    }
}