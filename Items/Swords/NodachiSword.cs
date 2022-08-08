using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using EpicBattleFantasyUltimate.Projectiles.SwordProjectiles;

namespace EpicBattleFantasyUltimate.Items.Swords
{
    public class NodachiSword: ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nodachi");
        }

        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Melee;

            Item.width = 64;
            Item.height = 64;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 40;
            Item.useAnimation = 40;

            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<NodachiSwordProj>();
            Item.autoReuse = false;
            Item.channel = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }
    }
}
