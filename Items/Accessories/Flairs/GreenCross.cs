using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Bows;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
    public class GreenCross : Flair
    {
        private int timer = 60 * 10;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Cross");
            Tooltip.SetDefault("A Geneva-Convention-friendly pin which boasts regenerative properties.\nRegenerates 5% of your maximum health every 10 seconds");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 19;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.HasItem(ModContent.ItemType<GaiasGift>()))
            {
                if (--timer == 0)
                {
                    int regen = (player.statLifeMax2 / 100) * 10;
                    player.statLife += regen;
                    player.HealEffect(regen);
                    timer = 60 * 10;
                }
                player.lifeRegen += 8;
            }
            else
            {
                if (--timer == 0)
                {
                    int regen = (player.statLifeMax2 / 100) * 5;
                    player.statLife += regen;
                    player.HealEffect(regen);
                    timer = 60 * 10;
                }
                player.lifeRegen += 5;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.RegenerationPotion, 3)
                .AddIngredient(ItemID.LifeCrystal, 5)
                .AddIngredient(ItemID.PixieDust, 10)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}