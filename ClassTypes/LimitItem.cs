using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.ClassTypes
{
    public abstract class LimitItem : ModItem
    {
        public override bool CloneNewInstances => true;

        public int LimitCost = 0;
        public int LimitGain = 0;
        public int LimitDrain = 0;

        public virtual void SetSafeDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            SetSafeDefaults();
        }

        public virtual bool CanUseSpecial(Player player)
        {
            var epicPlayer = EpicPlayer.ModPlayer(player);
            if (epicPlayer.LimitCurrent >= LimitCost)
            {
                epicPlayer.LimitCurrent += LimitGain;
                epicPlayer.LimitCurrent -= LimitCost;
                return true;
            }
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            return CanUseSpecial(player) && base.CanUseItem(player);
        }

        public override void HoldItem(Player player)
        {
            var epicPlayer = EpicPlayer.ModPlayer(player);

            epicPlayer.LimitCurrent -= LimitDrain;
        }
    }
}