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
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 19));
        }

        public override void SetDefaults()
        {
            item.damage = 15;
            item.ranged = true;
            item.consumable = true;
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.knockBack = 0.5f;
            item.value = 10000;
            item.rare = ItemRarityID.LightPurple;
            item.shoot = mod.ProjectileType("HellShot");
            item.shootSpeed = 7.5f;
            item.ammo = mod.ItemType("Shot");
        }

        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            if (weapon.type == mod.ItemType("HellfireShotgun"))
                damage += 30;
        }
    }
}