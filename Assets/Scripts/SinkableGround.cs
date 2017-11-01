// Author: Itai Yavin
// Contributors:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class SinkableGround : Holdable 
{
	[Tooltip("The speed of which sinking happens")]
	[Range(0.1f, 1.0f)]
	public float sinkSpeed = 0.1f;
	[Tooltip("The amount of time before sinking")]
	public float sinkDelay = 0.0f;
	[Tooltip("The rate of which the mesh is updated. If set to 5 then each fifth frame, etc.")]
	public int updateRate = 10;

	private MeshCollider meshCollider;
	private MeshFilter meshFilter;
	private Mesh mesh;

	private int currentVertexIndex;
	
	void Start () {
		meshCollider = GetComponent<MeshCollider>();
		meshFilter = GetComponent<MeshFilter>();

		mesh = meshFilter.mesh;
//		Debug.Log(mesh.vertexCount);

	}
	
	void Update () {
		
	}

	public override void OnTouchBegin(RaycastHit hit) 
	{
		Vector3 pointHit = hit.point;
		pointHit = transform.InverseTransformPoint(pointHit);

		mesh = meshFilter.mesh;
		Vector3[] vertices = mesh.vertices;
		Vector3[] normals = mesh.normals;

		Vector3 nearestVertex = vertices[0];
		float closestDistance = Vector3.Distance(nearestVertex, pointHit);

		float distance;
		for(int i = 1; i < vertices.Length; i++)
		{
			distance = Vector3.Distance(vertices[i], pointHit);
			if(distance < closestDistance)
			{
				nearestVertex = vertices[i];
				closestDistance = distance;
				currentVertexIndex = i;
			}
		}
	}
	
	public override void OnTouchHold(RaycastHit hit) 
	{
		if(Time.frameCount % updateRate == 0)
		{
			Vector3[] vertices = mesh.vertices;
			vertices[currentVertexIndex].y -= sinkSpeed;

			mesh.vertices = vertices;
		}
	}
	
	public override void OnTouchReleased() 
	{

	}
}
