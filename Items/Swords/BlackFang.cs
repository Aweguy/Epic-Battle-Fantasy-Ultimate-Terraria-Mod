using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Swords
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
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee;;
            Item.width = 64;
            Item.height = 64;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.knockBack = 2f;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60 * 5);
            if(player.statLife < player.statLifeMax)
            {
                int HealthHeal = (int)(damage / 10);
                player.statLife += HealthHeal;
                player.HealEffect(HealthHeal);
            }
        }

    }
}