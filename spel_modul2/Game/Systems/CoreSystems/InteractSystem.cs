using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using GameEngine;

namespace Game.Systems
{
    public class InteractSystem : ISystem
    {
        Group<PlayerComponent, PlayerControlComponent, PositionComponent> players;

        public InteractSystem()
        {
            players = new Group<PlayerComponent, PlayerControlComponent, PositionComponent>();
        }

        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var player in players)
            {
                PlayerComponent playerComponent = player.Item1;
                PlayerControlComponent controlComponent = player.Item2;
                PositionComponent playerPositionComponent = player.Item3;

                int closestInteractable = FindClosestInteractable(playerPositionComponent.Position);

                if (closestInteractable != -1)
                {
                    if (controlComponent.Interact.IsButtonDown() && !cm.GetComponentForEntity<InventoryComponent>(player.Entity).IsOpen)
                    {
                        InteractComponent interComp = cm.GetComponentForEntity<InteractComponent>(closestInteractable);

                        // Its a trap(Deal damage)
                        if (interComp.Type == InteractType.Trap && cm.HasEntityComponent<AttackComponent>(closestInteractable))
                        {
                            cm.GetComponentForEntity<HealthComponent>(player.Entity).IncomingDamage.Add(closestInteractable);
                        }

                        //Talk(show text)
                        else if (interComp.Type == InteractType.Talk && cm.HasEntityComponent<TextComponent>(closestInteractable))
                        {
                            foreach (var inter in cm.GetComponentsOfType<InteractComponent>())
                            {
                                if (cm.HasEntityComponent<TextComponent>(inter.Key))
                                    cm.GetComponentForEntity<TextComponent>(inter.Key).IsActive = false;
                            }
                            cm.GetComponentForEntity<TextComponent>(closestInteractable).IsActive = true;
                        }

                        //Loot(Give item to looter)
                        else if (interComp.Type == InteractType.Loot && cm.HasEntityComponent<ItemComponent>(closestInteractable))
                        {
                            //Remove components
                            cm.RemoveComponentFromEntity<InteractComponent>(closestInteractable);
                            cm.RemoveComponentFromEntity<CollisionComponent>(closestInteractable);
                            cm.RemoveComponentFromEntity<PositionComponent>(closestInteractable);
                            //Give the item to the player
                            cm.GetComponentForEntity<InventoryComponent>(player.Entity).ItemsToAdd.Add(closestInteractable);
                        }
                    }
                }
            }
        }


        private int FindClosestInteractable(Vector2 position)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            int closestNr = -1;
            float closestDist = 100;
            foreach (var entity in cm.GetComponentsOfType<InteractComponent>())
            {
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity.Key);
                float distance = Vector2.Distance(position, positionComponent.Position);
                if (distance < closestDist)
                {
                    closestNr = entity.Key;
                    closestDist = distance;
                }
            }
            return closestNr;
        }
    }
}