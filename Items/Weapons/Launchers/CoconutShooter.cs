using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
    public class CoconutShooter : EpicLauncher
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coconut Shooter");
            Tooltip.SetDefault("A gorilla's bazooka. Beat your chest with open palms before use.\nHigh damage with huge knockback, but slow fire rate and velocity.\nPassively boosts your max health by 50 if coconut gun us in your inventory.");
        }

        public override void SetSafeDefaults()
        {
            item.width = 94;
            item.height = 70;

            item.useTime = 60;
            item.useAnimation = 60;
            item.reuseDelay = 10;

            item.damage = 110;
            item.knockBack = 20f;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 3);
            item.rare = ItemRarityID.Purple;

            item.UseSound = SoundID.Item38;
            item.shootSpeed = 5f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 29f;
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
            return new Vector2(-38, -5);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts, 3);
            recipe.AddIngredient(ItemID.Wood, 120);
            recipe.AddIngredient(mod.ItemType("CyclonicEmerald"), 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}