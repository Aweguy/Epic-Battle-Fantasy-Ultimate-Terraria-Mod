using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Dusts
{
    public class NatureLeaves : ModDust
    {
        private int lifetime;
        public override void OnSpawn(Dust dust)
        {
            dust.frame = new Rectangle(0, 0, 24, 24);
            dust.alpha = 0;

            lifetime = Main.rand.Next(0, 150);
        }

        public override bool Update(Dust dust)
        {
            dust.scale = 0.5f;

            lifetime--;

            if (lifetime <= 0)
            {
                dust.alpha += (int)(255 / 150);
            }

            dust.velocity *= 0.99f;

            if (dust.alpha >= 255)
            {
                dust.active = false;
            }

            return base.Update(dust);
        }
    }
}