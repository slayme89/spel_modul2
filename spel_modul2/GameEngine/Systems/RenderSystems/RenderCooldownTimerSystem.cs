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
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Systems.RenderSystems
{
    
    public class RenderCooldownTimerSystem : ISystem, IRenderSystem
    {
        SpriteFont cooldownTexture;
        public void Update(GameTime gameTime) { }
        public void Render(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            Viewport viewport = Extensions.GetCurrentViewport(renderHelper.graphicsDevice);
            Debug.WriteLine("inne i foreach");

            foreach (var entity in cm.GetComponentsOfType<SkillComponent>())
            {
                Debug.WriteLine("inne i foreach");
                CooldownComponent cd = cm.GetComponentForEntity<CooldownComponent>(entity.Key);
                ActionBarComponent ab = cm.GetComponentForEntity<ActionBarComponent>(entity.Key);
                Vector2 pos = new Vector2(200, 300);
                int cooldown = (int)cd.CooldownTimer;

                if(cooldown > 0)
                {
                    Debug.WriteLine("inne i cooldown if");
                    renderHelper.DrawString(cooldownTexture, cooldown.ToString(), pos, Color.Red, RenderLayer.GUI3);
                }
                
            }
        }

        public void Load(ContentManager content)
        {
            cooldownTexture = content.Load<SpriteFont>("SkillIcons/HeavyAttack");
        }
        
    }
}
