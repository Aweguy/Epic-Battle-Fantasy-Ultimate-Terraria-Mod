#region Using directives

using System.ComponentModel;

using Terraria.ModLoader.Config;

using Microsoft.Xna.Framework;

#endregion

namespace EpicBattleFantasyUltimate.Config
{
    public class ClientSideConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(typeof(Vector2), "500, 500")]
        [Range(0f, 1920f)]
        [Label("Flair Slot Position")]
        [Tooltip("The position of the Flair Slots.")]
        public Vector2 FlairSlotPosition { get; set; }

        [DefaultValue(typeof(bool), "false")]
        [Label("Flair Slot Vertical Stack")]
        public bool VerticalStack { get; set; }


        [DefaultValue(typeof(Vector2), "500,500")]
        [Range(0f,1902f)]
        [Label("Limit Break Bar Position")]
        [Tooltip("The Postition of the Limit Break Bar")]

        public Vector2 LimitBarPosition { get; set; }




    }
}