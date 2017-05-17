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
                    if (playerControl.ActionBar1.IsButtonDown() && actionbarComp.Slots[0] != null)
                    {
                        if (actionbarComp.Slots[0].IsItem)
                            actionbarComp.Slots[0].Use(entity.Key);
                        else
                            ((SkillComponent)actionbarComp.Slots[0]).IsActivated = true;
                    }
                    else if (playerControl.ActionBar2.IsButtonDown() && actionbarComp.Slots[1] != null)
                    {
                        if (actionbarComp.Slots[1].IsItem)
                            actionbarComp.Slots[1].Use(entity.Key);
                        else
                            ((SkillComponent)actionbarComp.Slots[0]).IsActivated = true;
                    }
                    else if (playerControl.ActionBar3.IsButtonDown() && actionbarComp.Slots[2] != null)
                    {
                        if (actionbarComp.Slots[2].IsItem)
                            actionbarComp.Slots[2].Use(entity.Key);
                        else
                            ((SkillComponent)actionbarComp.Slots[0]).IsActivated = true;
                    }
                    else if (playerControl.ActionBar4.IsButtonDown() && actionbarComp.Slots[3] != null)
                    {
                        if (actionbarComp.Slots[3].IsItem)
                            actionbarComp.Slots[3].Use(entity.Key);
                        else
                            ((SkillComponent)actionbarComp.Slots[0]).IsActivated = true;
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