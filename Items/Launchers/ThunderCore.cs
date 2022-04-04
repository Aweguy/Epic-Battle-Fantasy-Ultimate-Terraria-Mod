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
	public class ThunderCore : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thunder Core");
			Tooltip.SetDefault("A portable powerhouse that is yellow despite all color schemes. Runs on clean Plutonium just below critical mass.\nShoots in 2 shot bursts.\nIncreases your speed by 20% when having Thunder Revolver in your inventory.");
		}
		public override void SetSafeDefaults()
		{
			Item.width = 100;
			Item.height = 52;

			Item.useTime = 15;
			Item.useAnimation = 30;
			Item.reuseDelay = 20;

			Item.damage = 55;
			Item.crit = 1;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item38;
			Item.shootSpeed = 14f;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 34f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
			muzzleOffset += new Vector2(0, -13.5f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-30, -9);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts, 3)
				.AddIngredient(ModContent.ItemType<PlutoniumCore>(), 3)
				.AddIngredient(ModContent.ItemType<VoltaicTopaz>(), 2)
				.AddIngredient(ItemID.Glass, 100)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}