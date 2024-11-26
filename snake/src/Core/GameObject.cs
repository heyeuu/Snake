using System.Numerics;
using Snake.Core;
using static Snake.Core.Renderer;

namespace Snake.Core
{
    public class GameObject
    {
        internal bool Remove = false;
        public string Tag { get; set; } = "";
        public Vector2 Position => position;
        public Texture Texture => texture;
        protected Vector2 position;
        protected Texture texture;

        public virtual void Update() { }

        protected void DetectCollision(in Action<GameObject> action)
        {
            var list = GameLogic.Current?.FindObjectPosition(position) ?? [];
            foreach (var obj in list)
                action(obj);
        }
        protected GameObject()
        {
            texture.texture = Texture.Null;
            GameLogic.AddGameObject(this);
        }
        public void DeleteThis()//为什么需要记录这个Romove？谨防段错误？
        {
            Remove = true;
        }
        ~GameObject()
        {
            DeleteThis();
        }
    }
}