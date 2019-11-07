using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace GK2
{
    public enum FillingType { Standard, Interpolated, Hybrid}
    public partial class Grid
    {
        public bool EditingNet;
        public FillingType fillingType;
        public DirectBitmap NormalMap;
        public bool UsingNormalMap;
        public Vector3D LightVersor;
        public Color lightColor;
        public double Kd;
        public double Ks;
        public double M;
        public bool ColorFill;
        public Color color;
        public DirectBitmap background;
        public (int x, int y) MouseOnVertexIndex { get; set; }
        public Vertex MouseOnVertex { get; set; }
        public readonly int ColumnSize;
        public PictureBox pictureBox;
        public DirectBitmap DB;
        public readonly int RowSize;
        public Vertex[,] vertices;
        public List<Triangle> triangles;
        public Grid(int columnSize, int rowSize, PictureBox box, Color color)
        {
            EditingNet = true;
            fillingType = FillingType.Standard;
            LightVersor = new Vector3D(0, 0, 1);
            lightColor = Color.White;
            Kd = 50;
            Ks = 50;
            M = 50;
            ColorFill = true;
            pictureBox = box;
            background = null;
            this.color = color;
            ColumnSize = columnSize < 2 ? 2 : columnSize;
            RowSize = rowSize < 2 ? 2 : rowSize;
            vertices = new Vertex[ColumnSize, rowSize];
            int xDistance = (box.Width - 1) / (rowSize - 1);
            int yDistance = (box.Height - 2) / (columnSize - 1);
            for (int i = 0; i < ColumnSize; i++)
            {
                for (int j = 0; j < RowSize; j++)
                {
                    vertices[i, j] = new Vertex
                    {
                        X = xDistance * j,
                        Y = yDistance * i,
                        Area = 3
                    };
                }
            }
            triangles = new List<Triangle>();
            for (int i = 0; i < ColumnSize; i++)
                for (int j = 0; j < RowSize; j++)
                {
                    if (i < ColumnSize - 1 && j < RowSize - 1)
                    {
                        triangles.Add(new Triangle(vertices[i, j], vertices[i + 1, j], vertices[i, j + 1]));
                    }
                    if (i < ColumnSize - 1 && j > 0)
                    {
                        triangles.Add(new Triangle(vertices[i, j], vertices[i + 1, j], vertices[i + 1, j - 1]));
                    }
                }
            this.UpdateAllGrid();
        }
    }
}
