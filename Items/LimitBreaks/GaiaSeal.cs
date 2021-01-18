using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Buffs.Buffs;
using System.Collections.Generic;

namespace EpicBattleFantasyUltimate.Items.LimitBreaks
{
    public class GaiaSeal : LimitItem
    {



        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gaia's Seal");
            Tooltip.SetDefault("A small emblem given to Greenwood’s defenders. Strikes foes with toxins while Gaia’s blessing shields you from debuffs.\n Gets stronger with each unique boss defeated");
        }

        public override void SetSafeDefaults()
        {
            item.width = 100;
            item.height = 100;

            item.damage = 100;
            item.magic = true;
            //item.mana = 100;
            LimitCost = 100;
            item.buffType = ModContent.BuffType<BlessedBuff>();
            item.buffTime = 60 * 60;

            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = ItemUseStyleID.SwingThrow;

            //item.channel = true; //Channel so that you can held the weapon [Important]


        }


        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }





        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //Rainbow Line

            var line = new TooltipLine(mod, "Gaia's Seal", "LIMIT BREAK!!!")
            {
                overrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)
            };
            tooltips.Add(line);


        }





        public override bool UseItem(Player player)
        {
           for (int i = 0; i < Player.MaxBuffs; ++i)
            {
                if (player.buffType[i] != 0 && Main.debuff[player.buffType[i]])
                {
                    player.DelBuff(i);
                    i--;
                }
            }




            for (int i = 0; i < Main.maxNPCs; i++)
            {

                NPC npc = Main.npc[i];

                if (!Main.npc[i].active)
                {
                    continue;
                }


                npc.AddBuff(BuffID.Poisoned, 60 * 600);

                if (player.whoAmI == Main.myPlayer)
                {


                    player.ApplyDamageToNPC(Main.npc[i], item.damage + (100 * EpicWorld.bossesDefeated), 0f, (npc.Center.X - player.Center.X > 0f).ToDirectionInt(), true);

                    

                }





            }

            return true;
        }





        /*public override void HoldItem(Player player)
        {
            item.damage = 100 + (100 * EpicWorld.bossesDefeated);


        }*/












    }
}
