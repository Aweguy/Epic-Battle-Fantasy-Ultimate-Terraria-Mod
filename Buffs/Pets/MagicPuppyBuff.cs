/*using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.MagicPuppy;

namespace EpicBattleFantasyUltimate.Buffs.Pets
{
	public class MagicPuppyBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Magic Puppy");
			Description.SetDefault("A magic puppy is following you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<EpicPlayer>().MagicPuppyBuff = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<MagicPuppy>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<MagicPuppy>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}*/