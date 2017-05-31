using GameEngine.Components;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using GameEngine.Managers;

namespace GameEngine.Systems
{
    public class WorldSystem : ISystem
    {
        public void Update(GameTime gameTime)
        {
            ComponentManager cm = ComponentManager.GetInstance();

            var players = (from i in cm.GetComponentsOfType<PlayerComponent>().Keys select i).ToList();
            var world = (from w in cm.GetComponentsOfType<WorldComponent>().Values select w).First() as WorldComponent;

            if (players.Count == 1)
            {
                var player1 = cm.GetComponentForEntity<PositionComponent>(players[0]);
                world.Center = player1.Position.ToPoint();
            }
            else if (players.Count == 2)
            {
                var player1 = cm.GetComponentForEntity<PositionComponent>(players[0]);
                var player2 = cm.GetComponentForEntity<PositionComponent>(players[1]);

                world.Center = Vector2.Lerp(player1.Position, player2.Position, 0.5f).ToPoint();
            }
        }

        public void Load(ContentManager content)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            WorldComponent world;

            var e = cm.GetComponentsOfType<WorldComponent>().GetEnumerator();
            e.MoveNext();
            world = (WorldComponent)e.Current.Value;

            XmlDocument doc = new XmlDocument();
            doc.Load("Content/map.xml");

            foreach(XmlNode node in doc.DocumentElement.ChildNodes)
            {
                int x = int.Parse(node.Attributes["x"].Value);
                int y = int.Parse(node.Attributes["y"].Value);
                string textureFile = node.Attributes["texture"].Value;
                Texture2D texture = content.Load<Texture2D>(textureFile);
                Point p = new Point(x, y);

                if (!world.Tiles.ContainsKey(p))
                    world.Tiles.Add(p, texture);
            }
        }
    }
}