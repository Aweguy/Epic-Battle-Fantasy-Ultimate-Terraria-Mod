using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using EpicBattleFantasyUltimate.Projectiles.SwordProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Swords
{
	public class HeavensGate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heaven's Gate"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A legendary sword belonging to a line of famed corsairs.");
		}

		public override void SetDefaults()
		{
			Item.width = 64;
			Item.height = 64;

			Item.damage = 50;
			Item.knockBack = 8f;
			Item.DamageType = DamageClass.Melee;;

			Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.autoReuse = true;
			Item.channel = true;

			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;

			Item.shootSpeed = 12f;
			Item.shoot = ModContent.ProjectileType<LightBlade>();
		}
		
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 VelocityManual = new Vector2(velocity.X, velocity.Y);

			Projectile.NewProjectile(source, Main.MouseWorld - (Vector2.Normalize(VelocityManual) * 80f), Vector2.Zero, type, damage, knockback, player.whoAmI, velocity.X, velocity.Y);

			return false;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.AncientLight);
			}
		}

		public override bool OnlyShootOnSwing => true;

	}
}