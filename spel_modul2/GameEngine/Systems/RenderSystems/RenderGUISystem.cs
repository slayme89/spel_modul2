using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public class RenderGUISystem : IRenderSystem
    {
        public void Render(RenderHelper rh)
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
                    rh.Draw(texture, containerRect, Color.White, RenderLayer.GUI2);
                }
            }
        }

        public void Load(ContentManager content)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<GUIComponent>())
            {
                GUIComponent guiComp = (GUIComponent)entity.Value;
                guiComp.Texture = content.Load<Texture2D>(guiComp.TexturePath);
            }
        }
    }
}
