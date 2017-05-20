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
                int weaponID = invenComp.WeaponBodyHead[0];
                int bodyID = invenComp.WeaponBodyHead[1];
                int headID = invenComp.WeaponBodyHead[2];
                ItemComponent weapon = cm.GetComponentForEntity<ItemComponent>(weaponID);
                ItemComponent body = cm.GetComponentForEntity<ItemComponent>(bodyID);
                ItemComponent head = cm.GetComponentForEntity<ItemComponent>(headID);
                PositionComponent playerPos = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                if(weaponID != 0)
                {
                    if (!cm.HasEntityComponent<PositionComponent>(weaponID))
                        cm.AddComponentsToEntity(weaponID, new PositionComponent());
                    else
                    {
                        PositionComponent weaponPos = cm.GetComponentForEntity<PositionComponent>(weaponID);
                        if (weaponPos.Position != playerPos.Position)
                            weaponPos.Position = playerPos.Position;
                    }
                }
                if (bodyID != 0)
                {
                    if (!cm.HasEntityComponent<PositionComponent>(bodyID))
                        cm.AddComponentsToEntity(bodyID, new PositionComponent());
                    else
                    {
                        PositionComponent bodyPos = cm.GetComponentForEntity<PositionComponent>(bodyID);
                        if (bodyPos.Position != playerPos.Position)
                            bodyPos.Position = playerPos.Position;
                    }
                }
                if (headID != 0)
                {
                    if (!cm.HasEntityComponent<PositionComponent>(headID))
                        cm.AddComponentsToEntity(headID, new PositionComponent());
                    else
                    {
                        PositionComponent headPos = cm.GetComponentForEntity<PositionComponent>(headID);
                        if (headPos.Position != playerPos.Position)
                            headPos.Position = playerPos.Position;
                    }
                }
            }
        }
    }
}
