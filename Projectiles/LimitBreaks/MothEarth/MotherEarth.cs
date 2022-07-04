#region using

using EpicBattleFantasyUltimate.Buffs.Buffs;
using EpicBattleFantasyUltimate.Dusts;
using EpicBattleFantasyUltimate.HelperClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

#endregion using

namespace EpicBattleFantasyUltimate.Projectiles.LimitBreaks.MothEarth
{
	public class MotherEarth : ModProjectile
	{
		private int StartDamage = 60 * 1; //When it will start damaging enemies.
		private int DamageTimer = 30; //The interval between each damage tick
		private bool Shaded = false;//If the cheaty shader Projectile has spawned

		#region Breathing Variables

		private int BreatheInTimer = 30;//How much it will breathe in.
		private int BreatheOutTimer = 30;//How much it will breathe out.
		private bool BreatheIn = false;//When it will scale down a bit.
		private bool BreatheOut = true;//When it will scale up a bit .

		#endregion Breathing Variables

		#region End Animation Variables

		private int EndDamage = 60 * 7;//When the end animation will start.
		private bool EndParticles = false;//When the leaves will be launched.
		private float[] scaleCache = new float[7]; //After effect based on scale.

		#endregion End Animation Variables

		#region Nature Blast Variables

		private int BlastTimer = 0;// The timer that summons the Projectiles around the symbol

		#endregion Nature Blast Variables

		#region Animation Dust Variables

		public int timer = 60 * 7;
		private List<Dust> effectDusts = new List<Dust>(); // create a list of dust object
		private float dustSpeed = 2.5f; //how fast the dust moves
		private float dustBoost = 3.25f; //The offset from the center from which the dust will spawn
		private int cooldownTime = 100;
		private int dustSpawnTime = 50; //how many frames after starting that the dust will be spawning for
		private int dustSpawnRate = 1; //bigger = more dust, total dust is dustSpawnTime*dustSpawnRate

		#endregion Animation Dust Variables

		#region Leaf Dust variables

		private List<Dust> effectDusts2 = new List<Dust>(); // create a list of dust object
		private float dustSpeed2 = 5f; //how fast the dust moves
		private float dustBoost2 = 2f; //The offset from the center from which the dust will spawn
		private int dustSpawnRate2 = 100; //bigger = more dust, total dust is dustSpawnTime*dustSpawnRate

		#endregion Leaf Dust variables

		#region Glowmask Variables

		int GlowmaskOpacity = 120;
		float GlowmaskScale = 1f;

		#endregion


		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			Projectile.width = 1;
			Projectile.height = 1;

			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Magic;

			Projectile.tileCollide = false;

			Projectile.scale = 1.6f;
			Projectile.alpha = 255;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];

			Projectile.position = Main.screenPosition + new Vector2(Main.screenWidth / 2, (Main.screenHeight / 2) - 256);

			if (StartDamage-- > 0)
			{
				for (int i = 0; i < Player.MaxBuffs; ++i)//Clear debuffs
				{
					if (player.buffType[i] != 0 && Main.debuff[player.buffType[i]])
					{
						player.DelBuff(i);
						i--;
					}
				}

				player.AddBuff(ModContent.BuffType<BlessedBuff>(), 60 * 10);

				Projectile.scale -= 0.01f;
				Projectile.alpha -= 204 / 60;
			}

			if (EndDamage > 0) //Trailing in the start
			{
				for (int i = scaleCache.Length - 1; i > 0; --i)
				{
					scaleCache[i] = scaleCache[i - 1];
				}
				scaleCache[0] = Projectile.scale;
			}

			if (StartDamage <= 0)//Runs when it starts damaging
			{
                if (!Shaded)//Spawning the shader cheaty Projectile
                {
					Shaded = true;

					Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ShaderProjectile>(), 0, 0, player.whoAmI, Projectile.Center.X, Projectile.Center.Y);
                }

				Particles();//Dust while it's alive.

				NatureBlasts(player);//Projectile Spawning

				Breathing();//The little scale up scale down effect

