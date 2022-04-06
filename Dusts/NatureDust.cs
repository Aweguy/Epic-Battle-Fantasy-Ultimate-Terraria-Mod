using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Dusts
{
    public class NatureDust : ModDust
    {
        private int lifetime = 60;

        public override void OnSpawn(Dust dust)
        {
            dust.frame = new Rectangle(0, 0, 15, 15);
            dust.alpha = 0;
        }

        public override bool Update(Dust dust)
        {
            dust.scale = 0.5f;

            lifetime--;

            if (lifetime <= 0)
            {
                dust.alpha += (int)(255 / 60);
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