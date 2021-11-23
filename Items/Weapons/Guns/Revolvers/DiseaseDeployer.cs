using EpicBattleFantasyUltimate.Items.Weapons.Launchers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
	public class DiseaseDeployer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Disease Deployer");
			Tooltip.SetDefault("Don’t worry, the lead tubing is only on the inside of the gun.\n Poisons all projectiles.");
		}
		public override void SetDefaults()
		{
			item.width = 50;
			item.height = 30;

			item.damage = 14;
			item.useTime = 15;
			item.useAnimation = 15;
			item.reuseDelay = 2;

			item.crit = 1;
			item.knockBack = 3f;
			item.ranged = true;
			item.noMelee = true;

			item.value = Item.sellPrice(gold: 3);
			item.rare = ItemRarityID.LightPurple;

			item.useAmmo = AmmoID.Bullet;
			item.UseSound = SoundID.Item41;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 12f;
			item.useStyle = ItemUseStyleID.HoldingOut;
		}
		public Projectile shot;

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 trueSpeed = new Vector2(speedX, speedY);
			shot = Main.projectile[Projectile.NewProjectile(position.X, position.Y, trueSpeed.X, trueSpeed.Y, type, damage, knockBack, player.whoAmI)];
			shot.GetGlobalProjectile<LauncherProjectile>().PoisonedRounds = true;

			return false;
		}
	}
}