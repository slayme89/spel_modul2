using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using GameEngine.Components;

namespace GameEngine.Systems
{
    public class PlayerEquipmentSystem : ISystem
    {
        PositionComponent weaponPos = new PositionComponent();
        PositionComponent bodyPos = new PositionComponent();
        PositionComponent headPos = new PositionComponent();
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

                if(!cm.HasEntityComponent<PositionComponent>(weaponID))
                    cm.AddComponentsToEntity(weaponID, weaponPos);
                if (!cm.HasEntityComponent<PositionComponent>(bodyID))
                    cm.AddComponentsToEntity(bodyID, bodyPos);
                if (!cm.HasEntityComponent<PositionComponent>(headID))
                    cm.AddComponentsToEntity(headID, headPos);

                if(weaponPos.Position != playerPos.Position)
                    weaponPos.Position = playerPos.Position;

                if (bodyPos.Position != playerPos.Position)
                    bodyPos.Position = playerPos.Position;

                if (headPos.Position != playerPos.Position)
                    headPos.Position = playerPos.Position;


            }
        }
    }
}
