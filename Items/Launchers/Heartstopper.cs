﻿using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class Heartstopper : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heartstopper");
			Tooltip.SetDefault("They say your heartbeat is one of the last things you hear before you die.\nThis can fix that.");
		}

		public override void SetSafeDefaults()
		{
			Item.width = 98;
			Item.height = 60;

			Item.useTime = 60;
			Item.useAnimation = 60;

			Item.damage = 60;
			Item.crit = 2;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item38;
			Item.shootSpeed = 14f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 34f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
			muzzleOffset += new Vector2(0, -9f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-32, -9);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts, 3)
				.AddIngredient(ModContent.ItemType<AncientAmber>(), 4)
				.AddIngredient(ModContent.ItemType<VoltaicTopaz>(), 4)
				.AddIngredient(ItemID.DirtBlock)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}