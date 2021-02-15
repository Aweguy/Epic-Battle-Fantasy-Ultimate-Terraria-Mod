using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
    internal sealed class WraithSawblade : ModNPC
    {
        private readonly float moveSpeed = 6f;
        private readonly float rotationSpeed = 0.13f;
        private int lifetime = 60 * 20;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sawblade");
            //Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            // You might want to play with the width and the height of the NPC a bit to get the required visuals.
            npc.width = npc.height = 32;

            npc.scale = 1.2f;

            npc.damage = 40;
            npc.lifeMax = 100;
            npc.knockBackResist = 0f;

            npc.noGravity = true;
            npc.behindTiles = true;
            npc.noTileCollide = true;
            npc.dontTakeDamage = true;

            drawOffsetY = -2f;
        }

        public override bool PreAI()
        {

            lifetime--;

            if(lifetime <= 0)
            {
                npc.life = 0;
            }

            // First update only.
            // We want to target the closest player and set directionY to 1.
            // This is because the AI of this NPC is dependent on its 'npc.direction' and 'npc.directionY' variables.
            if (npc.ai[0] == 0f)
            {
                npc.ai[0] = 1f;

                npc.TargetClosest();
                npc.directionY = 1;
            }

            // This 'if' statement and its appended 'else' are responsible for changing which way this NPC is heading.
            // This is done by checking the relevant 'npc.collideX' and 'npc.collideY' variables.
            if (npc.ai[1] == 0f)
            {
                // Increment rotation.
                // If you want a faster or slower rotation, you'll want to change the 'rotationSpeed' value at the top of this class.
                npc.rotation += (npc.direction * npc.directionY) * rotationSpeed;

                if (npc.collideY)
                {
                    npc.ai[0] = 2f;
                }
                else if (npc.ai[0] == 2f)
                {
                    npc.direction *= -1;

                    npc.ai[0] = npc.ai[1] = 1f;
                }

                if (npc.collideX)
                {
                    npc.directionY *= -1;

                    npc.ai[1] = 1f;
                }
            }
            else
            {
                // Same here are before, except it's rotating the other way.
                npc.rotation -= (npc.direction * npc.directionY) * rotationSpeed;

                if (npc.collideX)
                {
                    npc.ai[0] = 2f;
                }
                else if (npc.ai[0] == 2f)
                {
                    npc.directionY *= -1;

                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                }

                if (npc.collideY)
                {
                    npc.direction *= -1;

                    npc.ai[1] = 0f;
                }
            }

            // After all collision and direction checking, set the velocity of this NPC based on those checks using 'npc.direction' and 'npc.directionY'.
            npc.velocity = new Vector2(npc.direction, npc.directionY) * moveSpeed;

            return (false);
        }

        public override void PostAI()
        {
            SlopeCollision();
            OffsetCollision();
        }

        public override void FindFrame(int frameHeight)
        {
            if (++npc.frameCounter >= 10)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[npc.type]);
            }
        }

        // Overriding PreDraw to draw around NPC origin, instead of some arbitrary value.
        // Important to make sure the NPC is being drawn correctly around its own center, so it looks like it's half behind tiles.
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Vector2 origin = npc.frame.Size() / 2;

            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, origin, npc.scale, SpriteEffects.None, 0f);

            return (false);
        }

        private void SlopeCollision()
        {
            Vector4 vector = Collision.WalkDownSlope(npc.position, npc.velocity, npc.width, npc.height, 0.3f);
            npc.position = new Vector2(vector.X, vector.Y);
            npc.velocity = new Vector2(vector.Z, vector.W);
        }

        private void OffsetCollision()
        {
            npc.oldVelocity = npc.velocity;
            npc.collideX = npc.collideY = false;

            int widthOffset = 12;
            int heightOffset = 12;
            Vector2 pos = npc.Center - new Vector2(widthOffset / 2, heightOffset / 2);
            npc.velocity = Collision.noSlopeCollision(pos, npc.velocity, widthOffset, heightOffset, true, true);

            if (Collision.up)
            {
                npc.velocity.Y = 0.01f;
            }

            if (npc.oldVelocity.X != npc.velocity.X)
            {
                npc.collideX = true;
            }
            if (npc.oldVelocity.Y != npc.velocity.Y)
            {
                npc.collideY = true;
            }

            npc.oldPosition = npc.position;
            npc.oldDirection = npc.direction;
        }
    }
}
