using EpicBattleFantasyUltimate.Projectiles.SwordProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class GaiaAxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gaia's Axe");
            Tooltip.SetDefault("'The woodsman slices the wood of trees by day, and the bones of fiends by night.'\n Right-Click to throw a small axe. Cannot be used while the axe is thrown.");
        }

        public override void SetDefaults()
        {
            item.width = 72;
            item.height = 72;

            item.damage = 130;
            item.knockBack = 5f;
            item.melee = true;

            item.useTime = 30;
            item.useAnimation = 30;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;

            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
        }



    }
}