using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
	public class Threeforce : Flair
	{
		private int regentimer = 60 * 5;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Threeforce");
			Tooltip.SetDefault("This amulet is fueled by...uh...'Strength', 'Fearlessness' and...uhhhh 'smartness'?\n Increases limit break generation\nApplies passive LB regeneration.");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Lime;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			var epicPlayer = EpicPlayer.ModPlayer(player);

			regentimer--;

			epicPlayer.Tryforce = true;

			if (regentimer <= 0 && epicPlayer.LimitCurrent < 100)
			{
				epicPlayer.LimitCurrent++;

				regentimer = 60 * 5;
			}
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
			return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 10)
				.AddIngredient(ItemID.PixieDust, 100)
				.AddRecipeGroup("TitaniumBar", 10)
				.AddRecipeGroup("GoldBar", 20)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}