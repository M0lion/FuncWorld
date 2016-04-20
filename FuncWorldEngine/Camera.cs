using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Drawing;

namespace FuncWorldEngine
{
    class Camera
    {
        Matrix4 view;
        Matrix4 projection;
        
        Vector3 position;
        Vector3 direction;

        Vector3 up = Vector3.UnitY;

        float FOV;

        bool needsUpdate = true;

        public Rectangle viewport;

        //FOV: field of view in degrees
        //aspectRatio: W/H
        public Camera(float FOV, Rectangle viewport)
        {
            this.FOV = MathHelper.DegreesToRadians(FOV);
            setProjection(FOV, viewport.Width / (float)viewport.Height);
        }

        //FOV: field of view in degrees
        //aspectRatio: W/H
        public void setProjection(float FOV, float aspectRatio)
        {
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), aspectRatio, 0.01f, 100);
        }
        
        public void setDirection(float x, float y, float z)
        {
            direction = new Vector3(x,y,z);
            needsUpdate = true;
        }

        public void setDirection(Vector3 direction)
        {
            this.direction = direction;
            needsUpdate = true;
        }

        public void setPosition(float x, float y, float z)
        {
            position = new Vector3(x, y, z);
            needsUpdate = true;
        }

        public void setPosition(Vector3 position)
        {
            this.position = position;
            needsUpdate = true;
        }

        void updateView()
        {
            view = Matrix4.LookAt(position, position + direction, up);
            needsUpdate = false;
        }

        public void sendData(Shader shader)
        {
            if(needsUpdate)
                updateView();

            shader.SetVariable("view", view);
            shader.SetVariable("projection", projection);
        }

        public void rotateLocalY(float rads)
        {
            Matrix4 rot = Matrix4.CreateRotationY(rads);
            direction = Vector3.Transform(direction, rot);
            needsUpdate = true;
        }

        public void rotateLocalX(float rads)
        {
            Matrix4 rot = Matrix4.CreateRotationX(rads);
            direction = Vector3.Transform(direction, rot);
            needsUpdate = true;
        }

        public void MoveLocal(Vector3 move)
        {
            float y = move.Y;
            move.Y = 0;

            Vector3 direction = this.direction;
            direction.Y = 0;
            position += Vector3.Transform(move, Matrix4.LookAt(Vector3.Zero, direction, up).Inverted());
            position.Y += y;
            needsUpdate = true;
        }

        public float getFOV()
        {
            return FOV;
        }
    }
}
