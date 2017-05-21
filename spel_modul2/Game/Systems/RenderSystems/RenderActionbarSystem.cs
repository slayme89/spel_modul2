using Game.Components;
using GameEngine;
using GameEngine.Components;
using GameEngine.Managers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Systems
{
    public class RenderActionbarSystem : ISystem, IRenderSystem
    {
        public void Render(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            Viewport viewport = Extensions.GetCurrentViewport(renderHelper.graphicsDevice);
            foreach (var entity in cm.GetComponentsOfType<ActionBarComponent>())
            {
                ActionBarComponent actionBarComp = (ActionBarComponent)entity.Value;

                int actionBarHeight = (actionBarComp.SlotSize.Y + 10);
                int actionBarWidth = (actionBarComp.SlotSize.Y + 5) * 4 + 5;
                int positionRight = renderHelper.graphicsDevice.Viewport.TitleSafeArea.Left;
                if (cm.HasEntityComponent<PlayerComponent>(entity.Key))
                {
                    int pNum = cm.GetComponentForEntity<PlayerComponent>(entity.Key).Number;
                    if (pNum == 2)
                    {
                        positionRight = renderHelper.graphicsDevice.Viewport.TitleSafeArea.Right - 145;
                    }
                }
                actionBarComp.PositionOnScreen = new Point(positionRight, renderHelper.graphicsDevice.Viewport.TitleSafeArea.Bottom - actionBarHeight);
                Rectangle actionBarBackground = new Rectangle(actionBarComp.PositionOnScreen, new Point(actionBarWidth, actionBarHeight));

                renderHelper.DrawFilledRectangle(actionBarBackground, Color.DarkGray, RenderLayer.GUI1);
                for (int slot = 0; slot < 4; slot++)
                {
                    Rectangle actionBarSlot = new Rectangle(new Point(actionBarComp.PositionOnScreen.X + (actionBarComp.SlotSize.X + 5) * slot + 5, actionBarComp.PositionOnScreen.Y + 5), actionBarComp.SlotSize);
                    renderHelper.DrawFilledRectangle(actionBarSlot, Color.Gray, RenderLayer.GUI2);
                    if(actionBarComp.Slots[slot] != null)
                    {
                        
                        if (actionBarComp.Slots[slot].IsItem)
                            renderHelper.Draw(((ItemComponent)actionBarComp.Slots[slot]).ItemIcon, actionBarSlot, Color.AliceBlue, RenderLayer.GUI3);
                        else
                            renderHelper.Draw(((SkillComponent)actionBarComp.Slots[slot]).SkillIcon, actionBarSlot, Color.AliceBlue, RenderLayer.GUI3);
                    }

                }
            }
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}