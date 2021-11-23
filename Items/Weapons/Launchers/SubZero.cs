using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
	public class SubZero : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sub-Zero");
			Tooltip.SetDefault("It keeps fridges cool by day! It freezes foes by night! It’s Sub-Sub-Sub Sub Zero time!");
		}
		public override void SetSafeDefaults()
		{
			item.width = 100;
			item.height = 56;

			item.useTime = 60;
			item.useAnimation = 60;

			item.damage = 60;
			item.crit = 2;
			item.ranged = true;
			item.noMelee = true;

			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Purple;

			item.UseSound = SoundID.Item38;
			item.shootSpeed = 14f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 34f;
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
			return new Vector2(-32, -9);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IllegalGunParts, 3);
			recipe.AddIngredient(ModContent.ItemType<AbyssalSapphire>(), 5);
			recipe.AddIngredient(ModContent.ItemType<SolidWater>(), 4);
			recipe.AddIngredient(ItemID.IceBlock, 100);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}