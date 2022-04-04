using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class GungnirRifle : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gungnir Rifle");
			Tooltip.SetDefault("An ergonomic, high crit-rate gun that, shockingly, is not a spear.\nHigh damage and velocity bullets. Slow fire rate.\nHaving Regnir in your inventory increases its critical rate by 30%");
		}

		public override void SetSafeDefaults()
		{
			Item.width = 100;
			Item.height = 52;

			Item.useTime = 65;
			Item.useAnimation = 65;
			Item.reuseDelay = 20;

			Item.damage = 135;
			Item.crit = 25;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item40;
			Item.shootSpeed = 24f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 30f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
			muzzleOffset += new Vector2(0, -9f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-38, -5);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts, 3)
				.AddIngredient(ModContent.ItemType<PlutoniumCore>(), 2)
				.AddIngredient(ModContent.ItemType<LeckoBrick>(), 2)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}