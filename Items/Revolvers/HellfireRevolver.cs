using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class HellfireRevolver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellfire Revolver");
			Tooltip.SetDefault("For hunting small game like rabbits and haggis.\nSets enemies on fire. Shoots 3 bullets when using musket balls.");
		}
		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 30;

			Item.useTime = 25;
			Item.useAnimation = 25;

			Item.damage = 8;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 2);
			Item.rare = ItemRarityID.Pink;

			Item.useAmmo = AmmoID.Bullet;
			Item.UseSound = SoundID.Item36;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 9f;
			Item.useStyle = ItemUseStyleID.Shoot;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if (type == ProjectileID.Bullet)
			{
				int degrees = Main.rand.Next(10);
				float numberProjectiles = 3; // 3 shots
				float rotation = MathHelper.ToRadians(30); //30 degrees spread
				position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 40f;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .75f; // Watch out for dividing by 0 if there is only 1 projectile.
					Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
				}
				return false;
			}
			else
			{
				return true;
			}
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ModContent.ItemType<LeckoBrick>())
				.AddIngredient(ModContent.ItemType<VolcanicRuby>(), 25)
				.AddIngredient(ItemID.HellstoneBar, 5)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}