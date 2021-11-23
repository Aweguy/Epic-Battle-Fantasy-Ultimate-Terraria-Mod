using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
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
			item.width = 134;
			item.height = 56;

			item.useTime = 50;
			item.useAnimation = 50;

			item.damage = 97;
			item.crit = 10;
			item.noMelee = true;

			item.value = Item.sellPrice(gold: 2);
			item.rare = ItemRarityID.Yellow;

			item.UseSound = SoundID.Item40;
			item.shootSpeed = 12f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 30f;
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
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddIngredient(ModContent.ItemType<P2Processor>(), 4);
			recipe.AddIngredient(ModContent.ItemType<GlassShard>(), 50);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}