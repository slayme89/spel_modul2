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
    class PlayerArmSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<ArmComponent>())
            {
                PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                ArmComponent armComp = (ArmComponent)entity.Value;
                if (armComp.playerID == 0)
                    armComp.playerID = GetId(cm);
                posComp.Position = cm.GetComponentForEntity<PositionComponent>(armComp.playerID).Position;
            }
        }

        int GetId(ComponentManager cm)
        {
            int freeID = 0;
            foreach (ArmComponent arm in cm.GetComponentsOfType<ArmComponent>().Values)
            {
                int countToTwo = 0;
                int numbofPlayers = 0;
                foreach (var playerEntity in cm.GetComponentsOfType<PlayerControlComponent>())
                {
                    if (arm.playerID != playerEntity.Key)
                    {
                        countToTwo++;
                        freeID = playerEntity.Key;
                    }
                    numbofPlayers++;
                }
                if (countToTwo == numbofPlayers)
                    return freeID;
            }
            return 0;
        }
    }
}
