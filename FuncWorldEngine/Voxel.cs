using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace FuncWorldEngine
{
    class Voxel
    {
        protected enum Face
        {
            up, down,
            north, south,
            east, west
        }

        protected int texturePosX = 0;
        protected int texturePosY = 0;
        protected int textureSize = 255;
        protected float size = 1;

        protected Voxel parent;
        protected List<Voxel> child;

        public void getMesh(List<Vertex> vertices, Vector3 centerOfVoxel)
        {
            getFaceMesh(Face.up, vertices, centerOfVoxel);
            getFaceMesh(Face.down, vertices, centerOfVoxel);
            getFaceMesh(Face.north, vertices, centerOfVoxel);
            getFaceMesh(Face.south, vertices, centerOfVoxel);
            getFaceMesh(Face.east, vertices, centerOfVoxel);
            getFaceMesh(Face.west, vertices, centerOfVoxel);
        }

        protected void getFaceMesh(Face face, List<Vertex> vertices, Vector3 centerOfVoxel)
        {
            Vertex vertex = new Vertex();

            switch (face)
            {
                case Face.up:
                    vertex.normal = Vector3.UnitY;
                    break;
                case Face.down:
                    vertex.normal = -Vector3.UnitY;
                    break;
                case Face.north:
                    vertex.normal = -Vector3.UnitZ;
                    break;
                case Face.south:
                    vertex.normal = Vector3.UnitZ;
                    break;
                case Face.east:
                    vertex.normal = Vector3.UnitX;
                    break;
                case Face.west:
                    vertex.normal = -Vector3.UnitX;
                    break;
            }
            vertex.position = centerOfVoxel + getFaceCoord(face, new Vector2(1, 1));
            vertex.uvX = texturePosX + (textureSize * 0) + textureSize;
            vertex.uvX = texturePosY + (textureSize * 0) + textureSize;
            vertices.Add(vertex);

            vertex.position = centerOfVoxel + getFaceCoord(face, new Vector2(-1, 1));
            vertex.uvX = texturePosX + (textureSize * 0) + 0;
            vertex.uvX = texturePosY + (textureSize * 0) + textureSize;
            vertices.Add(vertex);

            vertex.position = centerOfVoxel + getFaceCoord(face, new Vector2(-1, -1));
            vertex.uvX = texturePosX + (textureSize * 0) + 0;
            vertex.uvX = texturePosY + (textureSize * 0) + 0;
            vertices.Add(vertex);

            vertex.position = centerOfVoxel + getFaceCoord(face, new Vector2(1, 1));
            vertex.uvX = texturePosX + (textureSize * 0) + textureSize;
            vertex.uvX = texturePosY + (textureSize * 0) + textureSize;
            vertices.Add(vertex);

            vertex.position = centerOfVoxel + getFaceCoord(face, new Vector2(-1, -1));
            vertex.uvX = texturePosX + (textureSize * 0) + 0;
            vertex.uvX = texturePosY + (textureSize * 0) + 0;
            vertices.Add(vertex);

            vertex.position = centerOfVoxel + getFaceCoord(face, new Vector2(1, -1));
            vertex.uvX = texturePosX + (textureSize * 0) + textureSize;
            vertex.uvX = texturePosY + (textureSize * 0) + 0;
            vertices.Add(vertex);
        }
        
        protected Vector3 getFaceCoord(Face face, Vector2 coord)
        {
            switch(face)
            {
                case Face.up:
                    return new Vector3(coord.X * size, size, coord.Y * size);
                case Face.down:
                    return new Vector3(coord.X * size, -size, coord.Y * size);
                case Face.north:
                    return new Vector3(coord.X * size, coord.Y * size, -size);
                case Face.south:
                    return new Vector3(coord.X * size, coord.Y * size, size);
                case Face.east:
                    return new Vector3(size, coord.X * size, coord.Y * size);
                case Face.west:
                    return new Vector3(-size, coord.X * size, coord.Y * size);
            }

            throw (new ArgumentException("face is not a Face", "Face"));
        }
    }
}
