using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class RenderGUISystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
        }

        public void Render(SpriteBatch sb)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<GUIComponent>())
            {
                if (entity.Value != null)
                {
                    GUIComponent guiComponent = (GUIComponent)entity.Value;
                    Texture2D texture = guiComponent.Texture;
                    Rectangle containerRect = new Rectangle(
                        guiComponent.ScreenPosition.X,
                        guiComponent.ScreenPosition.Y,
                        guiComponent.Texture.Width,
                        guiComponent.Texture.Height
                        );
                    sb.Begin();
                    sb.Draw(texture, containerRect, Color.White);
                    sb.End();
                }
            }
        }

        public void Load(ContentManager content)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<GUIComponent>())
            {
                GUIComponent guiComp = (GUIComponent)entity.Value;
                guiComp.Texture = content.Load<Texture2D>(guiComp.TextureName);
            }
        }
    }
}
