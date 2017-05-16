using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class ActionBarSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {

            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                PlayerControlComponent playerControl = (PlayerControlComponent)entity.Value;
                if (cm.HasEntityComponent<ActionBarComponent>(entity.Key)){
                    ActionBarComponent actionbarComp = cm.GetComponentForEntity<ActionBarComponent>(entity.Key);
                    if (playerControl.ActionBar1.IsButtonDown() && actionbarComp.Skills[0] != null)
                    {
                        actionbarComp.Skills[0].IsActivated = true;
                    }else if (playerControl.ActionBar2.IsButtonDown() && actionbarComp.Skills[1] != null)
                    {
                        actionbarComp.Skills[1].IsActivated = true;
                    }
                    else if (playerControl.ActionBar3.IsButtonDown() && actionbarComp.Skills[2] != null)
                    {
                        actionbarComp.Skills[2].IsActivated = true;
                    }
                    else if (playerControl.ActionBar4.IsButtonDown() && actionbarComp.Skills[3] != null)
                    {
                        actionbarComp.Skills[3].IsActivated = true;
                    }
                }
            }
        }

        void useSkill(ComponentManager cm, int entityId)
        {
            foreach (var skill in cm.GetComponentsForEntity(entityId))
            {

            }
        }
    }
}