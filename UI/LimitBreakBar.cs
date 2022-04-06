using EpicBattleFantasyUltimate.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace EpicBattleFantasyUltimate.UI
{
    public class LimitBreakBar : UIState
    {
        private UIText text;
        private UIElement area;
        private UIImage barFrame;
        private Color gradientA;
        private Color gradientB;
        private Texture2D BarFilling;

        public override void OnInitialize()
        {
            area = new UIElement();
            //Position x
            area.Left.Set(-area.Width.Pixels - 720, 1f);
            //position y
            area.Top.Set(26, 0f);
            area.Width.Set(182, 0f);
            area.Height.Set(60, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("EpicBattleFantasyUltimate/UI/LimitBreakFrame"));
            barFrame.Left.Set(22, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(138, 0f);
            barFrame.Height.Set(34, 0f);

            text = new UIText("0/100", 0.8f);
            text.Width.Set(138, 0f);
            text.Height.Set(34, 0f);
            text.Top.Set(40, 0f);
            text.Left.Set(0, 0f);
            gradientA = Color.DarkRed;
            gradientB = Color.Red;

            area.Append(text);
            area.Append(barFrame);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var epicPlayer = EpicPlayer.ModPlayer(Main.LocalPlayer);
            float quotient = (float)epicPlayer.LimitCurrent / epicPlayer.MaxLimit2;

            BarFilling = (Texture2D)ModContent.Request<Texture2D>("EpicBattleFantasyUltimate/UI/LimitBreakBarFilling");
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 8;
            hitbox.Height -= 16;

            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i++)
            {
                float percent = (float)i / (right - left);
                spriteBatch.Draw(BarFilling, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
            }
        }

        public override void Update(GameTime gameTime)
        {
            var config = ModContent.GetInstance<ClientSideConfig>();

            Vector2 drawStart = config.LimitBarPosition;

            area.Left.Set(drawStart.X, 0f);
            area.Top.Set(drawStart.Y, 0f);

            var epicPlayer = EpicPlayer.ModPlayer(Main.LocalPlayer);
            text.SetText($"Limit Break: { epicPlayer.LimitCurrent} / { epicPlayer.MaxLimit2}");
            base.Update(gameTime);
        }
    }
}