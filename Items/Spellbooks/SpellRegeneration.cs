using EpicBattleFantasyUltimate.Buffs.Buffs;
using EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Spellbooks
{
	public class SpellRegeneration : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Regeneration Spell");
			Tooltip.SetDefault("This spell vastly increases your regeneration.\nCosts a lot of mana and has a big cooldown.");
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 100;
			Item.useAnimation = 10;
			Item.mana = 25;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(silver: 50);
			Item.useTurn = true;
			Item.value = Item.sellPrice(gold: 10);
		}

		public override bool CanUseItem(Player player)
		{
			int buff = ModContent.BuffType<RegenerationSated>();
			return !player.HasBuff(buff);
		}

		public override bool? UseItem(Player player)
		{
			player.AddBuff(ModContent.BuffType<Regeneration>(), 60 * 10);
			player.AddBuff(ModContent.BuffType<RegenerationSated>(), 60 * 30);

			return base.UseItem(player);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.RegenerationPotion, 250)
				.AddIngredient(ItemID.Book)
				.AddTile(TileID.Bookcases)
				.Register();
		}
	}
}