using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public class RenderInteractSystem : IRenderSystem
    {
        public void Render(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach(var interactEntity in cm.GetComponentsOfType<InteractSystem>())
            {
                InteractComponent interact = (InteractComponent)interactEntity.Value;

                if (interact.IsActive == true)
                {
                    renderHelper.DrawString(interact.SpriteFont, interact.Text, interact.Position, Color.Black, RenderLayer.GUI2);
                }
            }

        }

        public void Load(ContentManager content)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<InteractComponent>())
            {
                InteractComponent interComp = (InteractComponent)entity.Value;
                interComp.SpriteFont = content.Load<SpriteFont>(interComp.SpriteFontPath);
            }
        }
    }

   

}
