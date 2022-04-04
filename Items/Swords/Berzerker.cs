using EpicBattleFantasyUltimate.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Swords
{
	public class Berzerker : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Berzerker");
			Tooltip.SetDefault("A heavy weapon with a stone edge and unyielding rage.");
		}

		public override void SetDefaults()
		{
			Item.width = 64;
			Item.height = 64;

			Item.damage = 150;
			Item.useAnimation = 40;
			Item.useTime = 40;
			Item.knockBack = 16f;

			Item.useStyle = ItemUseStyleID.Swing;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.DamageType = DamageClass.Melee;
		}

		public override bool? UseItem(Player Player)
		{
			if (Main.rand.NextFloat(1f) > 0.4f)
			{
				Player.AddBuff(ModContent.BuffType<Tired>(), 60 * 5);
			}
			return true;
		}

		public override void HoldItem(Player Player)
		{
			BerzerkerDash dashPlayer = Player.GetModPlayer<BerzerkerDash>();
			//If the dash is not active, immediately return so we don't do any of the logic for it
			if (!dashPlayer.DashActive)
				return;

			if (dashPlayer.DashTimer == BerzerkerDash.MAX_DASH_TIMER)
			{
				Vector2 newVelocity = Player.velocity;

				//Only apply the dash velocity if our current speed in the wanted direction is less than DashVelocity
				if ((dashPlayer.DashDir == BerzerkerDash.DashUp && Player.velocity.Y > -dashPlayer.DashVelocity) || (dashPlayer.DashDir == BerzerkerDash.DashDown && Player.velocity.Y < dashPlayer.DashVelocity))
				{
					//Y-velocity is set here
					//If the direction requested was DashUp, then we adjust the velocity to make the dash appear "faster" due to gravity being immediately in effect
					//This adjustment is roughly 1.3x the intended dash velocity
					float dashDirection = dashPlayer.DashDir == BerzerkerDash.DashDown ? 1 : -1.3f;
					newVelocity.Y = dashDirection * dashPlayer.DashVelocity;
				}
				else if ((dashPlayer.DashDir == BerzerkerDash.DashLeft && Player.velocity.X > -dashPlayer.DashVelocity) || (dashPlayer.DashDir == BerzerkerDash.DashRight && Player.velocity.X < dashPlayer.DashVelocity))
				{
					//X-velocity is set here
					int dashDirection = dashPlayer.DashDir == BerzerkerDash.DashRight ? 1 : -1;
					newVelocity.X = dashDirection * dashPlayer.DashVelocity;
				}

				Player.AddBuff(ModContent.BuffType<Tired>(), 60 * 5);//Applying the Tired debuff once.

				float TiredDebuff = 1f - Player.GetModPlayer<EpicPlayer>().TiredStacks * 0.2f;

				if (Player.HasBuff(ModContent.BuffType<Tired>()))
				{
					if (Player.GetModPlayer<EpicPlayer>().TiredStacks < 5)//Each stack of tired waekens the dash by 20%
					{
						Player.velocity = newVelocity * TiredDebuff;
					}
					else//Essentially manually clamping
					{
						Player.velocity = newVelocity * 0.33f;//Dash cap
					}
				}
				else
				{
					Player.velocity = newVelocity;
				}
			}

			//Decrement the timers
			dashPlayer.DashTimer--;
			dashPlayer.DashDelay--;

			if (dashPlayer.DashDelay == 0)
			{
				//The dash has ended.  Reset the fields
				dashPlayer.DashDelay = BerzerkerDash.MAX_DASH_DELAY;
				dashPlayer.DashTimer = BerzerkerDash.MAX_DASH_TIMER;
				dashPlayer.DashActive = false;
			}
		}

	}

	public class BerzerkerDash : ModPlayer
	{
		//These indicate what direction is what in the timer arrays used
		public static readonly int DashDown = 0;

		public static readonly int DashUp = 1;
		public static readonly int DashRight = 2;
		public static readonly int DashLeft = 3;

		//The direction the Player is currently dashing towards.  Defaults to -1 if no dash is ocurring.
		public float DashDir = -1;

		//The fields related to the dash accessory
		public bool DashActive = false;

		public int DashDelay = MAX_DASH_DELAY;
		public int DashTimer = MAX_DASH_TIMER;

		//The initial velocity.  10 velocity is about 37.5 tiles/second or 50 dashPlayerh
		public readonly float DashVelocity = 10f;

		//These two fields are the max values for the delay between dashes and the length of the dash in that order
		//The time is measured in frames
		public static readonly int MAX_DASH_DELAY = 50;

		public static readonly int MAX_DASH_TIMER = 35;

		public override void ResetEffects()
		{
			//if Player is on mount or dash is used, don't dash.
			if (Player.mount.Active || DashActive)
				return;

			if (Player.HeldItem.type == ModContent.ItemType<Berzerker>())
			{
				if (Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[DashDown] < 15)
					DashDir = DashDown;
				else if (Player.controlUp && Player.releaseUp && Player.doubleTapCardinalTimer[DashUp] < 15)
					DashDir = DashUp;
				else if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15)
					DashDir = DashRight;
				else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15)
					DashDir = DashLeft;
				else
					return;  //No dash was activated, return
			}
			else
				return;
			DashActive = true;
		}
	}
}