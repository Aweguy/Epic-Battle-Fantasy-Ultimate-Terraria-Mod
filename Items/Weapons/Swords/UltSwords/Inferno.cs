using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
	public class Inferno : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Inferno"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Wreathed in scorching flames.\nBurns foes.");
		}

		public override void SetDefaults()
		{
			item.damage = 77;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = 1;
			item.knockBack = 6f;
			item.value = Item.sellPrice(gold: 1);
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(3))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
			}
		}



		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 240);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("VolcanicRuby"), 13);
			recipe.AddIngredient(ItemID.LivingFireBlock, 10);
			recipe.AddIngredient(ItemID.FireFeather);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}