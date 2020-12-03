using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Buffs.Debuffs;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
    public class CursedSpike : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Spike");
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.timeLeft = 120;
            //drawOffsetX = -2;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.alpha = 100;

        }


        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Cursed>(), 10 * 60);
        }




        public override void AI()
        {


            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;

        }












    }
}
