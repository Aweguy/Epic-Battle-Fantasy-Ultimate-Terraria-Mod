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
			Tooltip.SetDefault("Bombs away!!!!\nLeft click to quickly drop bombs down");
		}

		public override void SetDefaults()
		{
			item.damage = 200;
			item.width = 24;
			item.height = 32;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 10;
			item.useAnimation = 10;
			item.mana = 10;
			item.rare = ItemRarityID.Yellow;
			item.useTurn = true;
			item.shoot = mod.ProjectileType("Bomb");
			item.shootSpeed = 16f;
			item.value = Item.sellPrice(gold: 1);
			item.noMelee = true;
			item.magic = true;
			item.autoReuse = true;
		}

		#region Shoot

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			

		    for(int i = 0; i <= 2; i++)
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
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage * 2, knockBack, player.whoAmI, 0f, ceilingLimit);

			}




			return false;
		}

		#endregion Shoot

	

	}
}