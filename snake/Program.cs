using Snake.Core;
using Snake.Food;

class Program
{
    static void Main(string[] args)
    {
        GameLogic gameLogic = new(20, 20);
        SnakeHead snakeHead = new(new(10, 10));
        Food.CreateNewFood();
        gameLogic.Run();
        while (true) ;
    }
}

