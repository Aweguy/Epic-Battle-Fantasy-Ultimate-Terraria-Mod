using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class FlameTitan : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame Titan");
			Tooltip.SetDefault("A mythical flamethrower capable of scouring ravening hordes. Untold magical power lies within.");
		}

		public override void SetSafeDefaults()
		{
			Item.width = 96;
			Item.height = 58;

			Item.useTime = 80;
			Item.useAnimation = 80;

			Item.damage = 100;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item38;
			Item.shootSpeed = 11f;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 29f;
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
				.AddIngredient(ModContent.ItemType<VolcanicRuby>(), 5)
				.AddIngredient(ModContent.ItemType<SteelPlate>(), 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}