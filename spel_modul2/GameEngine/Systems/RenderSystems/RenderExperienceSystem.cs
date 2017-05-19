using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Systems
{
    public class RenderExperienceSystem : ISystem, IRenderSystem
    {
        private Texture2D ExperienceTexture;
        public void Update(GameTime gameTime)
        {
        }

        public void Render(RenderHelper rh)
        {
            GraphicsDevice gd = rh.graphicsDevice;
            ComponentManager cm = ComponentManager.GetInstance();

            foreach (var entity in cm.GetComponentsOfType<PlayerComponent>())
            {
                HealthComponent healthComponent = cm.GetComponentForEntity<HealthComponent>(entity.Key);
                LevelComponent levelComponent = cm.GetComponentForEntity<LevelComponent>(entity.Key);

                if (levelComponent != null && healthComponent != null && healthComponent.IsAlive == true)
                {
                    Rectangle experienceRectangle = new Rectangle();
                    int playerNumber = cm.GetComponentForEntity<PlayerComponent>(entity.Key).Number;
                    int experience = levelComponent.Experience;
                    int maxExperience = 0;

                    switch (levelComponent.CurrentLevel)
                    {
                        case -1: maxExperience = 0;    break;
                        case 1:  maxExperience = 83;   break;
                        case 2:  maxExperience = 174;  break;
                        case 3:  maxExperience = 266;  break;
                        case 4:  maxExperience = 389;  break;
                        case 5:  maxExperience = 572;  break;
                        case 6:  maxExperience = 939;  break;
                        case 7:  maxExperience = 1306; break;
                        case 8:  maxExperience = 1673; break;
                        case 9:  maxExperience = 2040; break;
                        case 10: maxExperience = 2407; break;
                    }
                    float scaledExperience = (float)experience / maxExperience * 100f;

                    //check if its player 1 entity
                    if (playerNumber == 1)
                    {
                        experienceRectangle = new Rectangle(
                            gd.Viewport.TitleSafeArea.Left + 5,
                            gd.Viewport.TitleSafeArea.Top + 48,
                            (int)scaledExperience,
                            5
                            );
                    }
                    //check if its player 2 entity - FIXXXXAAA
                    else if (playerNumber == 2)
                    {
                        experienceRectangle = new Rectangle(
                            gd.Viewport.TitleSafeArea.Right - 105 + 100 - (int)scaledExperience,
                            gd.Viewport.TitleSafeArea.Top + 48,
                            (int)scaledExperience,
                            5
                            );
                    }
                    rh.Draw(ExperienceTexture, experienceRectangle, Color.White, RenderLayer.Foreground1);
                }
            }
        }

        public void Load(ContentManager content)
        {
            ExperienceTexture = content.Load<Texture2D>("UI/Experience");
        }
    }
}

