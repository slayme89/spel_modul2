using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine
{
    class RenderEnergySystem : ISystem
    {
        Texture2D energyTexture;
        public void Update(GameTime gameTime)
        {
        }

        public void Render(GraphicsDevice gd, SpriteBatch sb)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<HealthComponent>())
            {
                DrawEnergy(entity.Key, gd, sb);
            }
        }

        private void DrawEnergy(int entity, GraphicsDevice gd, SpriteBatch sb)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            HealthComponent healthComponent = cm.GetComponentForEntity<HealthComponent>(entity);
            EnergyComponent energyComponent = cm.GetComponentForEntity<EnergyComponent>(entity);

            if (energyComponent != null && healthComponent != null && healthComponent.IsAlive == true)
            {
                int currEnergy = energyComponent.Current;
                bool player = cm.HasEntityComponent<PlayerComponent>(entity);
                bool ai = cm.HasEntityComponent<AIComponent>(entity);
                Rectangle energyRectangle = new Rectangle();

                if (player)
                {
                    int playNum = cm.GetComponentForEntity<PlayerComponent>(entity).Number;

                    //check if its player 1 entity
                    if (playNum == 1)
                    {
                        energyRectangle = new Rectangle(gd.Viewport.TitleSafeArea.Left + 5, gd.Viewport.TitleSafeArea.Top + 22 + 3, currEnergy, 14);
                    }
                    //check if its player 2 entity
                    else if (playNum == 2)
                    {
                        energyRectangle = new Rectangle(gd.Viewport.TitleSafeArea.Left + 305, gd.Viewport.TitleSafeArea.Top +22 + 3, currEnergy, 14);
                    }
                }
                //else its an AI
                else if (ai)
                {
                    CollisionComponent aiCollisionBox = cm.GetComponentForEntity<CollisionComponent>(entity);
                    energyRectangle = new Rectangle(
                        aiCollisionBox.collisionBox.Location.X,
                        aiCollisionBox.collisionBox.Location.Y - (aiCollisionBox.collisionBox.Height / 2),
                        currEnergy / 2,
                        10);
                }
                sb.Begin();
                sb.Draw(energyTexture, energyRectangle, Color.White);
                sb.End();
            }
        }

        public void Load(ContentManager content)
        {
            energyTexture = content.Load<Texture2D>("UI/Energy");
        }
    }
}
