using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class SoundThemeComponent : IComponent
    {
        public SoundEffectInstance Music { get; set; }
        public string MusicFile { get; set; }
        public bool PlayMusic { get; set; }

        public SoundThemeComponent(string filePath)
        {
            PlayMusic = true;
            MusicFile = filePath;
        }
    }
}
