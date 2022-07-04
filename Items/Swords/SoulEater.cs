using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Swords
{
	public class SoulEater : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Eater");
			Tooltip.SetDefault("Honestly, it could have been worse. It could kill you instantly.\nWhen held, increases your damage by 80% but reduces your defenst by 50%.");
		}

		public override void SetDefaults()
		{
			Item.damage = 150;
			Item.DamageType = DamageClass.Melee;
			Item.width = 64;
			Item.height = 64;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.knockBack = 9f;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.scale = 1.15f;
		}

		public override void HoldItem(Player player)
		{
			player.GetDamage(DamageClass.Generic) += 0.8f;
			player.statDefense /= (int)2f;
		}
	}
}