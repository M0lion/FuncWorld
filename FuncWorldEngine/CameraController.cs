using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Input;
using System.Drawing;
using OpenTK;

namespace FuncWorldEngine
{
    class CameraController
    {
        Camera camera;
        Rectangle viewport;
        float FOV;

        float moveSpeed = 0.2f;

        public CameraController(Camera camera, Rectangle viewport, float FOV)
        {
            this.camera = camera;
            this.viewport = viewport;
            this.FOV = FOV;
        }

        public void Update()
        {
            if (InputManager.isMouseButtonDown(MouseButton.Left))
            {
                float rotY = -InputManager.mouseDeltaY * FOV / viewport.Height;
                camera.rotateLocalX(-rotY);
                float rotX = -InputManager.mouseDeltaX * FOV / viewport.Width;
                camera.rotateLocalY(rotX);
            }

            Vector3 move = Vector3.Zero;
            if (InputManager.isKeyDown(Key.W))
            {
                move.Z -= 1;
            }
            if (InputManager.isKeyDown(Key.S))
            {
                move.Z += 1;
            }
            if (InputManager.isKeyDown(Key.A))
            {
                move.X -= 1;
            }
            if (InputManager.isKeyDown(Key.D))
            {
                move.X += 1;
            }
            if (InputManager.isKeyDown(Key.Q))
            {
                move.Y += 1;
            }
            if (InputManager.isKeyDown(Key.E))
            {
                move.Y -= 1;
            }

            if(move != Vector3.Zero)
            {
                move.Normalize();
                move *= moveSpeed;
                camera.MoveLocal(move);
            }
        }
    }
}
