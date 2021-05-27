using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
    public class Heartstopper : EpicLauncher
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heartstopper");
            Tooltip.SetDefault("They say your heartbeat is one of the last things you hear before you die.\nThis can fix that.");
        }

        public override void SetSafeDefaults()
        {
            item.width = 98;
            item.height = 60;

            item.useTime = 60;
            item.useAnimation = 60;

            item.damage = 60;
            item.crit = 2;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Purple;

            item.UseSound = SoundID.Item38;
            item.shootSpeed = 14f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 34f;
            //Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
            muzzleOffset += new Vector2(0, -9f * player.direction).RotatedBy(muzzleOffset.ToRotation());
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-32, -9);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts, 3);
            recipe.AddIngredient(ModContent.ItemType<AncientAmber>(), 4);
            recipe.AddIngredient(ModContent.ItemType<VoltaicTopaz>(), 4);
            recipe.AddIngredient(ItemID.DirtBlock, 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}