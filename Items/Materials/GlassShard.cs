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
			Item.width = 10;
			Item.height = 10;

			Item.rare = ItemRarityID.Green;
			Item.maxStack = 9999;
		}

		public override void AddRecipes()
		{
			CreateRecipe(6)
				.AddIngredient(ItemID.Glass)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(6)
				.AddIngredient(ItemID.GlassWall,4)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(6)
				.AddIngredient(ItemID.GlassPlatform, 2)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(100)
				.AddIngredient(ItemID.GlassBathtub)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(25)
				.AddIngredient(ItemID.GlassBed)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(30)
				.AddIngredient(ItemID.GlassBookcase)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(6)
				.AddIngredient(ItemID.GlassBowl)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(10)
				.AddIngredient(ItemID.GlassCandelabra)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(6)
				.AddIngredient(ItemID.GlassCandle)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(15)
				.AddIngredient(ItemID.GlassChair)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(25)
				.AddIngredient(ItemID.GlassChandelier)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(30)
				.AddIngredient(ItemID.GlassChest)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(10)
				.AddIngredient(ItemID.GlassClock)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(30)
				.AddIngredient(ItemID.GlassDoor)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(40)
				.AddIngredient(ItemID.GlassDresser)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(20)
				.AddIngredient(ItemID.GlassKiln)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(10)
				.AddIngredient(ItemID.GlassLamp)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(6)
				.AddIngredient(ItemID.GlassLantern)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(50)
				.AddIngredient(ItemID.GlassPiano)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(20)
				.AddIngredient(ItemID.GlassSink)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(50)
				.AddIngredient(ItemID.GlassSofa)
				.AddTile(TileID.WorkBenches)
				.Register();

			CreateRecipe(36)
				.AddIngredient(ItemID.GlassTable)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}