				EndDamage--;//reducing the timer so the ending animation starts

				GlowmaskOpacity += 2;//glowmask update for the opacity
				GlowmaskScale += 0.015f;//glowmask update for the size

				if (DamageTimer-- <= 0 && EndDamage > 0)
				{
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						NPC npc = Main.npc[i];

						if(npc.active && !npc.dontTakeDamage && !npc.townNPC)
						{
							Damage(npc, player); //The constant damage causing
						}
					}
				}
				else if (EndDamage <= 0)
				{
					EndAnimation(); //The end animationa and dust
				}
			}

			

			return false;
		}

		private void Breathing()
		{
			if (BreatheIn)
			{
				Projectile.scale -= 0.0005f;
				BreatheOutTimer--;
			}
			else if (BreatheOut)
			{
				Projectile.scale += 0.0005f;
				BreatheInTimer--;
			}

			if (BreatheInTimer <= 0)
			{
				BreatheIn = true;
				BreatheOut = false;
				BreatheInTimer = 30;
			}

			if (BreatheOutTimer <= 0)
			{
				BreatheOut = true;
				BreatheIn = false;
				BreatheOutTimer = 30;
			}
		}

		private void NatureBlasts(Player player)
		{
			if (BlastTimer-- <= 0)
			{
				Vector2 origin = new Vector2(Projectile.Center.X, Projectile.Center.Y);

				Projectile.NewProjectile(Projectile.GetSource_FromThis(), origin, Vector2.Zero, ModContent.ProjectileType<NatureBlast>(), 24, 2, Main.myPlayer, Projectile.Center.X, Projectile.Center.Y);

				BlastTimer = 15;
			}
		}

		private void Damage(NPC npc, Player player)
		{
			npc.AddBuff(BuffID.Poisoned, 60 * 600);

			if (player.whoAmI == Main.myPlayer)
			{
				if (NPC.downedMoonlord)
				{
					player.ApplyDamageToNPC(npc, Projectile.damage + 1000, 0f, (npc.Center.X - player.Center.X > 0f).ToDirectionInt(), true);

				}
				else if (NPC.downedGolemBoss)
				{
					player.ApplyDamageToNPC(npc, Projectile.damage + 500, 0f, (npc.Center.X - player.Center.X > 0f).ToDirectionInt(), true);

				}
				else if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
				{
					player.ApplyDamageToNPC(npc, Projectile.damage + 300, 0f, (npc.Center.X - player.Center.X > 0f).ToDirectionInt(), true);

				}
				else if (Main.hardMode)
				{
					player.ApplyDamageToNPC(npc, Projectile.damage + 200, 0f, (npc.Center.X - player.Center.X > 0f).ToDirectionInt(), true);
				}
				else
				{
					player.ApplyDamageToNPC(npc, Projectile.damage + 100, 0f, (npc.Center.X - player.Center.X > 0f).ToDirectionInt(), true);

				}

				DamageTimer = 60;
			}
		}

		private void Particles()
		{
			Vector2 origin = new Vector2(Projectile.Center.X, Projectile.Center.Y);
			if (timer-- > 0)
			{
				if (timer > cooldownTime - dustSpawnTime)
				{
					for (int i = 0; i < dustSpawnRate; i++)
					{
						float rot = Main.rand.NextFloat() * (float)Math.PI * 2; //random angle
						effectDusts.Add(Dust.NewDustPerfect(origin + (new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * dustBoost) * 20f, ModContent.DustType<NatureDust>(), new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * dustSpeed, 0, new Color(255, 255, 255), 2.5f)); //add new dust to list
						effectDusts[effectDusts.Count - 1].noGravity = true;  //modify the newly created dust
						effectDusts[effectDusts.Count - 1].scale = 0.5f;
					}
				}

				//Circular Dust spawn code not necessary here
				/*for (int d = 0; d < effectDusts.Count; d++)
				{
					effectDusts[d].scale = 1;
					/*if (timer < ringRadius / dustSpeed) //when the timer is about to run out
					{
						effectDusts[d].velocity = (origin - effectDusts[d].position).SafeNormalize(Vector2.UnitY) * dustSpeed; // move dust toward center
					}
					else
					{
						if ((origin - effectDusts[d].position).Length() > ringRadius)
						{
							float tangentDirection = (origin - effectDusts[d].position).ToRotation() + (float)Math.PI / 2 + (d % 2 == 0 ? 0 : (float)Math.PI); //the direction the dust will fly in
							effectDusts[d].velocity = new Vector2((float)Math.Cos(tangentDirection), (float)Math.Sin(tangentDirection)) * dustSpeed; // change dust velocity
						}
					}
				}
				if (timer == 1)
				{
					Projectile.Kill();
				}
			}
			else
			{
				for (int d = 0; d < effectDusts.Count; d++)
				{
					effectDusts[d].scale = .1f; // make the dust almost disapear
				}
				effectDusts.Clear(); //empty out the list*/
			}
		}

		private void EndAnimation()
		{
			if (EndParticles == false)
			{
				Vector2 origin = new Vector2(Projectile.Center.X, Projectile.Center.Y);

				for (int i = 0; i < dustSpawnRate2; i++)
				{
					float rot = Main.rand.NextFloat() * (float)Math.PI * 2; //random angle

					dustSpeed2 = Main.rand.NextFloat(3f, 10f);

					effectDusts2.Add(Dust.NewDustPerfect(origin + (new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * dustBoost2) * 20f, ModContent.DustType<NatureLeaves>(), new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * dustSpeed2, 0, new Color(255, 255, 255), 2.5f)); //add new dust to list
					effectDusts2[effectDusts2.Count - 1].noGravity = true;  //modify the newly created dust
					effectDusts2[effectDusts2.Count - 1].scale = 1f;
				}

				EndParticles = true;
			}

			if (++Projectile.localAI[0] >= 3)// Trailing in the end animation
			{
				Projectile.localAI[0] = 0;

				for (int i = scaleCache.Length - 1; i > 0; --i)
				{
					scaleCache[i] = scaleCache[i - 1];
				}
				scaleCache[0] = Projectile.scale;
			}

			Projectile.scale += 0.005f;
			Projectile.alpha += 204 / 60;

			if (Projectile.alpha >= 255)
			{
				Projectile.Kill();
			}
		}
        public override bool PreDraw(ref Color lightColor)
        {
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			Rectangle frame = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
			Vector2 origin = frame.Size() / 2;
			SpriteEffects effects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			#region Shading

			/*Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			DrawData data = new DrawData(texture, Projectile.Center - Main.screenPosition, null, lightColor * Projectile.Opacity, Projectile.rotation, origin, Projectile.scale, effects, 0);
			GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(ItemID.AcidDye), Projectile, data);

			data.Draw(spriteBatch);*/

			#endregion Shading

			#region Trailing

			float initialOpacity = 0.8f;
			float opacityDegrade = 0.2f;

			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
			{
				float opacity = initialOpacity - opacityDegrade * i;
				Main.spriteBatch.Draw(texture, Projectile.position + Projectile.Hitbox.Size() / 2 - Main.screenPosition, frame, lightColor * Projectile.Opacity, Projectile.rotation, origin, scaleCache[i], effects, 0f);
			}

			#endregion Trailing

			return this.DrawProjectileCentered(Main.spriteBatch, lightColor * Projectile.Opacity);
		}

        public override void PostDraw(Color lightColor)
        {
			Texture2D texture = ModContent.Request<Texture2D>("EpicBattleFantasyUltimate/Projectiles/LimitBreaks/MothEarth/MotherEarth_Glowmask").Value;

			if (GlowmaskOpacity < 255 && StartDamage <= 0)
			{

				Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, texture.Width, texture.Height), Color.White * ((255 - GlowmaskOpacity) / 255f), Projectile.rotation, texture.Size() / 2, Projectile.scale * GlowmaskScale, SpriteEffects.None, 0);
			}
			#region Shading

			//Main.spriteBatch.End();
			//Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);

			#endregion Shading
		}
		
	}
	
}