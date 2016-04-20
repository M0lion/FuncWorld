using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace FuncWorldEngine
{
    class Game
    {
        protected GameWindow window;
        
        public bool running = true;

        protected Camera camera;

        public Game()
        {
        }

        public virtual void Update(double time)
        {
            InputManager.Update(window);
        }

        public virtual void Draw()
        {
            BeginDraw();

            EndDraw();
        }

        public virtual void Load(Window window)
        {
            this.window = window;
            camera = new Camera(90, window.ClientRectangle);

            camera.setPosition(0,0,-1);
            camera.setDirection(0,0,1);
            
            CheckError();
        }

        public static void CheckError()
        {
            ErrorCode ec = GL.GetError();
            if (ec != 0)
            {
                Console.WriteLine(ec.ToString());
            }
        }

        protected void BeginDraw()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        protected void EndDraw()
        {}
    }
}
