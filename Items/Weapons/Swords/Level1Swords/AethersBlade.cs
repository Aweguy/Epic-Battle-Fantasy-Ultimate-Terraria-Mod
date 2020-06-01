using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.Level1Swords
{
    public class AethersBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aether's Blade");
            Tooltip.SetDefault("The weaker predecessor of the Heaven's Gate Sword.\nEssential for crafting Heaven's Gate");
        }


        public override void SetDefaults()
        {
            item.damage = 30;
            item.melee = true;
            item.width = 48;
            item.height = 48;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 27;
            item.useAnimation = 27;
            item.knockBack = 4f;
            item.value = Item.sellPrice(silver: 1);
            item.rare = ItemRarityID.Pink;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.material = true;
        }


    }	
}
