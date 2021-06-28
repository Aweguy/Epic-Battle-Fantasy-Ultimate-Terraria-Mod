using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class LightningShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Shard");
            Tooltip.SetDefault("You feel shock and awe when you hold this.\nRight-click to launch a spinning sword boomerang.\nThe item cannot be used while the boomerang is out.");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.melee = true;
            item.width = 104;
            item.height = 116;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 10;
            item.useAnimation = 10;
            item.knockBack = 5f;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void UseStyle(Player player)
        {
            player.itemLocation = player.Center + new Vector2(0, 5);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.damage = 50;
                item.melee = true;
                item.width = 104;
                item.height = 116;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.useTime = 10;
                item.useAnimation = 10;
                item.knockBack = 5f;
                item.value = Item.sellPrice(gold: 10);
                item.rare = ItemRarityID.Red;
                item.shoot = mod.ProjectileType("LightningShardCyclone");
                item.shootSpeed = 20f;
                item.autoReuse = false;
                item.useTurn = false;
                item.noMelee = true;
                item.UseSound = SoundID.Item1;
                item.noUseGraphic = true;
                return player.ownedProjectileCounts[item.shoot] < 1;
            }
            else
            {
                item.damage = 50;
                item.melee = true;
                item.width = 104;
                item.height = 116;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.useTime = 10;
                item.useAnimation = 10;
                item.knockBack = 5f;
                item.value = Item.sellPrice(gold: 10);
                item.rare = ItemRarityID.Red;
                item.UseSound = SoundID.Item1;
                item.shoot = ProjectileID.None;
                item.noMelee = false;
                item.autoReuse = true;
                item.useTurn = true;
                item.noUseGraphic = false;
            }
            return player.ownedProjectileCounts[mod.ProjectileType("LightningShardCyclone")] < 1;
        }

    }
}