using EpicBattleFantasyUltimate.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
	internal sealed class WraithSawblade : ModNPC
	{
		private readonly float moveSpeed = 6f;

		//private readonly float rotationSpeed = 0.13f;
		private int lifetime = 60 * 20;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sawblade");
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
			// You might want to play with the width and the height of the NPC a bit to get the required visuals.
			NPC.width = NPC.height = 36;

			NPC.scale = 1.2f;

			NPC.damage = 40;
			NPC.lifeMax = 100;
			NPC.knockBackResist = 0f;

			NPC.noGravity = true;
			NPC.behindTiles = true;
			NPC.noTileCollide = true;
			NPC.dontTakeDamage = true;

			DrawOffsetY = -2f;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			if (Main.rand.NextFloat(1f) <= 0.4f)
			{
				target.AddBuff(ModContent.BuffType<RampantBleed>(), 60 * 3);
			}
		}

		public override bool PreAI()
		{
			lifetime--;

			if (lifetime <= 0)
			{
				NPC.life = 0;
			}

			// First update only.
			// We want to target the closest player and set directionY to 1.
			// This is because the AI of this NPC is dependent on its 'NPC.direction' and 'NPC.directionY' variables.
			if (NPC.ai[0] == 0f)
			{
				NPC.ai[0] = 1f;

				NPC.TargetClosest();
				NPC.directionY = 1;
			}

			// This 'if' statement and its appended 'else' are responsible for changing which way this NPC is heading.
			// This is done by checking the relevant 'NPC.collideX' and 'NPC.collideY' variables.
			if (NPC.ai[1] == 0f)
			{
				// Increment rotation.
				// If you want a faster or slower rotation, you'll want to change the 'rotationSpeed' value at the top of this class.
				//NPC.rotation += (NPC.direction * NPC.directionY) * rotationSpeed;

				if (NPC.collideY)
				{
					NPC.ai[0] = 2f;
				}
				else if (NPC.ai[0] == 2f)
				{
					NPC.direction *= -1;

					NPC.ai[0] = NPC.ai[1] = 1f;
				}

				if (NPC.collideX)
				{
					NPC.directionY *= -1;

					NPC.ai[1] = 1f;
				}
			}
			else
			{
				// Same here are before, except it's rotating the other way.
				//NPC.rotation -= (NPC.direction * NPC.directionY) * rotationSpeed;

				if (NPC.collideX)
				{
					NPC.ai[0] = 2f;
				}
				else if (NPC.ai[0] == 2f)
				{
					NPC.directionY *= -1;

					NPC.ai[0] = 1f;
					NPC.ai[1] = 0f;
				}

				if (NPC.collideY)
				{
					NPC.direction *= -1;

					NPC.ai[1] = 0f;
				}
			}

			// After all collision and direction checking, set the velocity of this NPC based on those checks using 'NPC.direction' and 'NPC.directionY'.
			NPC.velocity = new Vector2(NPC.direction, NPC.directionY) * moveSpeed;

			return (false);
		}

		public override void PostAI()
		{
			SlopeCollision();
			OffsetCollision();
		}

		public override void FindFrame(int frameHeight)
		{
			if (++NPC.frameCounter >= 2)
			{
				NPC.frameCounter = 0;
				NPC.frame.Y = (NPC.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[NPC.type]);
			}
		}
		// Overriding PreDraw to draw around NPC origin, instead of some arbitrary value.
		// Important to make sure the NPC is being drawn correctly around its own center, so it looks like it's half behind tiles.
		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			Texture2D texture = TextureAssets.Npc[NPC.type].Value;
			Vector2 origin = NPC.frame.Size() / 2;

			SpriteEffects effects = NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			DrawData data = new DrawData(texture, NPC.Center - Main.screenPosition, NPC.frame, drawColor * NPC.Opacity, NPC.rotation, origin, NPC.scale, effects, 0);

			data.Draw(spriteBatch);

			//spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, NPC.frame, drawColor, NPC.rotation, origin, NPC.scale, SpriteEffects.None, 0f);

			return false;
		}
       

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
		}

		private void SlopeCollision()
		{
			Vector4 vector = Collision.WalkDownSlope(NPC.position, NPC.velocity, NPC.width, NPC.height, 0.3f);
			NPC.position = new Vector2(vector.X, vector.Y);
			NPC.velocity = new Vector2(vector.Z, vector.W);
		}

		private void OffsetCollision()
		{
			NPC.oldVelocity = NPC.velocity;
			NPC.collideX = NPC.collideY = false;

			int widthOffset = 12;
			int heightOffset = 12;
			Vector2 pos = NPC.Center - new Vector2(widthOffset / 2, heightOffset / 2);
			NPC.velocity = Collision.noSlopeCollision(pos, NPC.velocity, widthOffset, heightOffset, true, true);

			if (Collision.up)
			{
				NPC.velocity.Y = 0.01f;
			}

			if (NPC.oldVelocity.X != NPC.velocity.X)
			{
				NPC.collideX = true;
			}
			if (NPC.oldVelocity.Y != NPC.velocity.Y)
			{
				NPC.collideY = true;
			}

			NPC.oldPosition = NPC.position;
			NPC.oldDirection = NPC.direction;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
	}
}