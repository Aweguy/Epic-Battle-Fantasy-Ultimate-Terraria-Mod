﻿using EpicBattleFantasyUltimate.Buffs.Minions;
using EpicBattleFantasyUltimate.Projectiles.Minions.OreMinions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.knockBack = 4f;
            Item.mana = 30;

            Item.width = 32;
            Item.height = 32;

            Item.useTime = 36;
            Item.useAnimation = 36;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(gold: 50);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item44;

            // These below are needed for a minion weapon
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<LittleOreBuff>();
            // No buffTime because otherwise the Item tooltip would say something like "1 minute duration"
            Item.shoot = ModContent.ProjectileType<LittleAmethystOre>();
            Item.scale = 1.15f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(Item.buffType, 2);

            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
            position = Main.MouseWorld;
            return true;
        }
    }
}