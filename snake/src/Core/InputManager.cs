using System.Collections.Concurrent;
using System.Diagnostics;

namespace Snake.Core;


[Flags]
enum InputType : long//为什么long
{
    None = 0x0,
    W = 0x1,
    A = 0x2,
    S = 0x4,
    D = 0x8,
    Space = 0x10,
    Escape = 0x20,
    Enter = 0x40,
    Up = 0x80,
    Down = 0x100,
    Left = 0x200,
    Right = 0x400
}

class InputManager
{
    InputType _input = InputType.None;
    InputType _lastInput = InputType.None;

    static InputManager? _instance;
    static public bool GetKeyDown(InputType input)
    {
        _instance ??= new InputManager();//单列
        return (_instance._input & input) == input && (_instance._lastInput & input) == 0;
    }
    static public bool GetKeyUp(InputType input)
    {
        _instance ??= new InputManager();
        return (_instance._input & input) == 0 && (_instance._lastInput & input) == input;//刚松开按键？
    }
    static public bool GetKey(InputType input)
    {
        _instance ??= new InputManager();
        return (_instance._input & input) == input;
    }

    static internal void Update()
    {
        _instance ??= new InputManager();
        _instance._lastInput = _instance._input;
        _instance._input = InputType.None;
    }


    InputManager()
    {
        Task.Run(() =>
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {

                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.W:
                            _input |= InputType.W;
                            break;
                        case ConsoleKey.A:
                            _input |= InputType.A;
                            break;
                        case ConsoleKey.S:
                            _input |= InputType.S;
                            break;
                        case ConsoleKey.D:
                            _input |= InputType.D;
                            break;
                        case ConsoleKey.Spacebar:
                            _input |= InputType.Space;
                            break;
                        case ConsoleKey.Escape:
                            _input |= InputType.Escape;
                            break;
                        case ConsoleKey.Enter:
                            _input |= InputType.Enter;
                            break;
                        case ConsoleKey.UpArrow:
                            _input |= InputType.Up;
                            break;
                        case ConsoleKey.DownArrow:
                            _input |= InputType.Down;
                            break;
                        case ConsoleKey.LeftArrow:
                            _input |= InputType.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            _input |= InputType.Right;
                            break;
                        default:
                            break;
                    }
                }
            }
        });
    }
}

