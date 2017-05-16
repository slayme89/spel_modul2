﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public enum RenderLayer { Background1, Background2, Layer1, Layer2, Layer3, Layer4, Foreground1 };

    public class RenderHelper
    {
        public GraphicsDevice graphicsDevice { get; }
        public SpriteBatch spriteBatch { get; }
        private float[] layerDepths;

        public RenderHelper(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            int length = Enum.GetValues(typeof(RenderLayer)).Length;
            layerDepths = new float[length];

            for (int i = 0; i < length; i++)
            {
                layerDepths[i] = (1f / length) * i;
            }

            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;
        }

        public float GetLayerDepth(RenderLayer layer)
        {
            return layerDepths[(int)layer];
        }
    }

    class RenderSystem : ISystem, IRenderSystem
    {
        private float[] layerDepthConstants;

        void ISystem.Update(GameTime gameTime) {}

        public RenderSystem()
        {
            int length = Enum.GetValues(typeof(RenderLayer)).Length;
            layerDepthConstants = new float[length];
            
            for (int i = 0; i < length; i++)
            {
                layerDepthConstants[i] = (1f / length) * i;
            }
        }

        public void Render(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            Viewport viewport = GetCurrentViewport(renderHelper.graphicsDevice);
            Rectangle viewportBounds = viewport.Bounds;
            SpriteBatch spriteBatch = renderHelper.spriteBatch;

            RenderTiles(renderHelper);

            //Render all textures
            foreach (var entity in cm.GetComponentsOfType<TextureComponent>())
            {
                TextureComponent textureComponent = (TextureComponent)entity.Value;
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity.Key);

                if (positionComponent != null)
                {
                    Point position = positionComponent.position.ToPoint() - textureComponent.offset;
                    Rectangle textureBounds = new Rectangle(position.X, position.Y, textureComponent.texture.Width, textureComponent.texture.Height);

                    if (viewportBounds.Intersects(textureBounds))
                        spriteBatch.Draw(textureComponent.texture, position.WorldToScreen(ref viewport).ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, renderHelper.GetLayerDepth(textureComponent.layer));
                }
            }

            //Render all animations
            foreach (var entity in cm.GetComponentsOfType<AnimationComponent>())
            {
                AnimationComponent animationComponent = (AnimationComponent)entity.Value;
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity.Key);

                if (positionComponent != null)
                {
                    Point position = positionComponent.position.ToPoint() - animationComponent.offset;
                    Rectangle animationBounds = new Rectangle(position.X, position.Y, animationComponent.frameSize.X, animationComponent.frameSize.Y);

                    if (viewportBounds.Intersects(animationBounds))
                        spriteBatch.Draw(animationComponent.spriteSheet, position.WorldToScreen(ref viewport).ToVector2(), animationComponent.sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, renderHelper.GetLayerDepth(animationComponent.layer));
                }
            }

            //Render all animationgroups
            foreach (var entity in cm.GetComponentsOfType<AnimationGroupComponent>())
            {
                AnimationGroupComponent animationComponent = (AnimationGroupComponent)entity.Value;
                PositionComponent positionComponent = cm.GetComponentForEntity<PositionComponent>(entity.Key);

                if (positionComponent != null)
                {
                    Point position = positionComponent.position.ToPoint() - animationComponent.offset;
                    Rectangle animationBounds = new Rectangle(position.X, position.Y, animationComponent.frameSize.X, animationComponent.frameSize.Y);

                    if (viewportBounds.Intersects(animationBounds))
                        spriteBatch.Draw(animationComponent.spritesheet, position.WorldToScreen(ref viewport).ToVector2(), animationComponent.sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, renderHelper.GetLayerDepth(animationComponent.layer));
                }
            }
        }

        void RenderTiles(RenderHelper renderHelper)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            WorldComponent world = (from w in cm.GetComponentsOfType<WorldComponent>().Values select w).First() as WorldComponent;
            Viewport viewport = Extensions.GetCurrentViewport(renderHelper.graphicsDevice);
            Rectangle viewportBounds = viewport.Bounds;

            foreach (var tile in world.tiles)
            {
                Point point = new Point(tile.Key.X * tile.Value.Width, tile.Key.Y * tile.Value.Height);
                Rectangle tileBounds = new Rectangle(point.X, point.Y, tile.Value.Width, tile.Value.Height);

                if (viewportBounds.Intersects(tileBounds))
                    renderHelper.spriteBatch.Draw(tile.Value, point.WorldToScreen(ref viewport).ToVector2(), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, (float)RenderLayer.Background1);
            }
        }

        private Viewport GetCurrentViewport(GraphicsDevice graphicsDevice)
        {
            ComponentManager cm = ComponentManager.GetInstance();
            WorldComponent world = (from w in cm.GetComponentsOfType<WorldComponent>().Values select w).First() as WorldComponent;
            Rectangle bounds = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            bounds.Offset(world.center - new Point(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2));

            return new Viewport(bounds);
        }
    }
}

