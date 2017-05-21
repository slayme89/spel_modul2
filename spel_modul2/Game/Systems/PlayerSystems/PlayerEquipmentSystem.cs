using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using GameEngine.Components;
using GameEngine.Systems;
using Game.Components;
using System.Diagnostics;

namespace Game.Systems
{
    public class PlayerEquipmentSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach(var entity in cm.GetComponentsOfType<InventoryComponent>())
            {
                InventoryComponent invenComp = (InventoryComponent)entity.Value;
                PositionComponent playerPosComp = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                Vector2 playerPos = CalculatePlayerNextPos(cm, entity.Key, (float)gameTime.ElapsedGameTime.TotalMilliseconds);

                for(int i = 0; i < 3; i++)
                {
                    UpdateEquipmentPosition(cm, invenComp.WeaponBodyHead[i], playerPos);
                }
            }
        }

        void UpdateEquipmentPosition(ComponentManager cm, int equipmentID, Vector2 playerPos)
        {
            if (equipmentID != 0)
            {
                if (!cm.HasEntityComponent<PositionComponent>(equipmentID))
                    cm.AddComponentsToEntity(equipmentID, new PositionComponent());
                else
                {
                    PositionComponent posComp = cm.GetComponentForEntity<PositionComponent>(equipmentID);
                    
                    if (posComp.Position != playerPos)
                        posComp.Position = playerPos;
                }
            }
        }

        Vector2 CalculatePlayerNextPos(ComponentManager cm, int playerID, float elapsedMilliseconds)
        {
            MoveComponent moveComp = cm.GetComponentForEntity<MoveComponent>(playerID);
            return moveComp.Velocity * moveComp.Speed * elapsedMilliseconds + cm.GetComponentForEntity<PositionComponent>(playerID).Position;
        }
    }
}
