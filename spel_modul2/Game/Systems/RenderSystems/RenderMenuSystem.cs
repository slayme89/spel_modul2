using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Systems
{
    public class RenderMenuSystem : IRenderSystem
    {
        public void Render(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            bool anyActive = false;
            //Buttons
            foreach (var but in cm.GetComponentsOfType<MenuButtonComponent>())
            {
                MenuButtonComponent button = (MenuButtonComponent)but.Value;
                if (button.IsActive)
                {
                    if (button.Ishighlighted == false)
                        renderHelper.Draw(button.NormalTexture, button.Position, button.Layer);
                    else if (button.Ishighlighted == true)
                        renderHelper.Draw(button.HighlightTexture, button.Position, button.Layer);
                    anyActive = true;
                }
            }
            //Backgrounds
            foreach (var background in cm.GetComponentsOfType<MenuBackgroundComponent>())
            {
                MenuBackgroundComponent backgroundComp = (MenuBackgroundComponent)background.Value;
                if (backgroundComp.IsActive)
                {
                    if (backgroundComp.HasFadingEffect)
                    {
                        Rectangle containerRect = new Rectangle(
                        backgroundComp.Position.X,
                        backgroundComp.Position.Y,
                        renderHelper.graphicsDevice.Viewport.TitleSafeArea.Width * 2,
                        renderHelper.graphicsDevice.Viewport.TitleSafeArea.Width * 2
                        );
                        renderHelper.Draw(backgroundComp.Texture, containerRect, new Color(255f, 255f, 255f, (byte)MathHelper.Clamp(backgroundComp.mAlphaValue, 0, 255)), RenderLayer.Menubackground);
                    }
                    else if (backgroundComp.HasMovingEffect)
                    {
                        Rectangle containerRect = new Rectangle(
                        backgroundComp.Position.X,
                        backgroundComp.Position.Y,
                        renderHelper.graphicsDevice.Viewport.TitleSafeArea.Width * 2,
                        renderHelper.graphicsDevice.Viewport.TitleSafeArea.Width * 2
                        );
                        renderHelper.Draw(backgroundComp.Texture, containerRect, Color.White, RenderLayer.Menubackground);
                    }
                    anyActive = true;
                }
            }
            //Title
            foreach (var entity in cm.GetComponentsOfType<MenuTitleComponent>())
                if (anyActive)
                    renderHelper.Draw(((MenuTitleComponent)entity.Value).Texture, ((MenuTitleComponent)entity.Value).Position, ((MenuTitleComponent)entity.Value).Layer);
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
            //Title
            foreach(var entity in cm.GetComponentsOfType<MenuTitleComponent>())
                ((MenuTitleComponent)entity.Value).Texture = content.Load<Texture2D>(((MenuTitleComponent)entity.Value).TexturePath);
        }
    }
}
