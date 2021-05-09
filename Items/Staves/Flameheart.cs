using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.Projectiles.SpellProjectiles.FirestormSpell;


namespace EpicBattleFantasyUltimate.Items.Staves
{
	public class Flameheart : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flameheart");
			Tooltip.SetDefault("Shoots a fireball that explodes into a bigger one when it dies.\nHas limited range.\nHas a chance of burning enemies");
		}

		public override void SetDefaults()
		{
			item.damage = 60;
			item.width = 40;
			item.height = 40;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 30;
			item.useAnimation = 30;
			item.mana = 30;
			item.rare = ItemRarityID.LightPurple;
			item.useTurn = true;
			item.shoot = ModContent.ProjectileType<Fireball>();
			item.shootSpeed = 0f;
			item.noMelee = true;
			item.magic = true;
			item.value = Item.sellPrice(silver: 10);        
		}


		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;

			return true;
		}


		public override bool AltFunctionUse(Player player)
		{

			return true;
		}


		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{


				item.useTime = 100;
				item.useAnimation = 100;
				item.mana = 60;
				item.shoot = ModContent.ProjectileType<Firestorm>();
				item.shootSpeed = 0f;

			}
			else
			{
				item.useTime = 30;
				item.useAnimation = 30;
				item.mana = 10;
				item.shoot = ModContent.ProjectileType<Fireball>();
				item.shootSpeed = 0f;
			}


			return base.CanUseItem(player);
		}








		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LivingFireBlock, 100);
			recipe.AddIngredient(ItemID.Book);
			recipe.AddTile(TileID.Bookcases);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}


	}
}
