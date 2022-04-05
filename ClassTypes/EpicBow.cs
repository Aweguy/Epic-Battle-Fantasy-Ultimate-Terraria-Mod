using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace EpicBattleFantasyUltimate.ClassTypes
{
    public abstract class EpicBow : ModItem
    {

        public int BowProj;

        #region SafeMethods
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

            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Ranged;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = BowProj;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = BowProj;
            position = player.Center;
            for (int l = 0; l < Main.projectile.Length; l++)
            {                                                    //this make so you can only spawn one of this Projectile at the time,
                Projectile proj = Main.projectile[l];
                if (proj.active && proj.type == Item.shoot && proj.owner == player.whoAmI)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
