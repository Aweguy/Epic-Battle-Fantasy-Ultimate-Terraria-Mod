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

namespace EpicBattleFantasyUltimate.UI.FlairSlots
{
    public class FlairSlot : UIState
    {

        public CustomItemSlot MyNormalSlot;
        public CustomItemSlot FlairSlot1 = new CustomItemSlot(ItemSlot.Context.EquipAccessory, 0.85f);
         

        public bool Visible
        {
            get => Main.playerInventory; // how do you display your slot?

        }

        #region OnInitialize

        public override void OnInitialize()
        {
            // add a texture to display when the accessory slot is empty
            CroppedTexture2D emptyTexture = new CroppedTexture2D(
                ModContent.GetInstance<EpicBattleFantasyUltimate>().GetTexture("UI/FlairSlots/FlairSlot"),
                CustomItemSlot.DefaultColors.EmptyTexture);

            //MyNormalSlot = new CustomItemSlot(); // leave blank for a plain inventory space

            

            FlairSlot1 = new CustomItemSlot(ItemSlot.Context.EquipAccessory, 0.85f)
            {

                IsValidItem =  item => (ModContent.GetModItem(item.type) as Flair) != null, //ModContent.GetModItem(item.type) is  Flair, // what do you want in the slot?

                

                HoverText = "Flair" // try to describe what will go into the slot
            };



            // you can set these once or change them in DrawSelf()
            //MyNormalSlot.Left.Set(85, 0);
            //MyNormalSlot.Top.Set(645, 0);

            FlairSlot1.Left.Set(85, 0);
            FlairSlot1.Top.Set(645, 0);


            // don't forget to add them to the UIState!
            //Append(MyNormalSlot);
            Append(FlairSlot1);


        }

        #endregion


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }



    }
}
