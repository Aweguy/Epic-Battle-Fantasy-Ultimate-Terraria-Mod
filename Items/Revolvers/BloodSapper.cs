using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class BloodSapper : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood Sapper");
			Tooltip.SetDefault("A sidearm so cruel, it seems to aim itself at the bloodiest of organs.");
		}
		public override void SetDefaults()
		{
			Item.width = 56;
			Item.height = 34;

			Item.damage = 13;
			Item.useTime = 10;
			Item.useAnimation = 10;

			Item.crit = 1;
			Item.knockBack = 3f;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.autoReuse = true;

			Item.value = Item.sellPrice(gold: 3);
			Item.rare = ItemRarityID.LightPurple;

			Item.useAmmo = AmmoID.Bullet;
			Item.UseSound = SoundID.Item41;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 12f;
			Item.useStyle = ItemUseStyleID.Shoot;
		}
		
	}
}