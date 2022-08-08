using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class Visecracker : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Visecracker");
			Tooltip.SetDefault("Puts the bullet's weight into every shot\nDeals melee damage.");
		}
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 28;

			Item.damage = 25;
			Item.knockBack = 10f;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;

			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true;

			Item.rare = ItemRarityID.Pink;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 10f;
			Item.useAmmo = AmmoID.Bullet;
			Item.UseSound = SoundID.Item41;
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			player.velocity -= new Vector2(1f, 0f) * player.Directions;//Prototype, will fix later
        }
    }
}