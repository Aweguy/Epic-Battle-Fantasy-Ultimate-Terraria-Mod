using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Spears.UltSpears
{
    public class GashClub : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gash Club");
            Tooltip.SetDefault("Made from the trunk of a rare adult Gash Root tree.");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 3.7f;
            item.knockBack = 6.5f;
            item.width = 72;
            item.height = 72;
            item.scale = 1f;
            item.rare = 5;
            item.value = Item.sellPrice(silver: 10);

            item.melee = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("GashClubProj");
        }

        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[item.shoot] < 1;
        }
    }
}
