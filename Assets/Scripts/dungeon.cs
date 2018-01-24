using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeon : MonoBehaviour {
    Mesh hexMesh;
    static List<Vector3> vertices = new List<Vector3>();
    static List<int> triangles = new List<int>();
    // Use this for initialization
    void Start () {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        vertices.Add(new Vector3(10, 0, 10));
        vertices.Add(new Vector3(-10, 0, -10));
        vertices.Add(new Vector3(-10, 0, 10));
        vertices.Add(new Vector3(10, 0, -10));
        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(1);
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(3);
        hexMesh.vertices = vertices.ToArray();
        hexMesh.triangles = triangles.ToArray();
      
        hexMesh.RecalculateNormals();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
