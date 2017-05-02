using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine
{
    class RenderHealthSystem : ISystem
    {
        Texture2D healthTexture;
        public void Update(GameTime gameTime)
        {
        }

        public void Render(GraphicsDevice gd, SpriteBatch sb)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<HealthComponent>())
            {
                DrawHealth(entity.Key, gd, sb);
            }
        }

        void DrawHealth(int entity, GraphicsDevice gd, SpriteBatch sb)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            HealthComponent healthComponent = cm.GetComponentForEntity<HealthComponent>(entity);

            if (healthComponent != null && healthComponent.IsAlive == true)
            {
                int maxHealth = healthComponent.Max;
                int currHealth = healthComponent.Current;
                bool player = cm.HasEntityComponent<PlayerComponent>(entity);
                bool ai = cm.HasEntityComponent<AIComponent>(entity);
                Rectangle healthRectangle = new Rectangle();

                if (player)
                {
                    int playNum = cm.GetComponentForEntity<PlayerComponent>(entity).Number;

                    //check if its player 1 entity
                    if (playNum == 1)
                    {
                        healthRectangle = new Rectangle(2, 2, currHealth, 20);
                    }
                    //check if its player 2 entity
                    else if (playNum == 2)
                    {
                        healthRectangle = new Rectangle(300, 2, currHealth, 20);
                    }
                }
                //else its an AI
                else if (ai)
                {
                    CollisionComponent aiCollisionBox = cm.GetComponentForEntity<CollisionComponent>(entity);
                    healthRectangle = new Rectangle(
                        aiCollisionBox.collisionBox.Location.X,
                        aiCollisionBox.collisionBox.Location.Y - (aiCollisionBox.collisionBox.Height / 2),
                        currHealth/2,
                        10);
                }
                sb.Begin();
                sb.Draw(healthTexture, healthRectangle, Color.White);
                sb.End();
            }
        }

        public void Load(ContentManager content)
        {
            healthTexture = content.Load<Texture2D>("UI/Health");
        }
    }
}
