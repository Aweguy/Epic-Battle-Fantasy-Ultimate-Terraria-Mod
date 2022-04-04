using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
    public class AngryFaic : Flair
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angry Faic");
            Tooltip.SetDefault("Wearing this makes you so angry, you want something to be BLAMMED!\n2 defense\nIncreases critical chance by 8% for all types of damage.\nIncreases damage by 8%.\nIncreases enemy aggression.");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 2;
            player.GetCritChance(DamageClass.Generic) += 8;
            player.GetDamage(DamageClass.Generic) += 0.08f;
            player.aggro += 450;


        }

        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.SoulBottleNight, 10)
                .AddIngredient(ItemID.HellstoneBar, 5)
                .AddIngredient(ModContent.ItemType<TargetBadge>())
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}