using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.StaffProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Staves
{
    public class DarkTooth : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Tooth");
            Tooltip.SetDefault("Ancient black magic staff used for Dark elemental magic. Creates a slowly growing black hole that explodes afterwards.\nConsumes Limit Break while active");
        }

        public override void SetDefaults()
        {
            Item.damage = 0;
            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.mana = 10;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(platinum: 1);
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<BlackHole>();
            Item.shootSpeed = 0f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.channel = true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }

        public override void HoldItem(Player player)
        {
            Color drawColor = Color.Black;
            if (Main.rand.Next(2) == 0)
            {
                drawColor = Color.Red;
            }

            if (player.channel)
            {
                Dust.NewDustDirect(player.position, player.width, player.height, 302, 0f, 0f, 0, drawColor, 1f);
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 20)
                .AddIngredient(ItemID.DarkShard, 15)
                .AddIngredient(ModContent.ItemType<DarkMatter>(), 50)
                .AddIngredient(ItemID.FragmentNebula, 10)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}