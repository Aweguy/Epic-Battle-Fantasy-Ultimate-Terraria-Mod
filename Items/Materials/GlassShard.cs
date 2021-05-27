using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
    public class GlassShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glass Shard");
            Tooltip.SetDefault("Crafted by artisan window smashers.");
        }

        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 10;

            item.rare = ItemRarityID.Green;
            item.maxStack = 9999;
        }

        #region AddRecipes

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Glass);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 6);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassWall, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassPlatform, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassBathtub);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassBed);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 25);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassBookcase);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 25);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassBowl);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassCandelabra);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassCandle);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassChair);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 15);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassChandelier);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 25);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassChest);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 30);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassClock);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassDoor);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 30);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassDresser);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 40);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassKiln);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 20);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassLamp);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 10);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassLantern);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 5);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassPiano);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassSink);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 20);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassSofa);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 100);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlassTable);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this, 35);
            recipe.AddRecipe();
        }

        #endregion AddRecipes
    }
}