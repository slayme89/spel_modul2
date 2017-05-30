using Microsoft.Xna.Framework;
using GameEngine.Managers;
using GameEngine.Components;

namespace GameEngine.Systems
{
    public class TextDurationSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach(var entity in cm.GetComponentsOfType<TextComponent>())
            {
                TextComponent textComp = (TextComponent)entity.Value;
                if (textComp.IsActive)
                {
                    if (textComp.Duration <= 0)
                        textComp.IsActive = false;
                    else
                        textComp.Duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                    textComp.Duration = 10.0f;  
            }
        }
    }
}
