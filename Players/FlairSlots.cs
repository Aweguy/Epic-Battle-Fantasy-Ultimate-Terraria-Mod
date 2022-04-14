using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Config;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Players
{

	public class FlairSlot1 : ModAccessorySlot
	{
		/*bool VerticalStack = ModContent.GetInstance<EpicConfig>().FlairVerticalStack;
		Vector2 position = ModContent.GetInstance<EpicConfig>().FlairSlotPosition;
		float padding = 0;*/

		//public override Vector2? CustomLocation => position;

		public override void OnMouseHover(AccessorySlotType context)
		{
			Main.hoverItemName = "Flair";
		}

		public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			if (ModContent.GetModItem(checkItem.type) is Flair) // if is Wing, then can go in slot
				return true;

			return false; // Otherwise nothing in slot
		}

		// Designates our slot to be a priority for putting wings in to. NOTE: use ItemLoader.CanEquipAccessory if aiming for restricting other slots from having wings!
		public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
		{
			if (ModContent.GetModItem(item.type) is Flair) // If is Wing, then we want to prioritize it to go in to our slot.
				return true;

			return false;
		}
		
		public override bool DrawVanitySlot => false;
		public override bool DrawDyeSlot => false;

		// Icon textures. Nominal image size is 32x32. Will be centered on the slot.
		public override string FunctionalTexture => "Terraria/Images/Item_" + ItemID.CreativeWings;
	}
	public class FlairSlot2 : ModAccessorySlot
	{
		public override void OnMouseHover(AccessorySlotType context)
		{
			Main.hoverItemName = "Flair";
		}

		public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			if (ModContent.GetModItem(checkItem.type) is Flair) // if is Wing, then can go in slot
				return true;

			return false; // Otherwise nothing in slot
		}

		// Designates our slot to be a priority for putting wings in to. NOTE: use ItemLoader.CanEquipAccessory if aiming for restricting other slots from having wings!
		public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
		{
			if (ModContent.GetModItem(item.type) is Flair) // If is Wing, then we want to prioritize it to go in to our slot.
				return true;

			return false;
		}

		public override bool DrawVanitySlot => false;
		public override bool DrawDyeSlot => false;

		// Icon textures. Nominal image size is 32x32. Will be centered on the slot.
		public override string FunctionalTexture => "Terraria/Images/Item_" + ItemID.CreativeWings;
	}

	public class FlairSlot3 : ModAccessorySlot
	{
        public override void OnMouseHover(AccessorySlotType context)
        {
			Main.hoverItemName = "Flair";
        }
        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
		{
			if (ModContent.GetModItem(checkItem.type) is Flair) // if is Wing, then can go in slot
				return true;

			return false; // Otherwise nothing in slot
		}

		// Designates our slot to be a priority for putting wings in to. NOTE: use ItemLoader.CanEquipAccessory if aiming for restricting other slots from having wings!
		public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
		{
			if (ModContent.GetModItem(item.type) is Flair) // If is Wing, then we want to prioritize it to go in to our slot.
				return true;

			return false;
		}

		public override bool DrawVanitySlot => false;
		public override bool DrawDyeSlot => false;

		// Icon textures. Nominal image size is 32x32. Will be centered on the slot.
		public override string FunctionalTexture => "Terraria/Images/Item_" + ItemID.CreativeWings;
	}
}





