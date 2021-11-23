using EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.PaintSplatteredBrush;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.SignatureItems
{
	public class PaintSplatteredBrush : ModItem, IPlayerLayerDrawable
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Paint Splattered B(r)ush");
			Tooltip.SetDefault("Great for impersonating artists!...\n \nAlso plumbers, for whatever reason\n[c/FFE800:By Speed Dynamo]");
		}

		public static List<int> BrushProj = new List<int> { ModContent.ProjectileType<RedBall>(), ModContent.ProjectileType<GreyBall>(), ModContent.ProjectileType<YellowBall>(), ModContent.ProjectileType<GreenBall>(), ModContent.ProjectileType<BlackBall>(), ModContent.ProjectileType<WhiteBall>(), ModContent.ProjectileType<BlueBall>(), ModContent.ProjectileType<IndigoBall>(), ModContent.ProjectileType<VioletBall>(), ModContent.ProjectileType<OrangeBall>(), };

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;

			item.damage = 10;
			item.knockBack = 3f;
			item.melee = true;

			item.useTime = 10;
			item.useAnimation = 9;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.autoReuse = true;
			item.channel = true;

			item.shootSpeed = 20f;
			item.shoot = ModContent.ProjectileType<RedBall>();

			item.rare = ItemRarityID.Expert;
		}

		#region Shoot

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
			int Ball = Main.rand.Next(BrushProj.ToArray());
			Projectile.NewProjectile(player.Center, perturbedSpeed, Ball, item.damage, 3f, player.whoAmI);

			return false;
		}

		#endregion Shoot

		#region DrawGlowMask

		public void DrawGlowmask(PlayerDrawInfo info)
		{
			Player player = info.drawPlayer; //the player!

			Texture2D tex;

			float rotation;
			int direction;

			if (player.itemAnimation > 0)
			{
				if (player.direction == -1)
				{
					tex = mod.GetTexture("Items/SignatureItems/PaintSplatteredBrush");
					direction = 1;
					rotation = player.itemRotation + MathHelper.Pi / 4;
				}
				else
				{
					tex = mod.GetTexture("Items/SignatureItems/PaintSplatteredBrush1");
					direction = -1;
					rotation = player.itemRotation + MathHelper.Pi + 5.5f;
				}

				Main.playerDrawData.Add(
				   new DrawData(
					   tex, //pass our glowmask's texture
					   info.itemLocation - Main.screenPosition, //pass the position we should be drawing at from the PlayerDrawInfo we pass into this method. Always use this and not player.itemLocation.
					   tex.Frame(), //our source rectangle should be the entire frame of our texture. If our mask was animated it would be the current frame of the animation.
					   Color.White, //since we want our glowmask to glow, we tell it to draw with Color.White. This will make it ignore all lighting
					   rotation, //the rotation of the player's item based on how they used it. This allows our glowmask to rotate with swingng swords or guns pointing in a direction.
					   new Vector2(player.direction == direction ? 0 : tex.Width, tex.Height), //the origin that our mask rotates about. This needs to be adjusted based on the player's direction, thus the ternary expression.
					   player.HeldItem.scale, //scales our mask to match the item's scale
					   info.spriteEffects, //the PlayerDrawInfo that was passed to this will tell us if we need to flip the sprite or not.
					   0 //we dont need to worry about the layer depth here
				   ));
			}
		}

		#endregion DrawGlowMask
	}
}