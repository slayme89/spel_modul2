using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Components;
using System.Diagnostics;

namespace GameEngine.Systems.RenderSystems
{
    
    public class RenderCooldownTimer : ISystem, IRenderSystem
    {
        private SpriteFont cooldownTexture;
        public void Update(GameTime gameTime) { }
        public void Render(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            Viewport viewport = Extensions.GetCurrentViewport(renderHelper.graphicsDevice);

            foreach (var entity in cm.GetComponentsOfType<SkillComponent>())
            {
                Debug.WriteLine("inne i foreach");
                SkillComponent skillComponent = (SkillComponent)entity.Value;
                CooldownComponent cd = cm.GetComponentForEntity<CooldownComponent>(entity.Key);
                ActionBarComponent ab = cm.GetComponentForEntity<ActionBarComponent>(entity.Key);
                Vector2 pos = new Vector2(ab.PositionOnScreen.X, ab.PositionOnScreen.Y);
                int cooldown = (int)cd.CooldownTimer;

                if(cooldown > 0)
                {
                    Debug.WriteLine("inne i cooldown if");
                    renderHelper.DrawString(cooldownTexture, cooldown.ToString(), pos, Color.Red, RenderLayer.Layer4);
                }
                
            }
        }

        
    }
}
