using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class HeavyClaw : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heavy Claw");
			Tooltip.SetDefault("Can hold three tons of metal in its grip, at least until your back gives out.\n Deals melee damage");
		}
		public override void SetSafeDefaults()
		{
			Item.width = 100;
			Item.height = 52;

			Item.useTime = 65;
			Item.useAnimation = 65;
			Item.useTime = 65;
			Item.reuseDelay = 20;

			Item.damage = 102;
			Item.DamageType = DamageClass.Melee;;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 7);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item40;
			Item.shootSpeed = 15f;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 30f;
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
				.AddIngredient(ItemID.IllegalGunParts, 3)
				.AddIngredient(ModContent.ItemType<SteelPlate>(), 2)
				.AddIngredient(ModContent.ItemType<LeckoBrick>(), 2)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}