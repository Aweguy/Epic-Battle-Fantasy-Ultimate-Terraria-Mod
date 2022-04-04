using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class CrystalWing : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystal Wing");
			Tooltip.SetDefault("A weapon crafted by futuristic Seraphim.\n Lower damage than the other launchers.\nSpecial Effect:\nHeals 25% of the damage done in one second. Can heal only once per second\nHeals 50% if Crystal Revolver is in inventory.");
		}

		public override void SetSafeDefaults()
		{
			Item.width = 84;
			Item.height = 54;

			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.reuseDelay = 10;

			Item.damage = 70;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item38;
			Item.shootSpeed = 11f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 34f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
			muzzleOffset += new Vector2(0, -7f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-36, -8);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts, 3)
				.AddIngredient(ModContent.ItemType<PristineDiamond>(), 5)
				.AddIngredient(ItemID.MarbleBlock, 150)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}