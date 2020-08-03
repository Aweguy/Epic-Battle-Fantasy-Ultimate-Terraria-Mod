using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.RedFlybot
{
    public class RedCannonBehind : ModProjectile
    {

		int ShootTimer = 60;//Determines when the cannon will shoot
		int damage;//The damage of the projectiles
		int ShotNum = 0;//Number of shots
		int ShootInterv = 30;//The interval between shots
		bool Shoot = false;//Determines if the cannon will shoot
		Vector2 velocity;


		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Cannon");
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 25;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.ranged = true;
            projectile.knockBack = 1f;
			projectile.hide = true;
			projectile.tileCollide = false;
        }


        public override void AI()
        {
			NPC npc = Main.npc[(int)projectile.ai[0]]; //Sets the npc that the projectile is spawned and will orbit

			Player target = Main.player[npc.target];

			projectile.Center = new Vector2(npc.Center.X + 9 * npc.spriteDirection , npc.Center.Y + 22);

			projectile.rotation = (projectile.Center - target.Center).ToRotation();

			projectile.timeLeft = 1000;



			if (npc.life <= 0)
			{
				projectile.Kill();
			}

			ShootTimer--;

			if (ShootTimer <= 0 && ShotNum < 3)
			{


				if (ShotNum < 2)
				{
					velocity = projectile.DirectionTo(target.Center) * 10f;//sets the velocity of the projectile.
					damage = 30;

				}
				else if (ShotNum == 2)
				{
					velocity = projectile.DirectionTo(target.Center) * 20f;//sets the velocity of the projectile.
					damage = 60;
				}






				ShotNum++;

				Projectile.NewProjectile(projectile.Center, velocity, ModContent.ProjectileType<RedLaser>(), damage, 10, Main.myPlayer, 0, 1);
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Flybots/SnipeShot").WithPitchVariance(.2f).WithVolume(.5f), projectile.position);



				if (ShotNum < 2)
				{

					ShootTimer = 30;
				}
				else if (ShotNum == 2)
				{
					ShootTimer = 60;
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Flybots/SnipeTarget").WithPitchVariance(.2f).WithVolume(.7f), projectile.position);

				}
				else
				{
					ShootTimer = 300;
					ShotNum = 0;
				}
			}

		}







		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindNPCs.Add(index);
        }







    }
}
