using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Dark;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
    public class Shot9 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Antimatter Cartridge");
            Tooltip.SetDefault("Converts foes to a focused flux, shredding reality. Probably safe to mass produce.");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 4));
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.consumable = true;
            Item.width = 48;
            Item.height = 18;
            Item.maxStack = 999;
            Item.knockBack = 1f;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightPurple;
            Item.shoot = ModContent.ProjectileType<AntimatterShot>();
            Item.shootSpeed = 7f;
             Item.ammo = ModContent.ItemType<Shot>();
        }

    }
}