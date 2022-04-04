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
	public class PaintSplatteredBrush : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Paint Splattered B(r)ush");
			Tooltip.SetDefault("Great for impersonating artists!...\n \nAlso plumbers, for whatever reason\n[c/FFE800:By Speed Dynamo]");
		}

		public static List<int> BrushProj = new List<int> { ModContent.ProjectileType<RedBall>(), ModContent.ProjectileType<GreyBall>(), ModContent.ProjectileType<YellowBall>(), ModContent.ProjectileType<GreenBall>(), ModContent.ProjectileType<BlackBall>(), ModContent.ProjectileType<WhiteBall>(), ModContent.ProjectileType<BlueBall>(), ModContent.ProjectileType<IndigoBall>(), ModContent.ProjectileType<VioletBall>(), ModContent.ProjectileType<OrangeBall>(), };

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;

			Item.damage = 10;
			Item.knockBack = 3f;
			Item.DamageType = DamageClass.Melee;;

			Item.useTime = 10;
			Item.useAnimation = 9;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.autoReuse = true;
			Item.channel = true;

			Item.shootSpeed = 20f;
			Item.shoot = ModContent.ProjectileType<RedBall>();

			Item.rare = ItemRarityID.Expert;
		}

		#region Shoot

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(5));
			int Ball = Main.rand.Next(BrushProj.ToArray());
			Projectile.NewProjectile(source, player.Center, perturbedSpeed, Ball, Item.damage, Item.knockBack, player.whoAmI);
			return false;
		}

		#endregion Shoot
	}
}