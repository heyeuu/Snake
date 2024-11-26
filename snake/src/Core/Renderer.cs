using System.Numerics;
namespace Snake.Core;

public class Renderer()
{
    public static string colorBackground = Color.Black;
    public static string colorForeground = Color.White;


    public static int width = 20;
    public static int height = 20;

    public static List<(Vector2 position, Texture textures)> ObjectCollection = [];
    public static List<(Vector2 position, string textures)> UI = [];

    static string[,] _pixels = new string[width, height];
    internal static string Render()
    {
        string output = "";
        // output += Controls.ClearScreen;
        output += Controls.Home;
        output += Controls.HideCursor;
        output += colorForeground;
        _pixels[0, 0] = Texture.BorderTopLeft;
        _pixels[width - 1, 0] = Texture.BorderTopRight;
        _pixels[0, height - 1] = Texture.BorderBottomLeft;
        _pixels[width - 1, height - 1] = Texture.BorderBottomRight;
        for (int i = 1; i < width - 1; i++)
            _pixels[i, 0] = Texture.BorderHorizontal;
        for (int i = 1; i < height - 1; i++)
        {
            _pixels[0, i] = Texture.BorderLeftVertical;
            for (int j = 1; j < width - 1; j++)
                _pixels[j, i] = Texture.Null;
            _pixels[width - 1, i] = Texture.BorderRightVertical;
        }
        for (int i = 1; i < width - 1; i++)
            _pixels[i, height - 1] = Texture.BorderHorizontal;
        foreach (var obj in ObjectCollection)
        {
            int x = (int)obj.position.X;
            int y = (int)obj.position.Y;
            if (x < 1 || x >= width - 1 || y < 1 || y >= height - 1)
                continue;
            _pixels[x, y] = obj.textures.color + obj.textures.texture + colorForeground;
        }
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                output += _pixels[j, i];
            }
            output += $"{i}\n";
        }

        return output;
    }

    public static class Controls
    {
        public const string Up = "\x1b[A";
        public const string Down = "\x1b[B";
        public const string Left = "\x1b[D";
        public const string Right = "\x1b[C";
        public const string Escape = "\x1b";
        public const string Enter = "\n";
        public const string Backspace = "\b";
        public const string Delete = "\x7f";
        public const string Home = "\x1b[H";
        public const string End = "\x1b[F";
        public const string ClearScreen = "\x1b[2J";
        public const string ClearLine = "\x1b[K";
        public const string HideCursor = "\x1b[?25l";
        public const string ShowCursor = "\x1b[?25h";
    }

    public static class Color
    {
        public const string White = "\x1b[37m";
        public const string Black = "\x1b[30m";
        public const string Red = "\x1b[31m";
        public const string Green = "\x1b[32m";
        public const string Yellow = "\x1b[33m";
        public const string Blue = "\x1b[34m";
        public const string Magenta = "\x1b[35m";
        public const string Cyan = "\x1b[36m";
        public const string Gray = "\x1b[37m";
    }

    public struct Texture(string color = Color.White, string texture = Texture.Null)
    {

        public const string Block = "██";
        public const string Food = "";
        public const string Head = "●●";

        public const string Null = "  ";
        public const string BorderHorizontal = "══";
        public const string BorderLeftVertical = "║ ";
        public const string BorderRightVertical = " ║";
        public const string BorderTopLeft = "╔═";
        public const string BorderTopRight = "═╗";
        public const string BorderBottomLeft = "╚═";
        public const string BorderBottomRight = "═╝";

        internal string texture = texture;
        internal string color = color;
    }
}