using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Buffs.Debuffs;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
    public class CursingRune : ModProjectile
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Curse");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;

            projectile.timeLeft = 1;
            projectile.alpha = 255;

            projectile.hostile = true;
            projectile.friendly = false;
        }


        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Cursed>(), 60 * 20);
        }







    }
}
