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
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 4));
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.ranged = true;
            item.consumable = true;
            item.width = 48;
            item.height = 18;
            item.maxStack = 999;
            item.knockBack = 1f;
            item.value = 10000;
            item.rare = ItemRarityID.LightPurple;
            item.shoot = ModContent.ProjectileType<AntimatterShot>();
            item.shootSpeed = 7f;
            item.ammo = mod.ItemType("Shot");
        }

    }
}