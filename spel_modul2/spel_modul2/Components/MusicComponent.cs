using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class MusicComponent : IComponent
    {
        public Song backgroundMusic { get; set; }
        public string musicfileName { get; set; }


        public MusicComponent(string musicfileName)
        {
            this.musicfileName = musicfileName;
        }
    }
}
