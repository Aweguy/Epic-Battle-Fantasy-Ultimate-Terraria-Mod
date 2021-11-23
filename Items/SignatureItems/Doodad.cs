using Microsoft.Xna.Framework;
using Terraria;
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
			item.width = 28;
			item.height = 22;
			item.damage = 45;
			item.useTime = 12;
			item.useAnimation = 13;
			item.ranged = true;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.EatingUsing;
			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.shootSpeed = 17f;
			item.rare = -12;
			item.value = Item.sellPrice(platinum: 1);
			item.UseSound = SoundID.Item1;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = Main.rand.Next(EpicBattleFantasyUltimate.thrownProjectiles.ToArray());
			return true;
		}
	}
}