using EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.ScarletCaster;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.SignatureItems
{
    public class ScarletCaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scarlet Caster");
            Tooltip.SetDefault("A torch-like staff that shoots two fireballs and a seeking firebat\n[c/FF0000:By Nab]");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 40;
            item.damage = 67;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.mana = 10;
            item.noMelee = true;
            item.magic = true;
            item.autoReuse = true;
            item.rare = -12;
            item.shoot = mod.ProjectileType("ScarletFireball");
            item.shootSpeed = 9.6f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<HellwingV2>(), damage, knockBack, player.whoAmI, 0f, 0f);

            int degrees = Main.rand.Next(10);
            float numberProjectiles = 2; // 2 shots
            float rotation = MathHelper.ToRadians(10); //10 degrees spread
            position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .75f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<ScarletFireball>(), damage, knockBack, player.whoAmI);
            }
            return false;
        }
    }
}