using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace EpicBattleFantasyUltimate.Projectiles.SpearProjectiles
{
	public class GashClubProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gash Club");
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			//projectile.hide = true;
			projectile.melee = true;
			projectile.ownerHitCheck = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.aiStyle = -1;
			drawOriginOffsetY = -30;
			drawOffsetX = -30;
			aiType = 697;
		}

		#region CutTiles

		public override void CutTiles()
		{
			int num = (int)(projectile.position.X / 16f);
			int num2 = (int)((projectile.position.X + (float)projectile.width) / 16f) + 1;
			int num3 = (int)(projectile.position.Y / 16f);
			int num4 = (int)((projectile.position.Y + (float)projectile.height) / 16f) + 1;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			AchievementsHelper.CurrentlyMining = true;
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (Main.tile[i, j] != null && Main.tileCut[Main.tile[i, j].type] && WorldGen.CanCutTile(i, j, TileCuttingContext.AttackProjectile))
					{
						WorldGen.KillTile(i, j, false, false, false);
						if (Main.netMode != NetmodeID.SinglePlayer)
						{
							NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
						}
					}
				}
			}
		}

		#endregion CutTiles

		#region Colliding

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (projHitbox.Intersects(targetHitbox))
			{
				return true;
			}
			float f = projectile.rotation - 0.7853982f * (float)Math.Sign(projectile.velocity.X);
			float num2 = 0f;
			float num3 = 50f;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center + f.ToRotationVector2() * (0f - num3), projectile.Center + f.ToRotationVector2() * num3, 23f * projectile.scale, ref num2))
			{
				return true;
			}

			return false;
		}

		#endregion Colliding

		public override void AI()
		{
			float RotSpeedCheck = 50f; //something about rotation speed? Higher means slower rotation. Also it defines the check, again. 50f defauly
			float SpinsNumber = 1f;  //Number of spins. 2f default
			float scaleFactor = 20f; //Increases Reach. Making it large makes it act weirdly
			Player player = Main.player[projectile.owner];
			float num3 = -0.7853982f; //doesn't do anything. Weird
			Vector2 value = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 value2 = Vector2.Zero;
			if (player.dead)
			{
				projectile.Kill();
			}
			else
			{
				int explDamage = projectile.damage * 2; //damage of the explosion
				int num5 = Math.Sign(projectile.velocity.X); //confused.
				projectile.velocity = new Vector2((float)num5, 0f);
				if (projectile.ai[0] == 0f)
				{
					projectile.rotation = new Vector2((float)num5, 0f - player.gravDir).ToRotation() + num3 + 3.14159274f; //Starting position of rotation
					if (projectile.velocity.X < 0f)
					{
						projectile.rotation -= 1.57079637f;
					}
				}
				projectile.alpha -= 128;
				if (projectile.alpha < 0)
				{
					projectile.alpha = 0;
				}
				float num6 = projectile.ai[0] / RotSpeedCheck;
				float num7 = 1f; //increases the value of projectile.ai[0] by 1f. Making it higher makes the projectile not smash. As expected making it lower stalls the explosion. Leave it 1f
				projectile.ai[0] += num7;
				projectile.rotation += 6.28318548f * SpinsNumber / RotSpeedCheck * (float)num5; //projectile sets the rotation speed. That's what RotSpeedCheck and SpinsNumber used for.
				bool flag = projectile.ai[0] == (float)(int)(RotSpeedCheck / 2f); //Flag check.
				if (projectile.ai[0] >= RotSpeedCheck || (flag && !player.controlUseItem))
				{
					projectile.Kill();
					player.reuseDelay = 10;
				}
				else if (flag)
				{
					Vector2 mouseWorld = Main.MouseWorld;
					int num8 = (player.DirectionTo(mouseWorld).X > 0f) ? 1 : (-1); //defines num8 which then tells where the projectile should look at.
					if ((float)num8 != projectile.velocity.X)
					{
						player.ChangeDir(num8);
						projectile.velocity = new Vector2((float)num8, 0f);
						projectile.netUpdate = true;
						projectile.rotation -= 3.14159274f;
					}
				}
				float num9 = projectile.rotation - 0.7853982f * (float)num5;
				value2 = (num9 + ((num5 == -1) ? 3.14159274f : 0f)).ToRotationVector2() * (projectile.ai[0] / RotSpeedCheck) * scaleFactor; //reach thing
				Vector2 vector = projectile.Center + (num9 + ((num5 == -1) ? 3.14159274f : 0f)).ToRotationVector2() * 30f;
				if (Main.rand.Next(2) == 0)
				{
					Dust dust = Dust.NewDustDirect(vector - new Vector2(5f), 10, 10, DustID.Smoke, player.velocity.X, player.velocity.Y, 150, default(Color), 1f);
					dust.velocity = projectile.DirectionTo(dust.position) * 0.1f + dust.velocity * 0.1f;
				}
				if (num6 >= 0.75f)
				{
					Dust dust2 = Dust.NewDustDirect(vector - new Vector2(5f), 10, 10, DustID.Pixie, player.velocity.X, player.velocity.Y, 50, default(Color), 1f);
					dust2.velocity = projectile.DirectionTo(dust2.position) * 0.1f + dust2.velocity * 0.1f;
					dust2.noGravity = true;
					dust2.color = new Color(20, 255, 100, 160);
				}
				if (projectile.ai[0] >= RotSpeedCheck - 8f && projectile.ai[0] < RotSpeedCheck - 2f)
				{
					for (int i = 0; i < 5; i++)
					{
						Dust dust3 = Dust.NewDustDirect(vector - new Vector2(5f), 10, 10, DustID.Pixie, player.velocity.X, player.velocity.Y, 50, default(Color), 1f);
						dust3.velocity *= 1.2f;
						dust3.noGravity = true;
						dust3.scale += 0.1f;
						dust3.color = new Color(20, 255, 100, 160);
					}
				}

				#region Projectile Summoning

				if (projectile.ai[0] == RotSpeedCheck - 3f && projectile.owner == Main.myPlayer)
				{
					Point point = default(Point);
					if (projectile.localAI[1] == 1f || WorldUtils.Find(vector.ToTileCoordinates(), Searches.Chain(new Searches.Down(4), new Conditions.IsSolid()), out point))
					{
						Projectile.NewProjectile(vector + new Vector2((float)(num5 * 20), -60f), Vector2.Zero, ProjectileID.MonkStaffT1Explosion, explDamage, 0f, projectile.owner, 0f, 0f);

						for (int i = 0; i < 20; i++)
						{
							Vector2 vel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 0f));
							if (vel.Length() < 3) vel = Vector2.Normalize(vel) * 3f;   //minimum speed
							{
								Projectile.NewProjectile(vector + new Vector2((float)(num5 * 20), 3f), vel, ModContent.ProjectileType<GashSpike>(), 20, 0, projectile.owner, 0, 1);
							}
						}
						Main.PlayTrackedSound(SoundID.DD2_MonkStaffGroundImpact, projectile.Center);
					}
					else
					{
						Main.PlayTrackedSound(SoundID.DD2_MonkStaffGroundMiss, projectile.Center);
					}
				}

				#endregion Projectile Summoning

				projectile.position = value - projectile.Size / 2f;
				projectile.position += value2;
				projectile.spriteDirection = projectile.direction;
				projectile.timeLeft = 2;
				player.ChangeDir(projectile.direction);
				player.heldProj = projectile.whoAmI;
				player.itemTime = 2;
				player.itemAnimation = 2;
				player.itemRotation = MathHelper.WrapAngle(projectile.rotation);
			}
		}
	}
}