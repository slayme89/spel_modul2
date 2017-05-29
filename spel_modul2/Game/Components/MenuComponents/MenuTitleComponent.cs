using GameEngine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    class MenuTitleComponent : IComponent
    {
        public string TexturePath { get; set; }
        public Texture2D Texture { get; set; }
        public RenderLayer Layer { get; set; }
        public Vector2 Position { get; set; }

        public MenuTitleComponent(string filePath, RenderLayer renderLayer, Vector2 position)
        {
            TexturePath = filePath;
            Layer = renderLayer;
            Position = position;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
