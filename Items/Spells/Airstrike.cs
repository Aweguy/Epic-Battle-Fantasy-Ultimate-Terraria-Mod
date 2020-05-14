﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace EpicBattleFantasyUltimate.Items.Spells
{
    public class Airstrike : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Airstrike");
            Tooltip.SetDefault("Bombs away!!!!\nLeft click to quickly drop bombs down\nRight click to drop 10 bombs but set it on cooldown for 5 seconds");
        }

        public override void SetDefaults()
        {
            item.damage = 200;
            item.width = 28;
            item.height = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 10;
            item.useAnimation = 10;
            item.mana = 10;
            item.rare = 8;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Bomb");
            item.shootSpeed = 8f;
			item.value = Item.sellPrice(gold: 1);
			item.noMelee = true;
			item.magic = true;
			item.autoReuse = true;
        }

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}


		public override bool CanUseItem(Player player)
		{
			
			if(player.HasBuff(mod.BuffType("Reloading")))
			{
				return false;
			}
			else
			{
				if (player.altFunctionUse == 2)
				{
					item.damage = 200;
					item.width = 28;
					item.height = 30;
					item.useStyle = ItemUseStyleID.HoldingUp;
					item.useTime = 20;
					item.useAnimation = 20;
					item.mana = 100;
					item.rare = 8;
					item.useTurn = true;
					item.shoot = mod.ProjectileType("Bomb");
					item.shootSpeed = 8f;
					item.noMelee = true;
					item.magic = true;
				}

					
				else
				{
					item.damage = 200;
					item.width = 28;
					item.height = 30;
					item.useStyle = ItemUseStyleID.HoldingUp;
					item.useTime = 30;
					item.useAnimation = 30;
					item.mana = 10;
					item.rare = 8;
					item.useTurn = true;
					item.shoot = mod.ProjectileType("Bomb");
					item.shootSpeed = 8f;
					item.noMelee = true;
					item.magic = true;
					item.autoReuse = true;
				}
				return base.CanUseItem(player);
			}
				





			



		}


		public override bool UseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				player.AddBuff(mod.BuffType("Reloading"), 60 * 5);
			}

			return base.UseItem(player);
		}






		#region Shoot
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			if(player.altFunctionUse == 2)
			{
				player.AddBuff(mod.BuffType("Reloading"), 60 * 5);
				for (int i = 0; i < 10; i++)
				{
					position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -600f);
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
			}
			else
			{
				for (int i = 0; i < 1; i++)
				{
					position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -600f);
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
			}
				
				return false;
		}
        #endregion

    }
}