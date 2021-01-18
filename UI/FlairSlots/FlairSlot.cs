using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using CustomSlot;
using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Accessories.Flairs;
using EpicBattleFantasyUltimate.Config;

namespace EpicBattleFantasyUltimate.UI.FlairSlots
{
    public class FlairSlot : UIState
    {

		public CustomItemSlot[] FlairSlots = new CustomItemSlot[3];

		public bool Visible
		{
			get => Main.playerInventory;
		}

		public override void OnInitialize()
		{
			CroppedTexture2D emptyTexture = new CroppedTexture2D(
			ModContent.GetInstance<EpicBattleFantasyUltimate>().GetTexture("UI/FlairSlots/FlairSlot"),
			CustomItemSlot.DefaultColors.EmptyTexture);

			for (int i = 0; i < FlairSlots.Length; ++i)
			{
				FlairSlots[i] = new CustomItemSlot(ItemSlot.Context.EquipAccessory, 0.85f)
				{
					IsValidItem = item => item.modItem is Flair,
					HoverText = "Flair"
				};

				FlairSlots[i].Left.Set(85 + 50 * i, 0f);
				FlairSlots[i].Top.Set(645, 0f);
				this.Append(FlairSlots[i]);
			}
		}


		public override void Update(GameTime gameTime)
		{
			var config = ModContent.GetInstance<ClientSideConfig>();

			float padding = 50;
			bool verticalStack = config.VerticalStack;
			Vector2 drawStart = config.FlairSlotPosition;

			for (int i = 0; i < FlairSlots.Length; ++i)
			{
				FlairSlots[i].Left.Set(drawStart.X + (verticalStack ? 0 : i * padding), 0f);
				FlairSlots[i].Top.Set(drawStart.Y + (verticalStack ? i * padding : 0), 0f);
			}

			base.Update(gameTime);
		}



	}
}
