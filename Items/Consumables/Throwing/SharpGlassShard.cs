using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace EpicBattleFantasyUltimate.Items.Consumables.Throwing
{
    public class SharpGlassShard : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharp Glass Shard");
            Tooltip.SetDefault("Remember, you can use it to hurt people >:D !");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.ranged = true;

            item.width = 10;
            item.height = 10;

            item.rare = -12;
            item.maxStack = 9999;

            item.shoot = mod.ProjectileType("GlassShardProjectile");
            item.shootSpeed = 20f;
            item.autoReuse = true;
            item.useTurn = true;
            item.UseSound = SoundID.Item1;
        }


        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(mod.BuffType("RampantBleed"), 60 * 10);
        }











        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("GlassShard"), 100);
            recipe.AddIngredient(ItemID.SoulofMight, 2);
            recipe.AddTile(TileID.AlchemyTable);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();

        }
    }
}
