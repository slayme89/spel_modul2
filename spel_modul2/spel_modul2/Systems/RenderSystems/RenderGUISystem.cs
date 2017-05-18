using GameEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    class RenderGUISystem : IRenderSystem
    {
        public void Render(RenderHelper rh)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<GUIComponent>())
            {
                if (entity.Value != null)
                {
                    GUIComponent guiComponent = (GUIComponent)entity.Value;
                    int x = guiComponent.ScreenPosition == GUIPosition.Left ? 0 : rh.graphicsDevice.Viewport.TitleSafeArea.Right - 108;
                    int y = rh.graphicsDevice.Viewport.TitleSafeArea.Top;

                    Texture2D texture = guiComponent.Texture;
                    Rectangle containerRect = new Rectangle(
                        x,
                        y,
                        guiComponent.Texture.Width,
                        guiComponent.Texture.Height
                        );
                    rh.Draw(texture, containerRect, Color.White, RenderLayer.Foreground2);
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
