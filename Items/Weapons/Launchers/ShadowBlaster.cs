using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
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
			item.width = 100;
			item.height = 52;

			item.useTime = 45;
			item.useAnimation = 45;

			item.damage = 100;
			item.crit = 8;
			item.ranged = true;
			item.noMelee = true;

			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Purple;

			item.UseSound = SoundID.Item38;
			item.shootSpeed = 19f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 34f;
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
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IllegalGunParts, 3);
			recipe.AddIngredient(mod.ItemType("DarkMatter"), 20);
			recipe.AddIngredient(ItemID.Obsidian, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}