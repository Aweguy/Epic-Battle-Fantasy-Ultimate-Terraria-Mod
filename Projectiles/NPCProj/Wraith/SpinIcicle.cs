using EpicBattleFantasyUltimate.HelperClasses;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
	public class SpinIcicle : ModProjectile
	{
		private int OrbitTimer;//How many ticks it will orbit the player
		private float Distance = 240;//The distance of the Projectile from the player target.
		private bool shoot = false;//The bool that makes it not follow the player after launched. Sets its velocity to the last player's position.
		private bool Orbit = false;//Decides how many ticks each icicle will orbit the player.
		private bool Frame = false;//The bool that determines the texture of the icicle

		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 5;
			Projectile.height = 5;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 60 * 25;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.tileCollide = false;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Chilled, 2 * 60);
		}

		public override bool PreAI()
		{
			Color drawColor = Color.Orange;
			if (Main.rand.Next(2) == 0)
			{
				drawColor = Color.Red;
			}

			NPC npc = Main.npc[(int)Projectile.ai[0]]; //Sets the npc that the Projectile is spawned and will orbit

			if (!npc.active)
			{
				Projectile.Kill();
			}

			if (npc.life <= 0)
			{
				Projectile.Kill();
			}

			if (Orbit == false)
			{
				// Again, networking compatibility.
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					Projectile.netUpdate = true;
					OrbitTimer = Main.rand.Next(60 * 8, 60 * 13);
				}

				Orbit = true;
			}

			if (--OrbitTimer >= 0)
			{
				Projectile.DoProjectile_OrbitPosition(Main.player[npc.target].Center, Distance, MathHelper.PiOver2);
				Projectile.rotation = (Projectile.Center - Main.player[npc.target].Center).ToRotation();
			}
			else
			{
				if (!shoot)
				{
					Projectile.velocity = Projectile.DirectionTo(Main.player[npc.target].Center) * 10f;//sets the velocity of the Projectile.
					Projectile.netUpdate = true; // Eldrazi: Multiplayer compatibility.
					shoot = true;
				}
			}

			if (Frame == false)
			{
				FrameCheck();
				Frame = true;
			}

			return (false);
		}

		private void FrameCheck()
		{
			if (!Frame)
			{
				Projectile.frame = Main.rand.Next(0, 3);

				Frame = true;
			}
		}

        /*public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.ProjectileTexture[Projectile.type];

			spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 48, 48, 48), Color.White, Projectile.rotation, new Vector2(24, 24), Projectile.scale, SpriteEffects.None, 0);

			return false;
		}*/

        #region Networking

        public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(OrbitTimer);
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			OrbitTimer = reader.ReadInt32();
		}

		#endregion Networking
	}
}