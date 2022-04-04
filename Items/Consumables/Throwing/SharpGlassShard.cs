using EpicBattleFantasyUltimate.Buffs.Debuffs;
using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.Thrown;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Consumables.Throwing
{
	public class SharpGlassShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sharp Glass Shard");
			Tooltip.SetDefault("Remember, you can use it to hurt people >:D !");
		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.DamageType = DamageClass.Ranged;

			Item.width = 10;
			Item.height = 10;

			Item.rare = -12;
			Item.maxStack = 9999;

			Item.shoot = ModContent.ProjectileType<GlassShardProjectile>();
			Item.shootSpeed = 20f;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item1;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(ModContent.BuffType<RampantBleed>(), 60 * 10);
		}
		public override void AddRecipes()
		{
			CreateRecipe(100)
				.AddIngredient(ItemID.IllegalGunParts, 3)
				.AddIngredient(ModContent.ItemType<GlassShard>(), 100)
				.AddIngredient(ItemID.SoulofMight, 2)
				.AddTile(TileID.AlchemyTable)
				.Register();
		}
	}
}