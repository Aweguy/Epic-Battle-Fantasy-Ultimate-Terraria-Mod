using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.SignatureItems
{
	public class Doodad : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doodad of the Squire");
			Tooltip.SetDefault("A bottomless bag filled with everything from honeydew to thrown daggers. Perfect for a bunch of mooks.\n[c/FF0000:By Squire Doodad].");
		}
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 22;
			Item.damage = 45;
			Item.useTime = 12;
			Item.useAnimation = 13;
			Item.DamageType = DamageClass.Ranged;
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 17f;
			Item.rare = -12;
			Item.value = Item.sellPrice(platinum: 1);
			Item.UseSound = SoundID.Item1;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			type = Main.rand.Next(EpicBattleFantasyUltimate.thrownProjectiles.ToArray());
			return true;
		}
	}
}