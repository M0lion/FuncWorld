using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FuncWorldEngine
{
    struct Vertex
    {
        public Vector3 position;
        public Vector3 normal;
        public int uvX, uvY;
        public static readonly int Stride = Marshal.SizeOf(default(Vertex));
    }
}
