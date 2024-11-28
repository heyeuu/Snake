using System.Collections.Concurrent;
using System.Numerics;
using System.Text.Json.Serialization;
using Snake;
using Snake.Core;
using Snake.Food;
enum Direction { Up, Down, Left, Right }
class SnakeHead : GameObject
{
    ConcurrentQueue<InputType> inputQueue = new();
    double Diff = 0;
    public double Length = 2;
    public float Speed = 10f;
    private Direction direction = Direction.Up;
    readonly Queue<SnakeBody> snakeBodies = [];//只读？
    public SnakeHead(Vector2 position)
    {
        this.position = position;
        snakeBodies.Enqueue(new SnakeBody(position + new Vector2(0, 1), Renderer.Color.Green));
        texture.texture = Renderer.Texture.Block;
        texture.color = Renderer.Color.Yellow;
    }
    public override void Update()
    {
        GetKeyDown();
        Diff += Time.DeltaTime * Speed;
        if (Diff < 1)
            return;
        Diff = 0;

        snakeBodies.Enqueue(new SnakeBody(position, Renderer.Color.Green));

        while (inputQueue.Count > 2)
            inputQueue.TryDequeue(out _);
        inputQueue.TryDequeue(out var result);
        if (result == InputType.W && direction != Direction.Down)
            direction = Direction.Up;
        if (result == InputType.S && direction != Direction.Up)
            direction = Direction.Down;
        if (result == InputType.A && direction != Direction.Right)
            direction = Direction.Left;
        if (result == InputType.D && direction != Direction.Left)
            direction = Direction.Right;
        bool deleteTail = true;
        Move(direction);
        DetectCollision(x =>
        {
            if (x.Tag == "Food")
            {
                x.DeleteThis();
                Food.CreateNewFood();
                deleteTail = false;
            }
            else if (x.Tag == "Snake")
                throw new Exception("Game Over");
        });
        if (deleteTail)
            snakeBodies.Dequeue().DeleteThis();
    }
    private void GetKeyDown()
    {
        if (InputManager.GetKeyDown(InputType.W))
            inputQueue.Enqueue(InputType.W);
        if (InputManager.GetKeyDown(InputType.S))
            inputQueue.Enqueue(InputType.S);
        if (InputManager.GetKeyDown(InputType.A))
            inputQueue.Enqueue(InputType.A);
        if (InputManager.GetKeyDown(InputType.D))
            inputQueue.Enqueue(InputType.D);
    }
    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                position.Y -= 1;
                break;
            case Direction.Down:
                position.Y += 1;
                break;
            case Direction.Left:
                position.X -= 1;
                break;
            case Direction.Right:
                position.X += 1;
                break;
        }
    }
}