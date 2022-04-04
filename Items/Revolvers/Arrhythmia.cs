using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class Arrhythmia : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arrhythmia");
			Tooltip.SetDefault("Echoes with an irregular beat when fired.\nHas a slightly randomized rate of fire");
		}
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 30;

			Item.damage = 14;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.reuseDelay = 2;

			Item.crit = 1;
			Item.knockBack = 3f;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 3);
			Item.rare = ItemRarityID.LightPurple;

			Item.useAmmo = AmmoID.Bullet;
			Item.UseSound = SoundID.Item41;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 12f;
			Item.useStyle = ItemUseStyleID.Shoot;
		}
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			int Random = Main.rand.Next(5, 25);
			Item.useTime = Item.useAnimation = Random;

			return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ModContent.ItemType<AncientAmber>())
				.AddIngredient(ModContent.ItemType<VoltaicTopaz>())
				.AddIngredient(ItemID.DirtBlock,25)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}