using EpicBattleFantasyUltimate.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Swords
{
	public class BoneBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bone Blade");
			Tooltip.SetDefault("The Macabre Machete, the Rib Rapier, the Spine Slicer, the-\nWeakens targets on hit.");
		}

		public override void SetDefaults()
		{
			Item.damage = 45;
			Item.DamageType = DamageClass.Melee;;
			Item.width = 64;
			Item.height = 64;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 45;
			Item.useAnimation = 45;
			Item.knockBack = 2f;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(ModContent.BuffType<Weaken>(), 60 * 5);
		}

	}
}