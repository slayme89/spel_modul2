﻿using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Systems
{
    public class InteractSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var player in cm.GetComponentsOfType<PlayerComponent>())
            {
                if (cm.HasEntityComponent<PlayerControlComponent>(player.Key) && cm.HasEntityComponent<PositionComponent>(player.Key))
                {
                    PlayerControlComponent controlComponent = cm.GetComponentForEntity<PlayerControlComponent>(player.Key);
                    PositionComponent playerPositionComponent = cm.GetComponentForEntity<PositionComponent>(player.Key);
                    int closestInteractable = FindClosestInteractable(playerPositionComponent.Position);

                    if (closestInteractable != -1)
                    {
                        if (controlComponent.Interact.IsButtonDown())
                        {
                            InteractComponent interComp = cm.GetComponentForEntity<InteractComponent>(closestInteractable);

                            // Its a trap
                            if (interComp.Type == InteractType.Trap && cm.HasEntityComponent<AttackComponent>(closestInteractable))
                            {
                                cm.GetComponentForEntity<DamageComponent>(player.Key).IncomingDamage.Add(closestInteractable);
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

        private float CalcDistance(Point destination, Point origin)
        {
            int x = destination.X - origin.X;
            int y = destination.Y - origin.Y;
            return (float)Math.Sqrt(x * x + y * y);
        }
    }
}