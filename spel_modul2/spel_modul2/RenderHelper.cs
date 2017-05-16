using Microsoft.Xna.Framework.Graphics;
using System;

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