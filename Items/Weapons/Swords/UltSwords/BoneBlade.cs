using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.Buffs.Debuffs;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class BoneBlade : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Blade");
            Tooltip.SetDefault("Also known as the Macabre Machete, and the Rib Rapier.\nWeakens targets on hit.");
        }


        public override void SetDefaults()
        {
            item.damage = 45;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 45;
            item.useAnimation = 45;
            item.knockBack = 2f;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }


        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Weaken>(), 60 * 5);
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Bone, 100);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
















    }
}
