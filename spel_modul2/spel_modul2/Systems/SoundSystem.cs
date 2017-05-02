using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace GameEngine
{
    class SoundSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<SoundComponent>())
            {
                SoundComponent soundComponent = (SoundComponent)entity.Value;
                if (soundComponent.PlayAttackSound && soundComponent.AttackSound.State != SoundState.Playing)
                {
                    soundComponent.WalkSound.Stop();
                    soundComponent.AttackSound.Play();
                    soundComponent.PlayAttackSound = false;
                }
                else if (soundComponent.PlayWalkSound && soundComponent.WalkSound.State != SoundState.Playing && soundComponent.AttackSound.State != SoundState.Playing)
                {
                    soundComponent.WalkSound.Play();
                }
                else if (!soundComponent.PlayWalkSound)
                {
                    soundComponent.WalkSound.Pause();
                }

                if (soundComponent.PlayDamageSound && soundComponent.DamageSound.State != SoundState.Playing)
                {
                    soundComponent.DamageSound.Play();
                    soundComponent.PlayDamageSound = false;
                }
            }

            foreach (var entity in cm.GetComponentsOfType<SoundThemeComponent>())
            {
                SoundThemeComponent stc = (SoundThemeComponent)entity.Value;
                if(stc.PlayMusic && stc.Music.State != SoundState.Playing)
                {
                    stc.Music.Play();
                }
            }
        }
    }
}
