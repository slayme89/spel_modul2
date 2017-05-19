using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public class RenderTextSystem : IRenderSystem
    {
        public void Render(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach(var textEntity in cm.GetComponentsOfType<TextComponent>())
            {
                TextComponent text = (TextComponent)textEntity.Value;

                //If its a text to display
                if (text.IsActive)
                {
                        renderHelper.DrawString(text.SpriteFont, text.Text, text.Position, text.Color, RenderLayer.GUI3);
                }
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
