using EpicBattleFantasyUltimate.Items.Launchers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
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
		public Projectile shot;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 trueSpeed = new Vector2(velocity.X, velocity.Y);
			shot = Main.projectile[Projectile.NewProjectile(source,position.X, position.Y, trueSpeed.X, trueSpeed.Y, type, damage, knockback, player.whoAmI)];
			shot.GetGlobalProjectile<LauncherProjectile>().PoisonedRounds = true;

			return false;
		}
	}
}