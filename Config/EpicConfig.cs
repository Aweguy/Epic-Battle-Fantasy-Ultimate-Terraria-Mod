#region Using directives

using Microsoft.Xna.Framework;
using System.ComponentModel;
using Terraria.ModLoader.Config;

#endregion Using directives

namespace EpicBattleFantasyUltimate.Config
{
    public class ClientSideConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(typeof(Vector2), "500,500")]
        [Range(0f, 1920f)]
        [Label("Limit Break Bar Position")]
        [Tooltip("The Postition of the Limit Break Bar")]
        public Vector2 LimitBarPosition { get; set; }
    }
}