using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine
{
    class RenderHealthSystem : ISystem
    {
        private Texture2D healthTexture;

        public void Update(GameTime gameTime)
        {
        }

        public void Render(GraphicsDevice gd, SpriteBatch sb)
        {
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

                        //check if its player 1 entity
                        if (playerNumber == 1)
                        {
                            float scaledHealth = (float)currHealth / healthComponent.Max * 100f;
                            healthRectangle = new Rectangle(
                                gd.Viewport.TitleSafeArea.Left + 5,
                                gd.Viewport.TitleSafeArea.Top + 8,
                                (int)scaledHealth,
                                12
                                );

                        }
                        //check if its player 2 entity - FIXXXXAA
                        else if (playerNumber == 2)
                        {
                            float scaledHealth = (float)currHealth / healthComponent.Max * 100f;
                            healthRectangle = new Rectangle(
                                gd.Viewport.TitleSafeArea.Left + 305,
                                gd.Viewport.TitleSafeArea.Top + 6,
                                (int)scaledHealth,
                                14
                                );
                        }
                    }
                    //else its an AI
                    else if (cm.HasEntityComponent<AIComponent>(entity.Key))
                    {
                        CollisionComponent aiCollisionBox = cm.GetComponentForEntity<CollisionComponent>(entity.Key);
                        healthRectangle = new Rectangle(
                            aiCollisionBox.collisionBox.Location.X,
                            aiCollisionBox.collisionBox.Location.Y - (aiCollisionBox.collisionBox.Height / 2),
                            currHealth / 2,
                            10).WorldToScreen(ref viewport);
                    }
                    sb.Begin();
                    sb.Draw(healthTexture, healthRectangle, Color.White);
                    sb.End();
                }
            }
        }

        public void Load(ContentManager content)
        {
            healthTexture = content.Load<Texture2D>("UI/Health");
        }
    }
}
