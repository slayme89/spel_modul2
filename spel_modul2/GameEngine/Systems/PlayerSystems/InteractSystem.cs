using GameEngine.Components;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Systems
{
    class InteractSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var player in cm.GetComponentsOfType<PlayerComponent>())
            {
                PlayerComponent playerComponent = (PlayerComponent)player.Value;
                if(cm.HasEntityComponent<PlayerControlComponent>(player.Key) && cm.HasEntityComponent<PositionComponent>(player.Key))
                {
                    PlayerControlComponent controlComponent = cm.GetComponentForEntity<PlayerControlComponent>(player.Key);
                    PositionComponent playerPositionComponent = cm.GetComponentForEntity<PositionComponent>(player.Key);
                    int closestInteractable = FindClosestInteractable(playerPositionComponent.position);

                    if (closestInteractable != -1)
                    {
                        if (controlComponent.Interact.IsButtonDown())
                        {
                            if (cm.GetComponentForEntity<InteractComponent>(closestInteractable).Type == InteractType.Trap)
                                cm.GetComponentForEntity<DamageComponent>(player.Key).IncomingDamage.Add(closestInteractable);
                            // Interact with closest interactable entity
                            // Temp
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
                float distance = Vector2.Distance(position, positionComponent.position);
                if(distance < closestDist)
                {
                    closestNr = entity.Key;
                    closestDist = distance;
                }
            }
            return closestNr;
        }

        private float CalcDistance(Point destination, Point origin)
        {
            int x = destination.X - origin.X;
            int y = destination.Y - origin.Y;
            return (float)Math.Sqrt(x * x + y * y);
        }
    }
}
