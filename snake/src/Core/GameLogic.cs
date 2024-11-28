using System.Diagnostics;
using System.Numerics;

namespace Snake.Core;

class GameLogic
{
    bool _stop = true;
    internal static GameLogic? Current { get; set; } = null;
    internal static void AddGameObject(GameObject obj)
    {
        if (Current == null) throw new InvalidOperationException("GameLogic not initialized");
        Current.gameObjects.Add(obj);
    }
    internal static void RemoveGameObject(GameObject obj)
    {
        if (Current == null) throw new InvalidOperationException("GameLogic not initialized");
        Current.gameObjects.Remove(obj);
    }
    private DateTime _lastUpdate;
    private readonly double _updateTime;
    List<GameObject> gameObjects = new();
    public GameLogic(int width, int height, int updateRate = 30)
    {
        Current = this;
        Renderer.width = width;
        Renderer.height = height;
        _updateTime = 1000.0f / updateRate;
        _lastUpdate = DateTime.Now;
        Task.Run(() =>
        {
            while (true)
            {
                if (_stop)
                    continue;
                if ((DateTime.Now - _lastUpdate).TotalMilliseconds < _updateTime)
                    continue;
                Time.DeltaTime = (DateTime.Now - _lastUpdate).TotalSeconds;
                Update();
                _lastUpdate = DateTime.Now;
                InputManager.Update();
                PhysicsUpdate();
                RenderUpdate();
                FinalUpdate();
                _lastUpdate = DateTime.Now;
            }
        });
    }

    public void Run()
    {
        _stop = false;
    }

    private void Update()
    {
        for (int i = 0; i < gameObjects.Count; i++)
            gameObjects[i].Update();
    }

    private void PhysicsUpdate()//?
    {

    }
    private void RenderUpdate()
    {
        Renderer.ObjectCollection = [];
        for (int i = 0; i < gameObjects.Count; i++)
            Renderer.ObjectCollection.Add((gameObjects[i].Position, gameObjects[i].Texture));
        Console.Write(Renderer.Render());
    }
    private void FinalUpdate()
    {
        for (int i = 0; i < gameObjects.Count; i++)
            if (gameObjects[i].Remove)
                gameObjects.Remove(gameObjects[i--]);
    }
    public static Vector2 RoundToGrid(Vector2 position)
    {
        position.X = (int)(position.X + 0.5f);
        position.Y = (int)(position.Y + 0.5f);
        return position;
    }
    internal List<GameObject> FindObjectPosition(Vector2 position)
    {
        List<GameObject> objects = new();
        foreach (GameObject obj in gameObjects)
            if (RoundToGrid(obj.Position) == RoundToGrid(position))
                objects.Add(obj);
        return objects;
    }
}