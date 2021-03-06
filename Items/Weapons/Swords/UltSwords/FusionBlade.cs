using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class FusionBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fusion Blade"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Modeled after the weapons used by the MILITIA branch.\nShoots a big bullet.");
        }

        public override void SetDefaults()
        {
            item.damage = 69;
            item.melee = true;
            item.width = 54;
            item.height = 60;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 8f;
            item.value = Item.sellPrice(gold: 4);
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shootSpeed = 5f;
            item.shoot = mod.ProjectileType("BulletBob");
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(3) == 0)
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Fire);
        }

    }
}