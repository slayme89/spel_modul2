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
        int armID;

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
                    armComp.playerID = GetId(cm, armComp);
                Vector2 nextPos = new Vector2(0.0f, 0.0f);
                if(cm.HasEntityComponent<MoveComponent>(armComp.playerID))
                    nextPos = CalculatePlayerNextPos(cm, armComp.playerID, (float)gameTime.ElapsedGameTime.TotalMilliseconds);

                posComp.Position = nextPos;
            }
        }

        int GetId(ComponentManager cm, ArmComponent arm)
        {
            int freeID = 0;
            foreach (var playerEntity in cm.GetComponentsOfType<PlayerControlComponent>())
            {
                if (armID != playerEntity.Key)
                {
                    freeID = playerEntity.Key;
                }
            }
            armID = freeID;
            return freeID;
        }

        Vector2 CalculatePlayerNextPos(ComponentManager cm, int playerID, float elapsedMilliseconds)
        {
            MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(playerID);
            return moveComp.Velocity * moveComp.Speed * elapsedMilliseconds + cm.GetComponentForEntity<PositionComponent>(playerID).Position;
        }
    }
}
