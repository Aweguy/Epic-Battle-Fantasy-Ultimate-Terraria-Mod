using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using EpicBattleFantasyUltimate.Projectiles.SwordProjectiles;

namespace EpicBattleFantasyUltimate.Items.Swords
{
	public class Equilibrium :ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Equilibrium");
			Tooltip.SetDefault("Representing the two sides of the feline deity, don't drop it on your toes");
		}
		public override void SetDefaults()
		{
			Item.width = 72;
			Item.height= 72;

			Item.damage = 100;
			Item.knockBack = 1f;
			Item.DamageType = DamageClass.Melee;
			Item.useStyle = ItemUseStyleID.Shoot;

			Item.channel = true;
			Item.useAnimation = 1;
			Item.useTime = 1;
			Item.noUseGraphic = true;


			Item.shoot = ModContent.ProjectileType<EquilibriumProj>();
			Item.shootSpeed = 2f;
		}
	}
}
