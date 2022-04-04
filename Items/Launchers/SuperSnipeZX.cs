using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class SuperSnipeZX : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Snipe ZX ");
			Tooltip.SetDefault("A 3rd-generation sniper rifle, complete with isopod and scope.");
		}
		public override void SetSafeDefaults()
		{
			Item.width = 134;
			Item.height = 56;

			Item.useTime = 50;
			Item.useAnimation = 50;

			Item.damage = 97;
			Item.crit = 10;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 2);
			Item.rare = ItemRarityID.Yellow;

			Item.UseSound = SoundID.Item40;
			Item.shootSpeed = 12f;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 30f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
			muzzleOffset += new Vector2(0, -14f * player.direction).RotatedBy(muzzleOffset.ToRotation());
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
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ModContent.ItemType<P2Processor>(), 5)
				.AddIngredient(ModContent.ItemType<GlassShard>(), 50)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}