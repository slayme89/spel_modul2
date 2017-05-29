using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace GameEngine.Systems
{

    
    public class SoundSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            foreach (var entity in cm.GetComponentsOfType<SoundComponent>())
            {
                SoundComponent soundComponent = (SoundComponent)entity.Value;

                foreach (MappedValue soundValue in soundComponent.Sounds.Values)
                {
                    if (soundValue.Sound.State != SoundState.Playing && soundValue.Action == SoundAction.Play)
                    {
                        if(soundValue.Sound.State == SoundState.Paused)
                            soundValue.Sound.Resume();
                        else
                            soundValue.Sound.Play();
                        soundValue.Action = SoundAction.None;
                        if (soundValue.Loop)
                        {
                            soundValue.Sound.Play();
                            soundValue.Action = SoundAction.Play;
                        }
                    }
                    if(soundValue.Sound.State == SoundState.Playing)
                    {
                        if (soundValue.Action == SoundAction.Pause)
                            soundValue.Sound.Pause();
                        else if (soundValue.Action == SoundAction.Stop)
                            soundValue.Sound.Stop();
                        soundValue.Action = SoundAction.None;
                    }
                }
                //    if (soundComponent.PlayAttackSound && soundComponent.AttackSound.State != SoundState.Playing)
                //    {
                //        soundComponent.WalkSound.Stop();
                //        soundComponent.AttackSound.Play();
                //        soundComponent.PlayAttackSound = false;
                //    }
                //    else if (soundComponent.PlayWalkSound && soundComponent.WalkSound.State != SoundState.Playing && soundComponent.AttackSound.State != SoundState.Playing)
                //    {
                //        soundComponent.WalkSound.Play();
                //    }
                //    else if (!soundComponent.PlayWalkSound)
                //    {
                //        soundComponent.WalkSound.Pause();
                //    }

                //    if (soundComponent.PlayDamageSound && soundComponent.DamageSound.State != SoundState.Playing)
                //    {
                //        soundComponent.DamageSound.Play();
                //        soundComponent.PlayDamageSound = false;
                //    }
                //}

                //foreach (var entity in cm.GetComponentsOfType<SoundThemeComponent>())
                //{
                //    SoundThemeComponent stc = (SoundThemeComponent)entity.Value;
                //    // Temp mute
                //    if (Keyboard.GetState().IsKeyDown(Keys.M))
                //        stc.PlayMusic = !stc.PlayMusic;

                //    if (stc.PlayMusic && stc.Music.State != SoundState.Playing)
                //    {
                //        stc.Music.Play();
                //    }
                //    else if(!stc.PlayMusic)
                //    {
                //        stc.Music.Stop();
                //    }
                //}
            }
        }
    }
}
