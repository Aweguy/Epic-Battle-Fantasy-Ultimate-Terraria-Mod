using EpicBattleFantasyUltimate.HelperClasses;
using EpicBattleFantasyUltimate.Items.Swords;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
    public class EquilibriumSlash:ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 12;

            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.tileCollide = false;

            Projectile.localNPCHitCooldown = -1;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override bool PreAI()
        {
            float rotation = Projectile.ai[0];
            Projectile.rotation = rotation;

            #region Animation
            if (--Projectile.frameCounter <= 0)
            {
                Projectile.frameCounter = 2;
                if (++Projectile.frame >= 6)
                {
                    Projectile.Kill();
                }
            }
            #endregion

            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return this.DrawProjectileCentered(Main.spriteBatch, lightColor);//Centering the origin point of the projectile.
        }

    }
}
