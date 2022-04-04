using EpicBattleFantasyUltimate.Items.Materials.Gems;
using EpicBattleFantasyUltimate.Projectiles.SwordProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Swords
{
	public class LightningShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lightning Shard");
			Tooltip.SetDefault("You feel shock and awe when you hold this.\nRight-click to launch a spinning sword boomerang.\nThe Item cannot be used while the boomerang is out.");
		}

		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Melee;;
			Item.width = 104;
			Item.height = 116;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.knockBack = 5f;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Red;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.useTurn = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.damage = 50;
				Item.DamageType = DamageClass.Melee;;
				Item.width = 104;
				Item.height = 116;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.useTime = 10;
				Item.useAnimation = 10;
				Item.knockBack = 5f;
				Item.value = Item.sellPrice(gold: 10);
				Item.rare = ItemRarityID.Red;
				Item.shoot = ModContent.ProjectileType<LightningShardCyclone>();
				Item.shootSpeed = 20f;
				Item.autoReuse = false;
				Item.useTurn = false;
				Item.noMelee = true;
				Item.UseSound = SoundID.Item1;
				Item.noUseGraphic = true;
				return player.ownedProjectileCounts[Item.shoot] < 1;
			}
			else
			{
				Item.damage = 50;
				Item.DamageType = DamageClass.Melee;;
				Item.width = 104;
				Item.height = 116;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.useTime = 10;
				Item.useAnimation = 10;
				Item.knockBack = 5f;
				Item.value = Item.sellPrice(gold: 10);
				Item.rare = ItemRarityID.Red;
				Item.UseSound = SoundID.Item1;
				Item.shoot = ProjectileID.None;
				Item.noMelee = false;
				Item.autoReuse = true;
				Item.useTurn = true;
				Item.noUseGraphic = false;
			}
			return player.ownedProjectileCounts[ModContent.ProjectileType<LightningShardCyclone>()] < 1;
		}

	}
}