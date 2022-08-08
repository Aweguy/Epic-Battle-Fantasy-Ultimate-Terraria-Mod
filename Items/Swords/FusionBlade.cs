using EpicBattleFantasyUltimate.Projectiles.SwordProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Swords
{
	public class FusionBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fusion Blade"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Modeled after the weapons used by the MILITIA branch.\nShoots a big bullet.");
		}

		public override void SetDefaults()
		{
			Item.damage = 59;
			Item.DamageType = DamageClass.Melee;;
			Item.width = 54;
			Item.height = 60;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8f;
			Item.value = Item.sellPrice(gold: 4);
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shootSpeed = 5f;
			Item.shoot = ModContent.ProjectileType<BulletBob>();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Firefly);
		}

	}
}