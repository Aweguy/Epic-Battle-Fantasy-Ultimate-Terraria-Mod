using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class LightningShard : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Shard");
            Tooltip.SetDefault("When you hold it, you are at 'Shock and Awe'.\nRight-click to launch a spinning sword boomerang.\nThe item cannot be used while the boomerang is out.");
        }


        public override void SetDefaults()
        {
            item.damage = 50;
            item.melee = true;
            item.width = 104;
            item.height = 116;
            item.useStyle = 1;
            item.useTime = 10;
            item.useAnimation = 10;
            item.knockBack = 5f;
            item.value = Item.sellPrice(gold: 10);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }



        public override void UseStyle(Player player)
        {

            player.itemLocation = player.Center + new Vector2(0, 5);
        }


        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.damage = 50;
                item.melee = true;
                item.width = 104;
                item.height = 116;
                item.useStyle = 1;
                item.useTime = 10;
                item.useAnimation = 10;
                item.knockBack = 5f;
                item.value = Item.sellPrice(gold: 10);
                item.rare = 10;
                item.shoot = mod.ProjectileType("LightningShardCyclone");
                item.shootSpeed = 20f;
                item.autoReuse = false;
                item.useTurn = false;
                item.noMelee = true;
                item.UseSound = SoundID.Item1;
                item.noUseGraphic = true;
                return player.ownedProjectileCounts[item.shoot] < 1;
            }


            else
            {
                item.damage = 50;
                item.melee = true;
                item.width = 104;
                item.height = 116;
                item.useStyle = 1;
                item.useTime = 10;
                item.useAnimation = 10;
                item.knockBack = 5f;
                item.value = Item.sellPrice(gold: 10);
                item.rare = 10;
                item.UseSound = SoundID.Item1;
                item.shoot = 0;
                item.noMelee = false;
                item.autoReuse = true;
                item.useTurn = true;
                item.noUseGraphic = false;
            }
            return player.ownedProjectileCounts[mod.ProjectileType("LightningShardCyclone")] < 1;

        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Gold", 10);
            recipe.AddIngredient(mod.ItemType("VoltaicTopaz"), 4);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }












    }
}
