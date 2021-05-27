using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
    public class ThunderCore : EpicLauncher
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunder Core");
            Tooltip.SetDefault("A portable powerhouse that is yellow despite all color schemes. Runs on clean Plutonium just below critical mass.\nShoots in 2 shot bursts.\nIncreases your speed by 20% when having Thunder Revolver in your inventory.");
        }

        public override void SetSafeDefaults()
        {
            item.width = 100;
            item.height = 52;

            item.useTime = 15;
            item.useAnimation = 30;
            item.reuseDelay = 20;

            item.damage = 55;
            item.crit = 1;
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
            muzzleOffset += new Vector2(0, -13.5f * player.direction).RotatedBy(muzzleOffset.ToRotation());
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30, -9);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts, 3);
            recipe.AddIngredient(mod.ItemType("PlutoniumCore"), 3);
            recipe.AddIngredient(mod.ItemType("VoltaicTopaz"), 2);
            recipe.AddIngredient(ItemID.Glass, 100);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}