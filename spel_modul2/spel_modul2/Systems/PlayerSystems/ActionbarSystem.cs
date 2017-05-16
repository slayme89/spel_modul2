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
        ActionBarComponent actionbarComp;
        public void Update(GameTime gameTime)
        {

            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                PlayerControlComponent playerControl = (PlayerControlComponent)entity.Value;
                if (cm.HasEntityComponent<ActionBarComponent>(entity.Key)){
                    ActionBarComponent actionbarComp = cm.GetComponentForEntity<ActionBarComponent>(entity.Key);
                    if (playerControl.theActionBar.IsButtonDown())
                    {
                        if (cm.HasEntityComponent<MoveComponent>(entity.Key) && cm.HasEntityComponent<AttackComponent>(entity.Key))
                        {
                            MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(entity.Key);
                            AttackComponent attackComp = cm.GetComponentForEntity<AttackComponent>(entity.Key);
                            if (actionbarComp.actionbarOpen)
                            {
                                attackComp.CanAttack = true;
                                moveComp.canMove = true;
                               
                                actionbarComp.actionbarOpen = false;
                            }
                            else
                            {
                                attackComp.CanAttack = false;
                                moveComp.canMove = false;
                                actionbarComp.actionbarOpen = true;
                            }
                        }
                    }
                }
            }
        }

        public void SelectSlot()
        {
        }
        /*  
        public bool AddInventoryItemToActionBar(int player, int item)
        {
          
        }
        */
    }
}