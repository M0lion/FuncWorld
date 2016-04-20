using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace FuncWorldEngine
{
    static class InputManager
    {
        static KeyboardState keyboard;
        static KeyboardState prevKeyboard;
        static MouseState mouse;
        static MouseState prevMouse;

        public static int mouseDeltaX, mouseDeltaY = 0;

        public static void Update(GameWindow window)
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();
            prevMouse = mouse;
            mouse = Mouse.GetState();

            mouseDeltaX = mouse.X - prevMouse.X;
            mouseDeltaY = mouse.Y - prevMouse.Y;
        }
        
        public static bool isKeyDown(Key key)
        {
            return keyboard.IsKeyDown(key);
        }

        public static bool wasKeyDown(Key key)
        {
            return prevKeyboard.IsKeyDown(key);
        }

        //keyDown event this frame?
        public static bool keyDown(Key key)
        {
            return keyboard.IsKeyDown(key) && !prevKeyboard.IsKeyDown(key);
        }

        //keyUp event this frame?
        public static bool keyUp(Key key)
        {
            return !keyboard.IsKeyDown(key) && prevKeyboard.IsKeyDown(key);
        }

        public static bool isMouseButtonDown(MouseButton mouseButton)
        {
            return mouse.IsButtonDown(mouseButton);
        }
    }
}
