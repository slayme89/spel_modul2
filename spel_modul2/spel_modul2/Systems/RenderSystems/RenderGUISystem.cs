using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    class RenderGUISystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
        }

        public void Render(GraphicsDevice gd, SpriteBatch sb)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<GUIComponent>())
            {
                //Its a player, draw PlayerHealthContainer
                if (cm.HasEntityComponent<PlayerComponent>(entity.Key) && cm.HasEntityComponent<HealthComponent>(entity.Key))
                {
                    DrawPlayerHealthContainer(entity.Key, gd, sb);
                }
                //Its a player, draw PlayerEnergyContainer
                if (cm.HasEntityComponent<PlayerComponent>(entity.Key) && cm.HasEntityComponent<EnergyComponent>(entity.Key))
                {
                    DrawPlayerEnergyContainer(entity.Key, gd, sb);
                }
            }
        }

        private void DrawPlayerHealthContainer(int entity, GraphicsDevice gd, SpriteBatch sb)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            GUIComponent guiComponent = cm.GetComponentForEntity<GUIComponent>(entity);
            PlayerComponent playerComponent = cm.GetComponentForEntity<PlayerComponent>(entity);

            Rectangle titleSafeArea = gd.Viewport.TitleSafeArea;
            Rectangle containerRect = new Rectangle(titleSafeArea.Left, titleSafeArea.Top, guiComponent.Texture.Width, guiComponent.Texture.Height);

            if (playerComponent.Number == 2)
                containerRect = new Rectangle(titleSafeArea.Left + 300, titleSafeArea.Top, guiComponent.Texture.Width, guiComponent.Texture.Height);
            Texture2D texture = guiComponent.Texture;

            sb.Begin();
            sb.Draw(texture, containerRect, Color.White);
            sb.End();
        }

        private void DrawPlayerEnergyContainer(int entity, GraphicsDevice gd, SpriteBatch sb)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            GUIComponent guiComponent = cm.GetComponentForEntity<GUIComponent>(entity);
            PlayerComponent playerComponent = cm.GetComponentForEntity<PlayerComponent>(entity);

            Rectangle titleSafeArea = gd.Viewport.TitleSafeArea;
            Rectangle containerRect = new Rectangle(titleSafeArea.Left, titleSafeArea.Top + 22, guiComponent.Texture.Width, guiComponent.Texture.Height);

            if (playerComponent.Number == 2)
                containerRect = new Rectangle(titleSafeArea.Left + 300, titleSafeArea.Top + 22, guiComponent.Texture.Width, guiComponent.Texture.Height);
            Texture2D texture = guiComponent.Texture;

            sb.Begin();
            sb.Draw(texture, containerRect, Color.White);
            sb.End();
        }

        public void Load(ContentManager content)
       {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<GUIComponent>())
            {
                GUIComponent guiComp = (GUIComponent)entity.Value;
                guiComp.Texture = content.Load<Texture2D>(guiComp.TextureName);
            }

       }
    }
}
