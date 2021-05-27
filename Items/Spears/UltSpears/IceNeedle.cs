using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Spears.UltSpears
{
    public class IceNeedle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Needle");
            Tooltip.SetDefault("An icicle massive and sharp enough to be a jousting lance.");
        }

        public override void SetDefaults()
        {
            item.damage = 60;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 20;
            item.useTime = 30;
            item.shootSpeed = 3.7f;
            item.knockBack = 6.5f;
            item.width = 32;
            item.height = 32;
            item.scale = 1f;
            item.rare = ItemRarityID.Pink;
            item.value = Item.sellPrice(gold: 10);
            item.melee = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (Main.rand.Next(5) == 0)
            {
                target.AddBuff(BuffID.Frostburn, 60 * 3);
            }
        }

        public override void UseStyle(Player player)
        {
            player.itemLocation = player.Center + new Vector2(0, 3);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.damage = 60;
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.useAnimation = 20;
                item.useTime = 30;
                item.shootSpeed = 3.7f;
                item.knockBack = 6.5f;
                item.width = 32;
                item.height = 32;
                item.scale = 1f;
                item.rare = ItemRarityID.Pink;
                item.value = Item.sellPrice(gold: 10);
                item.melee = true;
                item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
                item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
                item.shoot = mod.ProjectileType("IceNeedleProj");
                item.autoReuse = false;
            }
            else
            {
                item.damage = 60;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.useAnimation = 20;
                item.useTime = 30;
                item.shootSpeed = 3.7f;
                item.knockBack = 6.5f;
                item.width = 32;
                item.height = 32;
                item.scale = 1f;
                item.rare = ItemRarityID.Pink;
                item.value = Item.sellPrice(gold: 10);
                item.melee = true;
                item.noMelee = false; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
                item.noUseGraphic = false; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
                item.shoot = ProjectileID.None;
                item.autoReuse = true;
            }
            return player.ownedProjectileCounts[mod.ProjectileType("IceNeedleProj")] < 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("SolidWater"), 10);
            recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Silver", 10);
            recipe.AddIngredient(mod.ItemType("AbyssalSapphire"), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}