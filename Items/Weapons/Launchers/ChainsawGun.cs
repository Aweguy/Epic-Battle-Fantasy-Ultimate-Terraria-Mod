using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
    public class ChainsawGun : EpicLauncher
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chainsaw Gun");
            Tooltip.SetDefault("The most ridiculous idea until the invention of Train Guns.\nDeals melee damage.");
        }

        public override void SetSafeDefaults()
        {
            item.width = 112;
            item.height = 56;

            item.useTime = 40;
            item.useAnimation = 40;

            item.damage = 77;
            item.crit = 3;
            item.melee = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 2);
            item.rare = ItemRarityID.Purple;

            item.UseSound = SoundID.Item38;
            item.shootSpeed = 12f;
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
            return new Vector2(-43, -11);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts, 3);
            recipe.AddIngredient(mod.ItemType("RawTitanium"), 2);
            recipe.AddIngredient(mod.ItemType("PristineDiamond"), 2);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}