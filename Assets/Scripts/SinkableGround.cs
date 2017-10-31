// Author: Itai Yavin
// Contributors:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkableGround : Holdable 
{
	Mesh mesh;

	void Start () {
		
	}
	
	void Update () {
		
	}

	public override void OnTouchBegin(RaycastHit hit) 
	{
		mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        int i = 0;
        while (i < vertices.Length) {
            vertices[i] += normals[i] * Mathf.Sin(i);
            i++;
        }
        mesh.vertices = vertices;
	}
	
	public override void OnTouchHold(RaycastHit hit) 
	{

	}
	
	public override void OnTouchReleased() 
	{

	}
}
