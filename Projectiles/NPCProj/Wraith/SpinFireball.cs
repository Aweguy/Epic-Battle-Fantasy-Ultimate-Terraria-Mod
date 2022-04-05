using EpicBattleFantasyUltimate.HelperClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
	public class SpinFireball : ModProjectile
	{
		// How many ticks it will orbit the npc.
		private int OrbitTimer;

		// The distance of the Projectile from the npc that is spawned.
		private float Distance = 90;

		// The bool that makes it not follow the player after launched. Sets its velocity to the last player's position.
		private bool shoot = false;

		// Decides how many ticks each fireball will orbit the wraith.
		private bool Orbit = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spinning Fireball");
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 48;

			Projectile.penetrate = 1;
			Projectile.timeLeft = 60 * 25;

			Projectile.friendly = true;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.tileCollide = false;

			Orbit = false;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 2 * 60);
		}

		public override bool PreAI()
		{
			Color drawColor = Color.Orange;
			if (Main.rand.Next(2) == 0)
			{
				drawColor = Color.Red;
			}
			Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Firefly, 0f, 0f, 0, drawColor, 0.8f);

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
				Projectile.DoProjectile_OrbitPosition(npc.Center, Distance, MathHelper.PiOver2);
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

			if (++Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame == 2)
				{
					Projectile.frame = 0;
				}
			}

			return (false);
		}
        public override bool PreDraw(ref Color lightColor)
        {
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 48, 48, 48), Color.White, Projectile.rotation, new Vector2(24, 24), Projectile.scale, SpriteEffects.None, 0);

			return false;
		}
        

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