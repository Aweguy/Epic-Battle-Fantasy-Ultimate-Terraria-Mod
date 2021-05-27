using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Dusts
{
    public class LightBubble : ModDust
    {
        // This is the max distance the projectiles will go in the wave.
        public int distance = 100;

        // The Counter will act like "Degrees" and will increase to create the wave
        public int counter = 0;

        // Write a bit of code that sets the Start X position at the start of the AI.
        public int startX = 0;

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "EpicBattleFantasyUltimate/Dusts/LightBubble";
            return mod.Properties.Autoload;
        }

        public override void SetDefaults()
        {
            updateType = 226;
        }

        public override void OnSpawn(Dust dust)
        {
            startX = 10;

            dust.frame = new Rectangle(0, 0, 24, 24);
            dust.alpha = 0;
        }

        public override bool Update(Dust dust)
        {
            float xOffset = (float)(Math.Cos(MathHelper.ToRadians(counter)) * distance);
            // Get the X Position for the projectile
            int x = (int)(startX + xOffset);
            // Write some code to set the projectile position, making sure X is set to this int x

            // Increment the counter at the end.
            counter++;

            return true;
        }
    }
}