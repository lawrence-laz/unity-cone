using UnityEngine;

public class VoxelProvider : MonoBehaviour
{
    public GameObject VoxelPrefab;

    public void PutVoxel(Vector3 position)
    {
        var voxel = GetVoxel();
        voxel.position = position;
    }

    public Transform GetVoxel()
    {
        if (TryGetUnusedVoxel(out var voxel))
        {
            voxel.gameObject.SetActive(true);
            return voxel;
        }

        voxel = Instantiate(VoxelPrefab, transform).transform;

        return voxel;
    }

    public void ResetVoxels()
    {
        foreach (var child in transform)
        {
            var childTransform = (Transform)child;
            childTransform.gameObject.SetActive(false);
        }
    }

    private bool TryGetUnusedVoxel(out Transform voxel)
    {
        foreach (var child in transform)
        {
            var childTransform = (Transform)child;
            if (childTransform.gameObject.activeSelf == false)
            {
                voxel = childTransform;
                return true;
            }
        }

        voxel = null;
        return false;
    }
}
