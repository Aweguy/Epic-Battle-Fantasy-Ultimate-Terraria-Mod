using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Fire;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Ammo.Shots
{
	public class Shot : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame Shot");
			Tooltip.SetDefault("Reusable bullets that explode on impact! Don't ask how we get them back.");
		}

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = 1;
			Item.value = 100;
			Item.consumable = true;
			Item.maxStack = 9999;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = ModContent.ProjectileType<FlameShot>();
			Item.shootSpeed = 8f;
			Item.ammo = Item.type;
		}

	}
}