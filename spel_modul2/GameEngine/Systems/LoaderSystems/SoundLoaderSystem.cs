using GameEngine.Components;
using GameEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GameEngine.Systems
{
    class SoundLoaderSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
        }

        public void Load(ContentManager content)
        {
            var sounds = ComponentManager.GetInstance().GetComponentsOfType<SoundComponent>();
            foreach (SoundComponent soundComponent in sounds.Values)
            {
                soundComponent.AttackSound = content.Load<SoundEffect>(soundComponent.AttackFile).CreateInstance();
                soundComponent.WalkSound = content.Load<SoundEffect>(soundComponent.WalkFile).CreateInstance();
                soundComponent.DamageSound = content.Load<SoundEffect>(soundComponent.DamageFile).CreateInstance();
                soundComponent.WalkSound.IsLooped = true;
            }

            foreach (var entity in ComponentManager.GetInstance().GetComponentsOfType<SoundThemeComponent>())
            {
                SoundThemeComponent stc = (SoundThemeComponent)entity.Value;
                stc.Music = content.Load<SoundEffect>(stc.MusicFile).CreateInstance();
                stc.Music.Volume *= 0.1f;
            }
        }
    }
}
