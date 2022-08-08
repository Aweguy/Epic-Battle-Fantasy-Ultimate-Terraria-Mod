using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.SpellProjectiles.Airstrikes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Spells
{
	public class Airstrike : ModItem
	{
		public static readonly SoundStyle AirstrikeSound = new("EpicBattleFantasyUltimate/Assets/Sounds/Item/Airstrike")
		{
			Volume = 2f,
			PitchVariance = 0.5f
		};


		private float offsetX = 20f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Airstrike");
			Tooltip.SetDefault("Bombs away!!!!\nLeft click to quickly drop bombs down, right click to drop 3 weaker bombs at once.");
		}

		public override void SetDefaults()
		{
			Item.damage = 120;
			Item.width = 24;
			Item.height = 32;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.mana = 10;
			Item.rare = ItemRarityID.Yellow;
			Item.useTurn = true;
			Item.shoot = ModContent.ProjectileType<Bomb>();
			Item.shootSpeed = 16f;
			Item.value = Item.sellPrice(gold: 1);
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.autoReuse = true;
			if (!Main.dedServ)
				Item.UseSound = AirstrikeSound;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.useTime = 60;
				Item.useAnimation = 60;
				Item.damage = 60;
				Item.mana = 30;
				Item.shoot = ModContent.ProjectileType<SmallBomb>();
				Item.shootSpeed = 16f;
				if (!Main.dedServ)
					Item.UseSound = AirstrikeSound;
			}
			else
			{
				Item.damage = 120;
				Item.useTime = 40;
				Item.useAnimation = 40;
				Item.mana = 10;
				Item.shoot = ModContent.ProjectileType<Bomb>();
				Item.shootSpeed = 10f;
				if (!Main.dedServ)
					Item.UseSound = AirstrikeSound;
			}
			return base.CanUseItem(player);
		}
		#region Shoot
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2)
			{
				for (int i = 0; i <= 2; i++)
				{
					Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX + Main.rand.NextFloat(-100f, 100f), (float)Main.mouseY);
					float ceilingLimit = target.Y;
					if (ceilingLimit > player.Center.Y - 200f)
					{
						ceilingLimit = player.Center.Y - 200f;
					}

					position = Main.MouseWorld + new Vector2(((-(float)Main.rand.Next(-401, 401) + offsetX) * player.direction), -600f);
					position.Y -= (100 * i);
					Vector2 heading = target - position;
					if (heading.Y < 0f)
					{
						heading.Y *= -1f;
					}
					if (heading.Y < 20f)
					{
						heading.Y = 20f;
					}

					heading.Normalize();
					heading *= new Vector2(velocity.X, velocity.Y).Length();
					velocity.X = heading.X;
					velocity.Y = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
					Projectile.NewProjectile(source, position, velocity, type, 30, knockback, player.whoAmI, 0f, ceilingLimit);

				}
			}
			else
			{
				Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
				float ceilingLimit = target.Y;
				if (ceilingLimit > player.Center.Y - 200f)
				{
					ceilingLimit = player.Center.Y - 200f;
				}

				position = Main.MouseWorld + new Vector2(((-(float)Main.rand.Next(-401, 401) + offsetX) * player.direction), -600f);
				position.Y -= 100;
				Vector2 heading = target - position;
				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}
				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}

				heading.Normalize();
				heading *= new Vector2(velocity.X, velocity.Y).Length();
				velocity.X = heading.X;
				velocity.Y = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, position, velocity, type, 60, knockback, player.whoAmI, 0f, ceilingLimit);
			}

			return false;
		}

		#endregion Shoot

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts, 2)
				.AddIngredient(ModContent.ItemType<P2Processor>(), 10)
				.AddIngredient(ModContent.ItemType<SteelPlate>(), 2)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}