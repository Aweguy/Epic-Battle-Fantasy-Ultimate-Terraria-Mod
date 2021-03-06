﻿/*using EpicBattleFantasyUltimate.Buffs.Minions;
using EpicBattleFantasyUltimate.Projectiles.Minions.OreMinions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Summoning
{
    public class OreRod : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ore Rod");
            Tooltip.SetDefault("A strange staff like weapon that summons rather friendly ore creatures to aid youaw.");
            ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
            ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 60;
            item.knockBack = 4f;
            item.mana = 30;

            item.width = 32;
            item.height = 32;

            item.useTime = 36;
            item.useAnimation = 36;

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.sellPrice(gold: 50);
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item44;

            // These below are needed for a minion weapon
            item.noMelee = true;
            item.summon = true;
            item.buffType = ModContent.BuffType<LittleOreBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            item.shoot = ModContent.ProjectileType<LittleAmethystOre>();
            item.scale = 1.15f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(item.buffType, 2);

            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
            position = Main.MouseWorld;
            return true;
        }
    }
}*/