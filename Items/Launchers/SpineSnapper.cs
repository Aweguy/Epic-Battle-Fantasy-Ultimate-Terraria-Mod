using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class SpineSnapper : EpicLauncher
	{ 
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spine Snapper");
			Tooltip.SetDefault("Said to have been forged by a cabal of Cat Ninjas. Brings death and woe to those in its sights.");
		}
		public override void SetSafeDefaults()
		{
			Item.width = 112;
			Item.height = 56;

			Item.useTime = 40;
			Item.useAnimation = 40;

			Item.damage = 50;
			Item.crit = 3;
			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 2);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item38;
			Item.shootSpeed = 12f;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 34f;
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
	}
}