using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

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
			Projectile.width = 16;
			Projectile.height = 16;
			//Projectile.hide = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.ownerHitCheck = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.aiStyle = -1;
			DrawOriginOffsetY = -30;
			DrawOffsetX = -30;
			AIType = 697;
		}

		#region CutTiles

		public override void CutTiles()
		{
			int num = (int)(Projectile.position.X / 16f);
			int num2 = (int)((Projectile.position.X + (float)Projectile.width) / 16f) + 1;
			int num3 = (int)(Projectile.position.Y / 16f);
			int num4 = (int)((Projectile.position.Y + (float)Projectile.height) / 16f) + 1;
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
					if (Main.tile[i, j] != null && Main.tileCut[(int)Main.tile[i, j].TileType] && WorldGen.CanCutTile(i, j, TileCuttingContext.AttackProjectile))
					{
						WorldGen.KillTile(i, j, false, false, false);
						if (Main.netMode != NetmodeID.SinglePlayer)
						{
							NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
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
			float f = Projectile.rotation - 0.7853982f * (float)Math.Sign(Projectile.velocity.X);
			float num2 = 0f;
			float num3 = 50f;
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center + f.ToRotationVector2() * (0f - num3), Projectile.Center + f.ToRotationVector2() * num3, 23f * Projectile.scale, ref num2))
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
			Player player = Main.player[Projectile.owner];
			float num3 = -0.7853982f; //doesn't do anything. Weird
			Vector2 value = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 value2 = Vector2.Zero;
			if (player.dead)
			{
				Projectile.Kill();
			}
			else
			{
				int explDamage = Projectile.damage * 2; //damage of the explosion
				int num5 = Math.Sign(Projectile.velocity.X); //confused.
				Projectile.velocity = new Vector2((float)num5, 0f);
				if (Projectile.ai[0] == 0f)
				{
					Projectile.rotation = new Vector2((float)num5, 0f - player.gravDir).ToRotation() + num3 + 3.14159274f; //Starting position of rotation
					if (Projectile.velocity.X < 0f)
					{
						Projectile.rotation -= 1.57079637f;
					}
				}
				Projectile.alpha -= 128;
				if (Projectile.alpha < 0)
				{
					Projectile.alpha = 0;
				}
				float num6 = Projectile.ai[0] / RotSpeedCheck;
				float num7 = 1f; //increases the value of Projectile.ai[0] by 1f. Making it higher makes the Projectile not smash. As expected making it lower stalls the explosion. Leave it 1f
				Projectile.ai[0] += num7;
				Projectile.rotation += 6.28318548f * SpinsNumber / RotSpeedCheck * (float)num5; //Projectile sets the rotation speed. That's what RotSpeedCheck and SpinsNumber used for.
				bool flag = Projectile.ai[0] == (float)(int)(RotSpeedCheck / 2f); //Flag check.
				if (Projectile.ai[0] >= RotSpeedCheck || (flag && !player.controlUseItem))
				{
					Projectile.Kill();
					player.reuseDelay = 10;
				}
				else if (flag)
				{
					Vector2 mouseWorld = Main.MouseWorld;
					int num8 = (player.DirectionTo(mouseWorld).X > 0f) ? 1 : (-1); //defines num8 which then tells where the Projectile should look at.
					if ((float)num8 != Projectile.velocity.X)
					{
						player.ChangeDir(num8);
						Projectile.velocity = new Vector2((float)num8, 0f);
						Projectile.netUpdate = true;
						Projectile.rotation -= 3.14159274f;
					}
				}
				float num9 = Projectile.rotation - 0.7853982f * (float)num5;
				value2 = (num9 + ((num5 == -1) ? 3.14159274f : 0f)).ToRotationVector2() * (Projectile.ai[0] / RotSpeedCheck) * scaleFactor; //reach thing
				Vector2 vector = Projectile.Center + (num9 + ((num5 == -1) ? 3.14159274f : 0f)).ToRotationVector2() * 30f;
				if (Main.rand.Next(2) == 0)
				{
					Dust dust = Dust.NewDustDirect(vector - new Vector2(5f), 10, 10, DustID.Smoke, player.velocity.X, player.velocity.Y, 150, default(Color), 1f);
					dust.velocity = Projectile.DirectionTo(dust.position) * 0.1f + dust.velocity * 0.1f;
				}
				if (num6 >= 0.75f)
				{
					Dust dust2 = Dust.NewDustDirect(vector - new Vector2(5f), 10, 10, DustID.Pixie, player.velocity.X, player.velocity.Y, 50, default(Color), 1f);
					dust2.velocity = Projectile.DirectionTo(dust2.position) * 0.1f + dust2.velocity * 0.1f;
					dust2.noGravity = true;
					dust2.color = new Color(20, 255, 100, 160);
				}
				if (Projectile.ai[0] >= RotSpeedCheck - 8f && Projectile.ai[0] < RotSpeedCheck - 2f)
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

				if (Projectile.ai[0] == RotSpeedCheck - 3f && Projectile.owner == Main.myPlayer)
				{
					Point point = default(Point);
					if (Projectile.localAI[1] == 1f || WorldUtils.Find(vector.ToTileCoordinates(), Searches.Chain(new Searches.Down(4), new Conditions.IsSolid()), out point))
					{
						Projectile.NewProjectile(Projectile.GetSource_FromThis(),vector + new Vector2((float)(num5 * 20), -60f), Vector2.Zero, ProjectileID.MonkStaffT1Explosion, explDamage, 0f, Projectile.owner, 0f, 0f);

						for (int i = 0; i < 20; i++)
						{
							Vector2 vel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 0f));
							if (vel.Length() < 3) vel = Vector2.Normalize(vel) * 3f;   //minimum speed
							{
								Projectile.NewProjectile(Projectile.GetSource_FromThis(),vector + new Vector2((float)(num5 * 20), 3f), vel, ModContent.ProjectileType<GashSpike>(), 20, 0, Projectile.owner, 0, 1);
							}
						}
						SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundImpact, Projectile.Center);
					}
					else
					{
						SoundEngine.PlaySound(SoundID.DD2_MonkStaffGroundMiss, Projectile.Center);
					}
				}

				#endregion Projectile Summoning

				Projectile.position = value - Projectile.Size / 2f;
				Projectile.position += value2;
				Projectile.spriteDirection = Projectile.direction;
				Projectile.timeLeft = 2;
				player.ChangeDir(Projectile.direction);
				player.heldProj = Projectile.whoAmI;
				player.itemTime = 2;
				player.itemAnimation = 2;
				player.itemRotation = MathHelper.WrapAngle(Projectile.rotation);
			}
		}
	}
}