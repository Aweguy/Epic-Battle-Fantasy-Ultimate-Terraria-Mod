using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.ClassTypes;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
    public class BalanceBadge : Flair
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Balance Badge");
            Tooltip.SetDefault("Represents pure equilibrium and bestows a wealth of boosts.\n5% increase to all damage types\nIncreases maximum health and mana by 10\nIncreases movement and attack speed by 5%\nIncreases max minion slots by 1\nIncreases critical rates by 5%");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 19;
            item.defense = 5;
            item.accessory = true;
            item.rare = ItemRarityID.LightPurple;
        }



        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.05f;
            player.moveSpeed += 0.05f;
            player.statLifeMax2 += 10;
            player.statManaMax2 += 10;
            player.meleeSpeed += 0.05f;
            player.meleeCrit += 5;
            player.rangedCrit += 5;
            player.magicCrit += 5;
            player.maxMinions += 1;
        }



        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LightShard, 2);
            recipe.AddIngredient(ItemID.DarkShard, 2);
            recipe.AddIngredient(ItemID.SoulofLight, 4);
            recipe.AddIngredient(ItemID.SoulofNight, 4);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();



        }
    }
}
