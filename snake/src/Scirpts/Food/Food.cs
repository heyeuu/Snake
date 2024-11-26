using System.Numerics;
using Snake.Core;

namespace Snake.Food
{
    public class Food : GameObject
    {
        public static void CreateNewFood()
        {
            Random random = new();
            Vector2 position = new(random.Next(1, Renderer.width - 1), random.Next(1, Renderer.height - 1));
            Food food = new(position);
        }

        float Speed = 5f;
        double Timer = 0;
        public Food(Vector2 position)
        {
            this.position = position;
            texture.texture = Renderer.Texture.Food;
            texture.color = Renderer.Color.Blue;
            Tag = "Food";
        }

        public override void Update()
        {
            Timer += Time.DeltaTime * Speed;
            if (Timer < 1)
                return;

            Timer = 0;

            if (texture.color == Renderer.Color.Red)
                texture.color = Renderer.Color.Blue;
            else
                texture.color = Renderer.Color.Red;
        }
    }
}