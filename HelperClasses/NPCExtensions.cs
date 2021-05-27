using Microsoft.Xna.Framework;
using System;
using Terraria;

namespace EpicBattleFantasyUltimate.HelperClasses
{
    public static class NPCExtensions
    {
        public static void DoNPC_OrbitPosition(this NPC npc, Vector2 position, float distance, float rotationPerSecond = MathHelper.PiOver2)
        {
            npc.ai[1] += rotationPerSecond / 60;

            npc.position.X = position.X - (int)(Math.Cos(npc.ai[1]) * distance) - npc.width / 2;
            npc.position.Y = position.Y - (int)(Math.Sin(npc.ai[1]) * distance) - npc.height / 2;
        }
    }
}