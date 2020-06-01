using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Gores
{
    public class JudgementFeather1 : ModGore
    {

        Vector2 goreVel = Vector2.Zero;


        public override bool Update(Gore gore)
        {
            Vector2 goreVel = new Vector2(1, 0).RotatedBy(Main.rand.NextFloat(1.57f, 1.57f) + (Main.rand.Next(2) == 0 ? -1.0f : 1.0f) * 1.57f);

            gore.alpha += 10;
            gore.velocity *= 0.95f;

            if(gore.alpha >= 255)
            {
                gore.active = false;
            }


            return false;
        }



        public override void OnSpawn(Gore gore)
        {
            
            gore.velocity = goreVel;
        }


        








    }
}
