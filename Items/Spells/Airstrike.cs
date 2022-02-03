using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.SpellProjectiles.Airstrikes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Spells
{
	public class Airstrike : ModItem
	{
		private float offsetX = 20f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Airstrike");
			Tooltip.SetDefault("Bombs away!!!!\nLeft click to quickly drop bombs down, right click to drop 3 weaker bombs at once.");
		}

		public override void SetDefaults()
		{
			item.damage = 80;
			item.width = 24;
			item.height = 32;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 10;
			item.useAnimation = 10;
			item.mana = 10;
			item.rare = ItemRarityID.Yellow;
			item.useTurn = true;
			item.shoot = ModContent.ProjectileType<Bomb>();
			item.shootSpeed = 16f;
			item.value = Item.sellPrice(gold: 1);
			item.noMelee = true;
			item.magic = true;
			item.autoReuse = true;
			if (!Main.dedServ)
				item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Airstrike").WithVolume(.5f);
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.useTime = 40;
				item.useAnimation = 40;
				item.damage = 30;
				item.mana = 30;
				item.shoot = ModContent.ProjectileType<SmallBomb>();
				item.shootSpeed = 16f;
				if (!Main.dedServ)
					item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Airstrike").WithVolume(.5f);
			}
			else
			{
				item.useTime = 20;
				item.useAnimation = 20;
				item.mana = 10;
				item.shoot = ModContent.ProjectileType<Bomb>();
				item.shootSpeed = 10f;
				if (!Main.dedServ)
					item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Airstrike").WithVolume(.5f);
			}
			return base.CanUseItem(player);
		}
		#region Shoot

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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
						heading *= new Vector2(speedX, speedY).Length();
						speedX = heading.X;
						speedY = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
						Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, 30, knockBack, player.whoAmI, 0f, ceilingLimit);

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
					heading *= new Vector2(speedX, speedY).Length();
					speedX = heading.X;
					speedY = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, 60, knockBack, player.whoAmI, 0f, ceilingLimit);
			}

			return false;
		}

		#endregion Shoot

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddIngredient(ModContent.ItemType<P2Processor>(),5);
			recipe.AddIngredient(ModContent.ItemType<SteelPlate>());
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}