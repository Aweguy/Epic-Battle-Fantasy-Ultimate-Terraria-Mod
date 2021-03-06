﻿using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class Inferno : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Wreathed in scorching flames.\nBurns foes.");
        }

        public override void SetDefaults()
        {
            item.damage = 67;
            item.melee = true;
            item.width = 48;
            item.height = 48;
            item.useTime = 22;
            item.useAnimation = 22;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6f;
            item.value = Item.sellPrice(gold: 1);
            item.rare = ItemRarityID.Lime;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 240);
        }

    }
}