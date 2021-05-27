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
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 20));
        }

        public override void SetDefaults()
        {
            item.damage = 4;
            item.ranged = true;
            item.consumable = true;
            item.width = 10;
            item.height = 10;
            item.maxStack = 999;
            item.knockBack = 0.5f;
            item.value = 1000;
            item.rare = ItemRarityID.LightRed;
            item.shoot = mod.ProjectileType("FieldShot");
            item.shootSpeed = 7f;
            item.ammo = mod.ItemType("Shot");
        }

        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            if (weapon.type == mod.ItemType("ThunderCore"))
                damage += 10;
        }
    }
}