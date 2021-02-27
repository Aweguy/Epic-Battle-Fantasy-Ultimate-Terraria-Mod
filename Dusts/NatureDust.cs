using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Dusts
{
    public class NatureDust : ModDust
    {


        int lifetime = 60;

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "EpicBattleFantasyUltimate/Dusts/NatureDust";
            return mod.Properties.Autoload;
        }

        float DustScale;

        /*public override void SetDefaults()
        {
            updateType = 124;
        }*/





        public override void OnSpawn(Dust dust)
        {

            dust.frame = new Rectangle(0, 0, 15, 15);
            dust.alpha = 0;



        }

        public override bool Update(Dust dust)
        {
            dust.scale = 0.5f;

            lifetime--;

            if(lifetime <= 0)
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
