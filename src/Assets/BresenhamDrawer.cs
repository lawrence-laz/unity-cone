using System.Collections.Generic;
using UnityEngine;

public class BresenhamDrawer : MonoBehaviour
{
    /// <summary>
    /// Bresenham’s algorithm for circle drawing.
    /// https://www.geeksforgeeks.org/bresenhams-circle-drawing-algorithm/
    /// </summary>
    public List<Vector3> DrawCircle(Vector3 center, int radius, bool fill)
    {
        var points = new List<Vector3>();
        int xc = (int)center.x;
        int yc = (int)center.z;
        int x = 0, y = radius;
        int d = 3 - 2 * radius;
        DrawCircleOctants(xc, yc, x, y, (int)center.y, points, fill);
        while (y >= x)
        {
            x++;
            if (d > 0)
            {
                y--;
                d = d + 4 * (x - y) + 10;
            }
            else
                d = d + 4 * x + 6;
            DrawCircleOctants(xc, yc, x, y, (int)center.y, points, fill);
        }

        return points;
    }

    /// <summary>
    /// Bresenham's algorithm for 3D line drawing.
    /// https://www.geeksforgeeks.org/bresenhams-algorithm-for-3-d-line-drawing/
    /// </summary>
    public List<Vector3> DrawLine(Vector3 from, Vector3 to)
    {
        var x1 = from.x;
        var y1 = from.y;
        var z1 = from.z;
        var x2 = to.x;
        var y2 = to.y;
        var z2 = to.z;

        var points = new List<Vector3>();
        points.Add(from);
        var dx = Mathf.Abs(x2 - x1);
        var dy = Mathf.Abs(y2 - y1);
        var dz = Mathf.Abs(z2 - z1);
        int xs, ys, zs;
        float p1, p2;
        if (x2 > x1) xs = 1;
        else xs = -1;
        if (y2 > y1) ys = 1;
        else ys = -1;
        if (z2 > z1) zs = 1;
        else zs = -1;

        // Driving axis is X-axis 
        if (dx >= dy && dx >= dz)
        {
            p1 = 2 * dy - dx;
            p2 = 2 * dz - dx;
            while (x1 != x2)
            {
                x1 += xs;
                if (p1 >= 0)
                {
                    y1 += ys;
                    p1 -= 2 * dx;

                }
                if (p2 >= 0)
                {
                    z1 += zs;
                    p2 -= 2 * dx;
                }
                p1 += 2 * dy;
                p2 += 2 * dz;
                points.Add(new Vector3(x1, y1, z1));
            }
        }

        // Driving axis is Y-axis
        else if (dy >= dx && dy >= dz)
        {
            p1 = 2 * dx - dy;
            p2 = 2 * dz - dy;

            while (y1 != y2)
            {
                y1 += ys;
                if (p1 >= 0)
                {
                    x1 += xs;
                    p1 -= 2 * dy;

                }
                if (p2 >= 0)
                {
                    z1 += zs;
                    p2 -= 2 * dy;

                }
                p1 += 2 * dx;
                p2 += 2 * dz;
                points.Add(new Vector3(x1, y1, z1));
            }
        }

        // Driving axis is Z-axis
        else
        {
            p1 = 2 * dy - dz;
            p2 = 2 * dx - dz;
            while (z1 != z2)
            {
                z1 += zs;
                if (p1 >= 0)
                {
                    y1 += ys;
                    p1 -= 2 * dz;

                }
                if (p2 >= 0)
                {

                    x1 += xs;
                    p2 -= 2 * dz;
                }
                p1 += 2 * dy;
                p2 += 2 * dx;
                points.Add(new Vector3(x1, y1, z1));
            }
        }

        return points;
    }

    /// <summary>
    /// Draws 8 points at once.
    /// </summary>
    void DrawCircleOctants(int xc, int yc, int x, int y, int z, List<Vector3> points, bool fill)
    {
        if (fill)
        {
            points.AddRange(DrawLine(new Vector3(xc + x, z, yc + y), new Vector3(xc - x, z, yc + y)));
            points.AddRange(DrawLine(new Vector3(xc + x, z, yc - y), new Vector3(xc - x, z, yc - y)));
            points.AddRange(DrawLine(new Vector3(xc + y, z, yc + x), new Vector3(xc - y, z, yc + x)));
            points.AddRange(DrawLine(new Vector3(xc + y, z, yc - x), new Vector3(xc - y, z, yc - x)));
        }
        else
        {
            points.Add(new Vector3(xc + x, z, yc + y));
            points.Add(new Vector3(xc - x, z, yc + y));
            points.Add(new Vector3(xc + x, z, yc - y));
            points.Add(new Vector3(xc - x, z, yc - y));
            points.Add(new Vector3(xc + y, z, yc + x));
            points.Add(new Vector3(xc - y, z, yc + x));
            points.Add(new Vector3(xc + y, z, yc - x));
            points.Add(new Vector3(xc - y, z, yc - x));
        }
    }
}
