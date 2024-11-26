
using System.Numerics;
using Snake.Core;

namespace Snake;

public class SnakeBody : GameObject
{

    public int LifeTime { get; set; } = 0;
    public SnakeBody(Vector2 position, string color)
    {
        base.position = position;
        texture.color = color;
        texture.texture = Renderer.Texture.Block;

        Tag = "Snake";
    }
}