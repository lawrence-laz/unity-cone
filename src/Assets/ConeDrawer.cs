using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ConeDrawer : MonoBehaviour
{
    public Vector3 BaseCenter;
    public int BaseRadius;
    public int Height;
    public InputField NumberOfVoxels;

    private const int _topRadius = 0;
    private BresenhamDrawer _drawer;
    private VoxelProvider _voxels;

    public void SetBaseRadius(string radius)
    {
        if (int.TryParse(radius, out var radiusInt))
        {
            BaseRadius = radiusInt;
        }
    }

    public void SetHeight(string height)
    {
        if (int.TryParse(height, out var heightInt))
        {
            Height = heightInt;
        }
    }

    public void SetBaseCenterX(string x)
    {
        if (int.TryParse(x, out var xInt))
        {
            BaseCenter.x = xInt;
        }
    }

    public void SetBaseCenterY(string y)
    {
        if (int.TryParse(y, out var yInt))
        {
            BaseCenter.y = yInt;
        }
    }

    public void SetBaseCenterZ(string z)
    {
        if (int.TryParse(z, out var zInt))
        {
            BaseCenter.z = zInt;
        }
    }

    [ContextMenu("Draw")]
    public void Draw()
    {
        _voxels.ResetVoxels();
        Draw(BaseCenter, BaseRadius, Height);
    }

    public void Draw(Vector3 baseCenter, int baseRadius, int height)
    {
        var usingCircles = false;
        var points = new List<Vector3>();

        if (usingCircles)
        {
            for (int verticalOffset = 0; verticalOffset < height; verticalOffset++)
            {
                var currentCenter = baseCenter + Vector3.up * verticalOffset;

                var progressToTop = ((float)verticalOffset) / height;
                var currentRadius = Mathf.Lerp(baseRadius, _topRadius, progressToTop);

                points.AddRange(_drawer.DrawCircle(currentCenter, (int)currentRadius, fill: false));
            }
        }
        else
        {
            var baseCircle = _drawer.DrawCircle(BaseCenter, baseRadius, fill: false);
            var topPoint = baseCenter + Vector3.up * Height;
            foreach (var point in baseCircle)
            {
                points.AddRange(_drawer.DrawLine(point, topPoint));
            }

            points.AddRange(_drawer.DrawCircle(baseCenter, baseRadius, fill: true));
        }

        points = points.Distinct().ToList();
        NumberOfVoxels.text = points.Count.ToString();

        foreach (var point in points)
        {
            _voxels.PutVoxel(point);
        }
    }

    private void Start()
    {
        _drawer = GetComponent<BresenhamDrawer>();
        _voxels = GetComponent<VoxelProvider>();
    }
}
