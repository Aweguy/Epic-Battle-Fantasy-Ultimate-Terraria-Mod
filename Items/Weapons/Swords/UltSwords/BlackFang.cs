﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class BlackFang : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Fang");
            Tooltip.SetDefault("'This weapon is totally historically accurate, I'm sure of it. I saw it in an anime once!' - Matt");
        }

        public override void SetDefaults()
        {
            item.damage = 66;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 30;
            item.useAnimation = 30;
            item.knockBack = 2f;
            item.value = Item.sellPrice(gold: 5);
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60 * 5);
            player.statLife += 6;
            player.HealEffect(6);
        }

    }
}