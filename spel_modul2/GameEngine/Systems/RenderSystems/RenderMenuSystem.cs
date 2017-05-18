using GameEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    class RenderMenuSystem : IRenderSystem
    {
        public void Render(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            //Buttons
            foreach (var but in cm.GetComponentsOfType<MenuButtonComponent>())
            {
                MenuButtonComponent button = (MenuButtonComponent)but.Value;

                if (button.IsActive && button.Ishighlighted == false)
                    renderHelper.Draw(button.NormalTexture, button.Position, button.Layer);
                else if (button.IsActive && button.Ishighlighted == true)
                    renderHelper.Draw(button.HighlightTexture, button.Position, button.Layer);
            }
            //Backgrounds
            foreach (var background in cm.GetComponentsOfType<MenuBackgroundComponent>())
            {
                MenuBackgroundComponent backgroundComp = (MenuBackgroundComponent)background.Value;
                if (backgroundComp.IsActive)
                {
                    Rectangle containerRect = new Rectangle(
                    backgroundComp.Position.X,
                    backgroundComp.Position.Y,
                    backgroundComp.Texture.Width,
                    backgroundComp.Texture.Height
                    );
                    renderHelper.Draw(backgroundComp.Texture, containerRect, Color.White, RenderLayer.Menubackground);
                }
            }
        }

        public void Load(ContentManager content)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            //Buttons
            foreach (var entity in cm.GetComponentsOfType<MenuButtonComponent>())
            {
                MenuButtonComponent buttonComp = (MenuButtonComponent)entity.Value;
                buttonComp.NormalTexture = content.Load<Texture2D>(buttonComp.NormalTexturePath);
                buttonComp.HighlightTexture = content.Load<Texture2D>(buttonComp.HighlightTexturePath);
            }
            //Backgrounds
            foreach (var entity in cm.GetComponentsOfType<MenuBackgroundComponent>())
            {
                MenuBackgroundComponent backComp = (MenuBackgroundComponent)entity.Value;
                backComp.Texture = content.Load<Texture2D>(backComp.TexturePath);
            }
        }
    }
}
