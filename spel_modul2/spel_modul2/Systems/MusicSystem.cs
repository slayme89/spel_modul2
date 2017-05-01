using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class MusicSystem : ISystem
    {
       
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();
        }

        public void Load(ContentManager content)
        {
           
            var backmusic = ComponentManager.GetInstance().GetComponentsOfType<MusicComponent>();

            foreach (MusicComponent music in backmusic.Values)
            {
                music.backgroundMusic = content.Load<Song>(music.musicfileName);
                MediaPlayer.Play(music.backgroundMusic);
            }
        }
    }
}
