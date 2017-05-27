using Game.Components;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Game.Systems
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
                    int lastExperience = ExperienceCalculator(levelComponent.CurrentLevel - 1);
                    int maxExperience = ExperienceCalculator(levelComponent.CurrentLevel);

                    float scaledExperience = ((float)experience / (maxExperience - lastExperience)) * 100f;

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

        private int ExperienceCalculator(int level)
        {
            if (level <= 0) return 0;
            else if (level <= 1) return 83;
            else if (level <= 2) return 174;
            else if (level <= 3) return 266;
            else if (level <= 4) return 389;
            else if (level <= 5) return 572;
            else if (level <= 6) return 939;
            else if (level <= 7) return 1306;
            else if (level <= 8) return 1673;
            else if (level <= 9) return 2407;
            else return 10;
        }
    }
}

