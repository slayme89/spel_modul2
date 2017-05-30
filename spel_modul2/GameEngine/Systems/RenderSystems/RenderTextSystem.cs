using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public class RenderTextSystem : IRenderSystem
    {
        public void Render(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach (var textEntity in cm.GetComponentsOfType<TextComponent>())
            {
                TextComponent text = (TextComponent)textEntity.Value;

                //If textType is DIalogBox, render it in the dialogbox
                if(text.IsActive && text.Type == TextType.DialogBox)
                    renderHelper.DrawString(text.SpriteFont,
                        text.Text,
                        new Vector2(
                            renderHelper.graphicsDevice.Viewport.TitleSafeArea.Center.X - 240,
                            renderHelper.graphicsDevice.Viewport.TitleSafeArea.Bottom - 75
                            ),
                        text.Color,
                        RenderLayer.GUI3);

                //If a text is active, display it
                else if (text.IsActive)
                    renderHelper.DrawString(text.SpriteFont, text.Text, text.Position, text.Color, RenderLayer.GUI3);
            }
        }

        public void Load(ContentManager content)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<TextComponent>())
            {
                TextComponent textComp = (TextComponent)entity.Value;
                textComp.SpriteFont = content.Load<SpriteFont>(textComp.SpriteFontPath);
            }
        }
    }
}
