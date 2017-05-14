using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine
{
    class RenderEnergySystem : ISystem
    {
        private Texture2D energyTexture;

        public void Update(GameTime gameTime)
        {
        }

        public void Render(GraphicsDevice gd, SpriteBatch sb)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<PlayerComponent>())
            {
                HealthComponent healthComponent = cm.GetComponentForEntity<HealthComponent>(entity.Key);
                EnergyComponent energyComponent = cm.GetComponentForEntity<EnergyComponent>(entity.Key);

                if (energyComponent != null && healthComponent != null && healthComponent.IsAlive == true)
                {
                    int currEnergy = energyComponent.Current;
                    Rectangle energyRectangle = new Rectangle();
                    int playNum = cm.GetComponentForEntity<PlayerComponent>(entity.Key).Number;
                    float scaledEnergy = (float)currEnergy / energyComponent.Max * 100f;
                    //check if its player 1 entity
                    if (playNum == 1)
                    {
                        energyRectangle = new Rectangle(
                            gd.Viewport.TitleSafeArea.Left + 5,
                            gd.Viewport.TitleSafeArea.Top + 28,
                            (int)scaledEnergy,
                            12
                            );
                    }
                    //check if its player 2 entity - FIXXXXAAA
                    else if (playNum == 2)
                    {
                        energyRectangle = new Rectangle(
                             gd.Viewport.TitleSafeArea.Right - 105 + 100 - (int)scaledEnergy,
                            gd.Viewport.TitleSafeArea.Top + 28,
                            (int)scaledEnergy,
                            12
                            );
                    }
                    sb.Draw(energyTexture, energyRectangle, Color.White);
                }
            }
        }

        public void Load(ContentManager content)
        {
            energyTexture = content.Load<Texture2D>("UI/Energy");
        }
    }
}
