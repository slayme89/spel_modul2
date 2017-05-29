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

namespace Game.Systems.CoreSystems
{
    class MusicSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            foreach (var entity in cm.GetComponentsOfType<WorldComponent>())
            {
                if(cm.HasEntityComponent<SoundComponent>(entity.Key))
                {
                    SoundComponent soundComp = cm.GetComponentForEntity<SoundComponent>(entity.Key);
                    foreach(MappedValue sound in soundComp.Sounds.Values)
                    {
                        if (sound.Action == SoundAction.None && sound.Sound.State != SoundState.Playing)
                        {
                            sound.Sound.Play();
                        }
                    }
                }
            }
        }
    }
}
