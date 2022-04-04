using EpicBattleFantasyUltimate.Projectiles.SwordProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Swords
{
	public class GaiaAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gaia's Axe");
			Tooltip.SetDefault("'The woodsman slices the wood of trees by day, and the bones of fiends by night.'");
		}

		public override void SetDefaults()
		{
			Item.width = 72;
			Item.height = 72;

			Item.damage = 130;
			Item.knockBack = 5f;
			Item.DamageType = DamageClass.Melee;

			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.UseSound = SoundID.Item1;
			Item.useStyle = ItemUseStyleID.Swing;

			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Red;
			Item.autoReuse = true;
		}
	}
}