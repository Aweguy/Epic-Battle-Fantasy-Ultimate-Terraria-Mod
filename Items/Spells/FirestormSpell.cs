using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace EpicBattleFantasyUltimate.Items.Spells
{
    public class FirestormSpell : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firestorm Spell");
            Tooltip.SetDefault("You will have a HELL of a time.");
        }

        public override void SetDefaults()
        {
            item.damage = 70;
            item.width = 28;
            item.height = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 100;
            item.useAnimation = 100;
            item.mana = 30;
            item.rare= ItemRarityID.Purple;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Firestorm");
            item.shootSpeed = 4f;
			item.noMelee = true;
            item.magic = true;
            item.scale = 1.2f;
            item.value = 10000000;
        }



        


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LivingFireBlock, 1000);
            recipe.AddIngredient(ItemID.SoulofFright, 30);
            recipe.AddIngredient(ItemID.FragmentSolar, 10);
            recipe.AddIngredient(mod.ItemType("FireballSpell"));
            recipe.AddTile(TileID.Bookcases);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
