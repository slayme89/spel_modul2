using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using GameEngine.Components;
using System.Diagnostics;

namespace GameEngine.Systems
{
    class PlayerSpriteTurnSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach(var entity in cm.GetComponentsOfType<ArmComponent>())
            {
                ArmComponent armComp = (ArmComponent)entity.Value;
                MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(armComp.playerID);
                AnimationGroupComponent armAnimation = cm.GetComponentForEntity<AnimationGroupComponent>(entity.Key);
                if (!cm.HasEntityComponent<AnimationGroupComponent>(armComp.playerID))
                    return;
                AnimationGroupComponent playerAnimation = cm.GetComponentForEntity<AnimationGroupComponent>(armComp.playerID);
                int animation = getAnimationRow(moveComp.Direction);
                Debug.WriteLine(animation + "    " + moveComp.Direction);
                armAnimation.ActiveAnimation = animation;
                playerAnimation.ActiveAnimation = animation;
            }
        }

        int getAnimationRow(Point direction)
        {

            if (direction.X > 0 && direction.Y == 0)
                return 3;
            else if (direction.X == 0 && direction.Y > 0)
                return 0;
            else if (direction.X < 0 && direction.Y == 0)
                return 1;
            else
                return 2;
        }
    }
}
