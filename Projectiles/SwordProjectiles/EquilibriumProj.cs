using EpicBattleFantasyUltimate.HelperClasses;
using EpicBattleFantasyUltimate.Items.Swords;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
	public class EquilibriumProj : ModProjectile
	{
		float Offset = 40f;//The offset between the projectile's center and the player's
		int SlashCooldown = 10;//The cooldown between each slash.

		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 28;
		}
		public override void SetDefaults()
		{
			Projectile.width = 48;
			Projectile.height = 44;

			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;

			Projectile.tileCollide = false;

			Projectile.localNPCHitCooldown = 5;
			Projectile.usesLocalNPCImmunity = true;

			DrawOriginOffsetX = 25;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if(--SlashCooldown <= 0)//The cooldown between slashing the npcs
			{
                float rotation = Main.rand.NextFloat(360);//Random rotation, for aesthetic purposes
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<EquilibriumSlash>(), Projectile.damage, 0, Projectile.owner, rotation);//Spawning the slash on the npc hit
            }

        }

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];//Setting the player owner to be used in further calculations

			player.direction = Projectile.direction;//Correcting player direction to follow the projectile's

			if (player.channel && player.CCed != true)//If the player is channeling and is not CCed, keep the projectile close to the player's centre
			{

				Projectile.rotation = (Vector2.Normalize(Main.MouseWorld - player.Center)).ToRotation();//Setting the rotation towards the mouse. Used in the line below

				Projectile.velocity = Vector2.Normalize(Main.MouseWorld - player.Center);//Making the projectile's angle to rotate towards the mouse cursor

				Projectile.Center = player.Center + Vector2.Normalize(Projectile.velocity) * Offset;//Using the offset to push it a bit further away from the player's centre
			}
			else//if player stops channeling the projectile goes away
			{
				Projectile.Kill();
			}
			//Animating the projectile's sprite
			#region Animation
			if (--Projectile.frameCounter <= 0)
			{
				Projectile.frameCounter = 2;
				if (++Projectile.frame >= 27)
				{
					Projectile.frame = 0;
				}
			}
			#endregion


			return false;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			return this.DrawProjectileCentered(Main.spriteBatch, lightColor);//Centering the origin point of the projectile.
		}
	}
}
