using GameEngine.Systems;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using GameEngine.Components;
using Game.Components;

namespace Game.Systems.CoreSystems
{
    public class GUISystem : ISystem
    {
        

        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            bool P1Alive = false;
            bool P2Alive = false;
            bool AllDead = false;
            var allPlayers = cm.GetComponentsOfType<PlayerComponent>();
            if(allPlayers.Count <= 0)
            {
                P1Alive = false;
                P2Alive = false;
                AllDead = true;
            }
            else
            {
                foreach(var player in allPlayers)
                {
                    PlayerComponent playerComp = (PlayerComponent)player.Value;
                    LevelComponent levelComp = cm.GetComponentForEntity<LevelComponent>(player.Key);

                        if (playerComp.Number == 1)
                        {
                            if (levelComp.CurrentLevel >= 1)
                                P1Alive = true;
                            else
                                P2Alive = false;
                        }
                        else if (playerComp.Number == 2)
                        {
                            if (levelComp.CurrentLevel >= 1)
                                P2Alive = true;
                            else
                                P2Alive = false;
                        }
                }
            }
            
            foreach(var gui in cm.GetComponentsOfType<GUIComponent>())
            {
                GUIComponent guiComp = (GUIComponent)gui.Value;
                if (!AllDead)
                {
                    switch (guiComp.Type)
                    {
                        case GUIType.Player1:
                            if (P1Alive)
                                guiComp.IsActive = true;
                            else
                                guiComp.IsActive = false;
                            break;
                       case GUIType.Player2:
                            if (P2Alive)
                                guiComp.IsActive = true;
                            else
                                guiComp.IsActive = false;
                            break;
                    }
                }
                else
                {
                    guiComp.IsActive = false;
                } 
            }
        }
    }
}
