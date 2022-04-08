using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldLogic : NoteLogic
{
    public int duration;

    // public float size = 1f;
    // public float yOffset = 0f;
    // public MeshFilter mf;

    // private void OnValidate()
    // {
    //     Mesh m = new Mesh();

    //     m.name = "stars plane";

    //     m.vertices = new Vector3[] {
    //         new Vector3(0, 0, 0),
    //         new Vector3(0, 0, size),
    //         new Vector3(size, 0, 0),
    //         new Vector3(size, 0, size),
    //     };

    //     m.triangles = new int[]{
    //         0, 1, 2,
    //         1, 3, 2
    //     };

    //     m.RecalculateNormals();

    //     float yo = yOffset - (int)yOffset;

    //     m.uv = new Vector2[] {
    //         new Vector2 (0, 0 - yo),
    //         new Vector2 (0, 1 - yo),
    //         new Vector2 (1, 0 - yo),
    //         new Vector2 (1, 1 - yo)
    // };

    //     mf.sharedMesh = m;
    // }
}
