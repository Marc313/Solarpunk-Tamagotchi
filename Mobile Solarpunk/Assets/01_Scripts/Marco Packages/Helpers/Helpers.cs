using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    #region Float Helpers
    // Float Helpers \\

    /// <summary>
    /// Maps a float 'value' from its original range in between 'min1' and 'max1' to a new range between 'min2' and 'max2'
    /// </summary>
    public static float Map(float min1, float max1, float min2, float max2, float value)
    {
        float normalizedValue = Mathf.InverseLerp(min1, max1, value);
        float newValue = Mathf.Lerp(min2, max2, normalizedValue);
        return newValue;
    }
    #endregion

    /// <summary>
    /// Rounds a Vector3 to a Vector3Int
    /// </summary>
    /// <param presetNameField="vector"> The original Vector </param>
    /// <returns></returns>
    #region Vector3 Helpers
    public static Vector3Int ToVector3Int(this Vector3 vector)
    {
        return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }
    #endregion

    #region Transform Helpers
    /// <summary>
    /// Sets the x value of _transform.position to _x
    /// </summary>
    /// <param presetNameField="_transform"> Original transform </param>
    /// <param presetNameField="_x"> New x value </param>
    public static void SetXPosition(this Transform _transform, float _x)
    {
        Vector3 newPos = _transform.position;
        newPos.x = _x;
        _transform.position = newPos;
    }

    /// <summary>
    /// Sets the y value of _transform.position to _y
    /// </summary>
    /// <param presetNameField="_transform"> Original transform </param>
    /// <param presetNameField="_y"> New y value </param>
    public static void SetYPosition(this Transform _transform, float _y)
    {
        Vector3 newPos = _transform.position;
        newPos.x = _y;
        _transform.position = newPos;
    }

    /// <summary>
    /// Sets the z value of _transform.position to _z
    /// </summary>
    /// <param presetNameField="_transform"> Original transform </param>
    /// <param presetNameField="_z"> New z value </param>
    public static void SetZPosition(this Transform _transform, float _z)
    {
        Vector3 newPos = _transform.position;
        newPos.x = _z;
        _transform.position = newPos;
    }
    #endregion

    #region GameObject Helpers
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_object"></param>
    /// <param name="_angle"></param>
    /// <returns>Returns the rotation of the object</returns>
    public static Quaternion RotateYToRight(this GameObject _object, float _angle)
    {
        Vector3 euler = _object.transform.rotation.eulerAngles;
        euler.y += _angle;
        _object.transform.rotation = Quaternion.Euler(euler);
        return _object.transform.rotation;
    }
    #endregion

    public static Mesh[] SplitSubmeshes(this Mesh _mesh, Material[] _materials, MeshRenderer _meshRenderer)
    {
        List<Vector3>[] submeshVertices = new List<Vector3>[_materials.Length];
        List<int>[] submeshIndices = new List<int>[_materials.Length];
        for (int i = 0; i < _materials.Length; i++)
        {
            submeshVertices[i] = new List<Vector3>();
            submeshIndices[i] = new List<int>();
        }

        // Iterate over the _mesh triangles and assign each triangle to the correct submesh based on its material
        int[] indices = _mesh.GetIndices(0);
        for (int i = 0; i < indices.Length; i += 3)
        {
            int submeshIndex = _mesh.subMeshCount - 1;
            for (int j = 0; j < _materials.Length; j++)
            {
                int materialIndex = _mesh.GetSubMesh(j).indexStart;
                int materialIndexEnd = materialIndex + _mesh.GetSubMesh(j).indexCount;

                if (i >= materialIndex && i < materialIndexEnd)
                {
                    submeshIndex = j;
                    break;
                }
            }

            submeshIndices[submeshIndex].Add(indices[i]);
            submeshIndices[submeshIndex].Add(indices[i + 1]);
            submeshIndices[submeshIndex].Add(indices[i + 2]);

            submeshVertices[submeshIndex].Add(_mesh.vertices[indices[i]]);
            submeshVertices[submeshIndex].Add(_mesh.vertices[indices[i + 1]]);
            submeshVertices[submeshIndex].Add(_mesh.vertices[indices[i + 2]]);
        }

        // Create new meshes for each submesh
        Mesh[] submeshes = new Mesh[_materials.Length];
        for (int i = 0; i < _materials.Length; i++)
        {
            submeshes[i] = new Mesh();
            submeshes[i].vertices = submeshVertices[i].ToArray();
            submeshes[i].triangles = submeshIndices[i].ToArray();
            submeshes[i].RecalculateNormals();
            submeshes[i].RecalculateBounds();
        }

        return submeshes;
    }
}
