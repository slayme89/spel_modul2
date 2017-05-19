using GameEngine.Managers;
using Microsoft.Xna.Framework.Audio;

namespace GameEngine.Components
{
    public class SoundThemeComponent : IComponent
    {
        public SoundEffectInstance Music { get; set; }
        public string MusicFile { get; set; }
        public bool PlayMusic { get; set; }

        public SoundThemeComponent(string filePath)
        {
            ResourceManager rm = ResourceManager.GetInstance();

            PlayMusic = true;
            MusicFile = filePath;

            Music = rm.GetResource<SoundEffect>(filePath).CreateInstance();
        }
    }
}
