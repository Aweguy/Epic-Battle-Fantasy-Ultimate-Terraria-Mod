using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace EpicBattleFantasyUltimate.Items.Spellbooks
{
    public class SpellRegeneration : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regeneration Spell");
            Tooltip.SetDefault("This spell vastly increases your regeneration.\nCosts a lot of mana and has a big cooldown.");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 100;
            item.useAnimation = 10;
            item.mana = 25;
            item.rare = ItemRarityID.Yellow;
            item.value = Item.sellPrice(silver: 50);
            item.useTurn = true;
            item.value = Item.sellPrice(gold: 10);
        }

        public override bool CanUseItem(Player player)
        {
            int buff = mod.BuffType("RegenerationSated");
            return !player.HasBuff(buff);
        }

        public override bool UseItem(Player player)
        {
            player.AddBuff(mod.BuffType("Regeneration"), 60 * 10);
            player.AddBuff(mod.BuffType("RegenerationSated"), 60 * 30);

            return base.UseItem(player);
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RegenerationPotion, 250);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
