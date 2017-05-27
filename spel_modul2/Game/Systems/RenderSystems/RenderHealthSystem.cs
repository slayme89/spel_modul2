using Game.Components;
using GameEngine;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Systems
{
    public class RenderHealthSystem : IRenderSystem
    {
        private Texture2D healthTexture;

        public void Render(RenderHelper rh)
        {
            GraphicsDevice gd = rh.graphicsDevice;
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<HealthComponent>())
            {
                HealthComponent healthComponent = (HealthComponent)entity.Value;

                if (healthComponent != null && healthComponent.IsAlive == true)
                {
                    int currHealth = healthComponent.Current;
                    Rectangle healthRectangle = new Rectangle();
                    Viewport viewport = Extensions.GetCurrentViewport(gd);

                    if (cm.HasEntityComponent<PlayerComponent>(entity.Key))
                    {
                        int playerNumber = cm.GetComponentForEntity<PlayerComponent>(entity.Key).Number;
                        float scaledHealth = (float)currHealth / healthComponent.Max * 100f;
                        //check if its player 1 entity
                        if (playerNumber == 1)
                        {
                            healthRectangle = new Rectangle(
                                gd.Viewport.TitleSafeArea.Left + 5,
                                gd.Viewport.TitleSafeArea.Top + 8,
                                (int)scaledHealth,
                                12
                                );
                        }
                        //check if its player 2 entity
                        else if (playerNumber == 2)
                        {
                            healthRectangle = new Rectangle(
                                gd.Viewport.TitleSafeArea.Right - 105 + 100 - (int)scaledHealth,
                                gd.Viewport.TitleSafeArea.Top + 8,
                                (int)scaledHealth,
                                12
                                );
                        }
                    }
                    //else its an AI
                    else if (cm.HasEntityComponent<AIComponent>(entity.Key))
                    {
                        CollisionComponent aiCollisionBox = cm.GetComponentForEntity<CollisionComponent>(entity.Key);
                        PositionComponent p;
                        CollisionComponent c;
                        if (cm.TryGetEntityComponents(entity.Key, out p, out c))
                        {
                            /*healthRectangle = new Rectangle(
                                aiCollisionBox.collisionBox.Location.X,
                                aiCollisionBox.collisionBox.Location.Y - (aiCollisionBox.collisionBox.Height / 2),
                                currHealth / 2,
                                10).WorldToScreen(ref viewport);*/

                            healthRectangle = new Rectangle((int)p.Position.X, (int)p.Position.Y, currHealth / 2, 10).WorldToScreen(ref viewport);
                            healthRectangle.Offset(-c.CollisionBox.Width / 2, -c.CollisionBox.Height / 2 - 10);
                        }
                    }
                    rh.Draw(healthTexture, healthRectangle, Color.White, RenderLayer.GUI1);
                }
            }
        }

        public void Load(ContentManager content)
        {
            healthTexture = content.Load<Texture2D>("UI/Health");
        }
    }
}
