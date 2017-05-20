using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using GameEngine.Components;
using System.Diagnostics;
using GameEngine.Systems;
using Game.Components;

namespace Game.Systems
{
    class PlayerArmSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<ArmComponent>())
            {
                ArmComponent armComp = (ArmComponent)entity.Value;
                if (!cm.HasEntityComponent<PositionComponent>(armComp.playerID))
                    return;
                PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(entity.Key);

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
