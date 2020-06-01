using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;



namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class Berzerker : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berzerker");
            Tooltip.SetDefault("A heavy weapon with a stone edge and unyielding rage.");
        }

        public override void SetDefaults()
        {
            item.damage = 150;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 50;
            item.useAnimation = 50;
            item.knockBack = 10f;
            item.value = Item.sellPrice(gold: 10);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.scale = 2f;
        }


        public override bool AltFunctionUse(Player player)
        {
            return true;
        }


        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 3;
                item.useTime = 50;
                item.useAnimation = 50;
                item.damage = 250;
                item.width = 64;
                item.height = 64;
                item.knockBack = 5f;
                item.value = Item.sellPrice(gold: 10);
                item.UseSound = SoundID.Item1;
                item.buffType = mod.BuffType("Tired");
                item.buffTime = 300;

            }
            else
            {
                item.damage = 150;
                item.melee = true;
                item.width = 64;
                item.height = 64;
                item.useTime = 50;
                item.useAnimation = 50;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.knockBack = 10f;
                item.value = Item.sellPrice(gold: 10);
                item.rare = 10;
                item.UseSound = SoundID.Item1;
                item.buffType = mod.BuffType("Tired");
                item.buffTime = 300;
            }
            return base.CanUseItem(player);




        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Titanium", 20);
            recipe.AddIngredient(ItemID.Leather, 2);
            recipe.AddIngredient(ItemID.SoulofMight, 4);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

        

        }










    }
}
