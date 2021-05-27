using EpicBattleFantasyUltimate.Items.Ammo.Shots;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.ClassTypes
{
    public abstract class EpicLauncher : ModItem
    {
        #region Constants and Variables

        private const float MAX_CHARGE = 120f;

        private int Charge;

        public bool IsAtMaxCharge => Charge == MAX_CHARGE;

        #endregion Constants and Variables

        public override bool CloneNewInstances => true;

        public virtual void SetSafeDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            SetSafeDefaults();

            item.shoot = ProjectileID.PurificationPowder;
            item.useAmmo = ItemType<Shot>();
            item.useStyle = ItemUseStyleID.HoldingOut;
        }

        public override bool CanUseItem(Player player)
        {
            int buff = mod.BuffType("Overheat");
            return !player.HasBuff(buff);
        }
    }
}