using EpicBattleFantasyUltimate.Projectiles.SpearProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Spears.UltSpears
{
	public class GiantSlayer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Giant Slayer");
			Tooltip.SetDefault("A sizable spear that bests foes through sheer weight and power.\nIgnores the enemies' defense stat.\nHas big reach.");
		}

		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 50;
			Item.useTime = 70;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.width = 32;
			Item.height = 32;
			Item.scale = 0.7f;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(gold: 10);
			Item.DamageType = DamageClass.Melee;;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an Item. This prevents the melee hitbox of this Item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this Item.
			Item.shoot = ModContent.ProjectileType<GiantSlayerProj>();
		}


		public override void HoldItem(Player player)
		{
			player.armorPenetration += 100;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 25f;
			if (Collision.CanHit(position, 100000000, 100000000, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}
	}
}