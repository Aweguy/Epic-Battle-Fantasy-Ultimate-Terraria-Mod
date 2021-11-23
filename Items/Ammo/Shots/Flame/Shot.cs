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
			item.damage = 5;
			item.ranged = true;
			item.width = 12;
			item.height = 12;
			item.maxStack = 1;
			item.value = 1000;
			item.rare = ItemRarityID.LightRed;
			item.shoot = ModContent.ProjectileType<FlameShot>();
			item.shootSpeed = 8f;
			item.ammo = item.type;
		}

	}
}