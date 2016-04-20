using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace FuncWorldEngine
{
    class Mesh
    {
        List<Vertex> vertices;

        Shader shader;

        int vbo;

        Matrix4 modelMatrix;
        public Vector3 position;

        //used by draw, set by build
        int vertexCount;

        public Mesh()
        {
            string[] shaderDesc = { "basic" };
            shader = ShaderManager.getShaderHandle(shaderDesc.ToList());

            vertices = new List<Vertex>();
        }

        public void addVertex(Vertex vertex)
        {
            vertices.Add(vertex);
        }

        public void Build()
        {
            vbo = GL.GenBuffer();

            Vertex[] data = vertices.ToArray();
            vertexCount = data.Length;

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(vertexCount * Vertex.Stride), data, BufferUsageHint.StaticDraw);
        }

        public void Draw(Camera camera)
        {

            modelMatrix = Matrix4.CreateTranslation(position);
            shader.SetVariable("model", modelMatrix);
            camera.sendData(shader);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);

            shader.setAttribLocation("in_Position", 0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 0);

            shader.setAttribLocation("in_Normal", 1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 3);

            shader.setAttribLocation("in_Uv", 2);
            GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Int, false, Vertex.Stride, 6);

            Shader.Bind(shader);
            GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Count);
        }
    }
}
