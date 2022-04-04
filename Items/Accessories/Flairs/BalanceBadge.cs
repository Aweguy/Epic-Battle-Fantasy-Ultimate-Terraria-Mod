using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
	public class BalanceBadge : Flair
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Balance Badge");
			Tooltip.SetDefault("Represents pure equilibrium and bestows a wealth of boosts.\n5 defense\n5% increase to all damage types\nIncreases maximum health and mana by 10\nIncreases movement and attack speed by 5%\nIncreases max minion slots by 1\nIncreases critical rates by 5%");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 19;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightPurple;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 5;
			player.GetDamage(DamageClass.Generic) += 0.05f;
			player.moveSpeed += 0.05f;
			player.statLifeMax2 += 10;
			player.statManaMax2 += 10;
			player.meleeSpeed += 0.05f;
			player.GetCritChance(DamageClass.Generic) += 5;
			player.maxMinions += 1;
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
			return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LightShard, 5)
				.AddIngredient(ItemID.DarkShard,5)
				.AddIngredient(ItemID.SoulofLight, 4)
				.AddIngredient(ItemID.SoulofNight,4)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}