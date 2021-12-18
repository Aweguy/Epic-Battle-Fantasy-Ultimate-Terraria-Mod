using EpicBattleFantasyUltimate.Buffs.Debuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
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
			item.damage = 45;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 45;
			item.useAnimation = 45;
			item.knockBack = 2f;
			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(ModContent.BuffType<Weaken>(), 60 * 5);
		}

	}
}