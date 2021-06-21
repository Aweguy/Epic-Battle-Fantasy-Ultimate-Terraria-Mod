using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using EpicBattleFantasyUltimate.Items.Weapons.Swords.Level1Swords;
using EpicBattleFantasyUltimate.Projectiles.SwordProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class HeavensGate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heaven's Gate"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A legendary sword belonging to a line of famed corsairs.");
        }

        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;

            item.damage = 50;
            item.knockBack = 8f;
            item.melee = true;

            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.autoReuse = true;
            item.channel = true;

            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;

            item.shootSpeed = 12f;
            item.shoot = ModContent.ProjectileType<LightBlade>();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 Velocity = new Vector2(speedX, speedY);

            Projectile.NewProjectile(Main.MouseWorld - (Vector2.Normalize(Velocity) * 80f), Vector2.Zero, type, damage, knockBack, player.whoAmI,speedX,speedY);

            return false;
        }


        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(3) == 0)
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.AncientLight);
            }
        }

        public override bool OnlyShootOnSwing => true;

    }
}