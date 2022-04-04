using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class ShadowBlaster : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Blaster");
			Tooltip.SetDefault("A gun developed by fascists after studying Cosmic Monoliths.\nHigh critical chance.");
		}

		public override void SetSafeDefaults()
		{
			Item.width = 100;
			Item.height = 52;

			Item.useTime = 45;
			Item.useAnimation = 45;

			Item.damage = 100;
			Item.crit = 8;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item38;
			Item.shootSpeed = 19f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 34f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
			muzzleOffset += new Vector2(0, -8f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-50, -10);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts, 3)
				.AddIngredient(ModContent.ItemType<DarkMatter>(), 20)
				.AddIngredient(ItemID.Obsidian, 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}