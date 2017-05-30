using GameEngine.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine.Managers;
using GameEngine.Components;
using Microsoft.Xna.Framework.Audio;
using Game.Components;

namespace Game.Systems.CoreSystems
{
    class MusicSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            GameStateManager gm = GameStateManager.GetInstance();

            foreach (var entity in cm.GetComponentsOfType<SoundComponent>())
            {

                if(cm.HasEntityComponent<WorldComponent>(entity.Key))
                {
                    SoundComponent soundComp = cm.GetComponentForEntity<SoundComponent>(entity.Key);
                    foreach (MappedValue sound in soundComp.Sounds.Values)
                    {
                        if (sound.Sound.State == SoundState.Paused && gm.State == GameState.Game)
                        {
                            sound.Sound.Resume();
                        }
                        else if (sound.Action == SoundAction.None && sound.Sound.State != SoundState.Playing && gm.State == GameState.Game)
                        {
                            sound.Sound.Play();
                        }
                        else if (sound.Sound.State == SoundState.Playing && gm.State != GameState.Game)
                            sound.Sound.Pause();
                    }
                }else if (cm.HasEntityComponent<MenuTitleComponent>(entity.Key))
                {
                    SoundComponent soundComp = cm.GetComponentForEntity<SoundComponent>(entity.Key);
                    foreach (MappedValue sound in soundComp.Sounds.Values)
                    {
                        
                        if (sound.Sound.State == SoundState.Paused && gm.State == GameState.Menu)
                        {
                            sound.Sound.Resume();
                        }
                        else if (sound.Action == SoundAction.None && sound.Sound.State != SoundState.Playing && gm.State == GameState.Menu)
                        {
                            sound.Sound.Play();
                        }
                        else if (sound.Sound.State == SoundState.Playing && gm.State != GameState.Menu)
                            sound.Sound.Pause();
                    }
                }
            }
        }
    }
}
