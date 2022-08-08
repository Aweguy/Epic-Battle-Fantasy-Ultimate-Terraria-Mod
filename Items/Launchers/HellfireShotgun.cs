using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class HellfireShotgun : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellfire Shotgun");
			Tooltip.SetDefault("A hunting weapon capable of firing in bursts. Has compatibility issues with specialized ammunition.\nFires 5 shots simutenuously but very slow fire rate.\nSets enemies on fire\nFire lasts longer when you have the Hellfire Revolver in the inventory.");
		}
		public override void SetSafeDefaults()
		{
			Item.width = 110;
			Item.height = 56;

			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.reuseDelay = 100;

			Item.damage = 35;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item36;
			Item.shootSpeed = 7f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 30f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotation of the weapon
			muzzleOffset += new Vector2(0, -9f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			float numberProjectiles = 5; // 5 shots
			float rotation = MathHelper.ToRadians(45); //30 degrees spread
			position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 40f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .75f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(source,position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
			}
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-30, 0);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts, 3)
				.AddIngredient(ModContent.ItemType<LeckoBrick>(), 4)
				.AddIngredient(ModContent.ItemType<VolcanicRuby>(), 5)
				.AddIngredient(ItemID.HellstoneBar,20)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}