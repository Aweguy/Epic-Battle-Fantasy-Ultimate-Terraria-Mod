using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Items.Ammo.Shots;


namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
    public class SpineSnapper : ModItem
    {

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spine Snapper");
			Tooltip.SetDefault("Said to have been forged by a cabal of Cat Ninjas. Brings death and woe to those in its sights.");
		}

		public override void SetDefaults()
		{
			item.width = 112;
			item.height = 56;

			item.useTime = 40;
			item.useAnimation = 40;


			item.damage = 50;
			item.crit = 3;
			item.melee = true;
			item.noMelee = true;

			item.value = Item.sellPrice(gold: 2);
			item.rare = ItemRarityID.Purple;

			item.shoot = ProjectileID.PurificationPowder;
			item.useAmmo = ModContent.ItemType<Shot>();
			item.UseSound = SoundID.Item38;
			item.shootSpeed = 12f;
			item.useStyle = ItemUseStyleID.HoldingOut;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 34f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
			muzzleOffset += new Vector2(0, -10.5f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-25, -10);
		}

		public override bool CanUseItem(Player player)
		{
			int buff = mod.BuffType("Overheat");
			return !player.HasBuff(buff);
		}
	}
}
