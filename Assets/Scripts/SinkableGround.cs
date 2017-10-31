// Author: Itai Yavin
// Contributors:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class SinkableGround : Holdable 
{
	[Tooltip("The minimum accepted distance to vertex")]
	public float accuracy = 1.0f;
	[Tooltip("The speed of which sinking happens")]
	[Range(0.01f, 1.0f)]
	public float sinkSpeed = 0.1f;
	[Tooltip("The amount of time before sinking")]
	public float sinkDelay = 0.0f;
	[Tooltip("The rate of which the mesh is updated. If set to 5 then each fifth frame, etc.")]
	public int updateRate = 10;

	private MeshCollider meshCollider;
	private MeshFilter meshFilter;
	
	void Start () {
		meshCollider = GetComponent<MeshCollider>();
		meshFilter = GetComponent<MeshFilter>();
	}
	
	void Update () {
		
	}

	public override void OnTouchBegin(RaycastHit hit) 
	{

	}
	
	public override void OnTouchHold(RaycastHit hit) 
	{
		/*if(Time.frameCount % updateRate == 0){
			Mesh mesh = meshFilter.mesh;
			Vector3[] vertices = mesh.vertices;
			Vector3[] normals = mesh.normals;

			Vector3 nearestVertex = ;
			float closestDistance = ;

			foreach (Vector3 vertex in vertices)
			{
				if(Vector3.Distance(vertex, hit.point) < accuracy)
				{
					nearestVertex = vertex;

				}
			}

			mesh.vertices = vertices;
		}*/
	}
	
	public override void OnTouchReleased() 
	{

	}
}
