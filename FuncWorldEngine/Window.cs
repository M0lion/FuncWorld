using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace FuncWorldEngine
{
    class Window : GameWindow
    {
        Game game;

        public Window(Game game) : base(800, 600, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 8))
        {
            this.game = game;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.Multisample);
            
            game.Load(this);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            game.Update(e.Time);
            if (!game.running)
            {
                Console.ReadLine();
                this.Close();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            game.Draw();
            SwapBuffers();
        }
    }
}