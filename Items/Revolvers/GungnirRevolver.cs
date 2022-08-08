using EpicBattleFantasyUltimate.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class GungnirRevolver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Regnir");
			Tooltip.SetDefault("You thought this gun would have a special power too.\nNo, its special power is being normal\nVery High Velocity shots.\nBoosts critical rate by 30% if Gungnir Rifle is in the inventory.");
		}
		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 30;

			Item.damage = 46;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.reuseDelay = 10;

			Item.knockBack = 3f;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 3);
			Item.rare = ItemRarityID.LightPurple;

			Item.useAmmo = AmmoID.Bullet;
			Item.UseSound = SoundID.Item41;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 30f;
			Item.useStyle = ItemUseStyleID.Shoot;
		}
        public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ModContent.ItemType<LeckoBrick>())
				.AddIngredient(ModContent.ItemType<P2Processor>(), 2)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}