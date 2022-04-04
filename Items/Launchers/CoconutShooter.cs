using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class CoconutShooter : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coconut Shooter");
			Tooltip.SetDefault("A gorilla's bazooka. Beat your chest with open palms before use.\nHigh damage with huge knockback, but slow fire rate and velocity.");
		}

		public override void SetSafeDefaults()
		{
			Item.width = 94;
			Item.height = 70;

			Item.useTime = 60;
			Item.useAnimation = 60;
			Item.reuseDelay = 10;

			Item.damage = 110;
			Item.knockBack = 20f;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 3);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item38;
			Item.shootSpeed = 5f;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 29f;
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
				.AddIngredient(ItemID.IllegalGunParts,3)
				.AddIngredient(ItemID.Wood, 120)
				.AddIngredient(ModContent.ItemType<CyclonicEmerald>(), 2)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}