using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace EpicBattleFantasyUltimate.ClassTypes
{
    public abstract class EpicBow : ModItem
    {

        public int BowProj;

        #region SafeMethods
        public override bool CloneNewInstances => true;
        public virtual void SetSafeDefaults()
        {
        }
        public virtual void SetSafeStaticDefaults()
        {
        }
        #endregion
        public override void SetStaticDefaults()
        {
            SetSafeStaticDefaults();
        }

        public override void SetDefaults()
        {
            SetSafeDefaults();

            item.noMelee = true;
            item.noUseGraphic = true;
            item.ranged = true;
            item.channel = true;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAmmo = AmmoID.Arrow;
            item.shoot = BowProj;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = BowProj;
            position = player.Center;
            for (int l = 0; l < Main.projectile.Length; l++)
            {                                                    //this make so you can only spawn one of this projectile at the time,
                Projectile proj = Main.projectile[l];
                if (proj.active && proj.type == item.shoot && proj.owner == player.whoAmI)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
