using EpicBattleFantasyUltimate.Projectiles.SpearProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Spears.UltSpears
{
	public class GashClub : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gash Club");
			Tooltip.SetDefault("Made from the trunk of a rare adult Gash Root tree.");
		}
		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.shootSpeed = 24f;
			Item.knockBack = 7f;
			Item.width = 16;
			Item.height = 16;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.autoReuse = true;
			Item.DamageType = DamageClass.Melee;;
			Item.damage = 40;

			Item.UseSound = SoundID.DD2_MonkStaffSwing;
			Item.shoot = ModContent.ProjectileType<GashClubProj>();
		}
		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use Item when using autoReuse
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
	}
}