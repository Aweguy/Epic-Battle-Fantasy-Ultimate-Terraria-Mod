using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Dusts
{
    public class LightFeather : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            UpdateType = 226;
            dust.frame = new Rectangle(0, 0, 32, 32);
            dust.alpha = 0;
        }

        public override bool Update(Dust dust)
        {
            dust.position += dust.velocity;

            //float num112 = dust.scale;
            //if (num112 > 1f)
            //{
            //num112 = 1f;
            //}
            /*if (!dust.noLight)
			{
				Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), num112 * 0.2f, num112 * 0.7f, num112 * 1f);
			}*/
            if (dust.noGravity)
            {
                Dust dust107 = dust;
                dust107.velocity *= 0.93f;
                if (dust.fadeIn == 0f)
                {
                    dust.scale += 0.0025f;
                }
            }
            //Dust dust108 = dust;
            //dust108.velocity *= new Vector2(0.97f, 0.99f);
            dust.scale -= 0.01f;
            dust.alpha += 1;

            return true;
        }
    }
}