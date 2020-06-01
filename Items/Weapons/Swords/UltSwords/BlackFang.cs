using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;



namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class BlackFang : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Fang");
            Tooltip.SetDefault("'This weapon is totally historically accurate, I'm sure of it. I saw it in an anime once!' - Matt");
        }

        public override void SetDefaults()
        {
            item.damage = 66;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 30;
            item.useAnimation = 30;
            item.knockBack = 2f;
            item.value = Item.sellPrice(gold: 5);
            item.rare = 9;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }


        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60 * 5);
            player.statLife += 6;
            player.HealEffect(6);
        }









        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DarkShard);
            recipe.AddIngredient(ItemID.BladeofGrass);
            recipe.AddIngredient(mod.ItemType("VolcanicRuby"), 2);
            recipe.AddIngredient(ItemID.SoulofNight, 6);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }










    }
}
