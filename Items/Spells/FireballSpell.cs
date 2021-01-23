﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.Projectiles.SpellProjectiles;


namespace EpicBattleFantasyUltimate.Items.Spells
{
    public class FireballSpell : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball Spell");
            Tooltip.SetDefault("Shoots a fireball that explodes into a bigger one when it dies.\nHas limited range.\nHas a chance of burning enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 60;
            item.width = 28;
            item.height = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 100;
            item.useAnimation = 100;
            item.mana = 30;
            item.rare = ItemRarityID.LightPurple;
            item.useTurn = true;
            item.shoot = ModContent.ProjectileType<FireballBig>();
            item.shootSpeed = 4f;
			item.noMelee = true;
            item.magic = true;
            item.scale = 1.2f;
            item.value = Item.sellPrice(silver: 10);
            
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;

            return true;
        }













        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LivingFireBlock, 100);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
