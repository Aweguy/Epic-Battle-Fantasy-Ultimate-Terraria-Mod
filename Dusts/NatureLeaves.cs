using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Dusts
{
    public class NatureLeaves : ModDust
    {


        int lifetime;

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "EpicBattleFantasyUltimate/Dusts/NatureLeaves";
            return mod.Properties.Autoload;
        }

        float DustScale;

        /*public override void SetDefaults()
        {
            updateType = 124;
        }*/





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
