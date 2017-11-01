// Author: Itai Yavin
// Contributors:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkableGround : Holdable 
{
	[Tooltip("The rate of which the mesh is updated. If set to 5 then each fifth frame, etc.")]
	public int updateRate = 10;
	
	[Header("Sinking")]
	[Tooltip("The speed of which sinking happens")]
	[Range(0.01f, 1.0f)]
	public float sinkSpeed = 0.1f;
	[Tooltip("The amount of time before sinking")]
	public float sinkDelay = 0.0f;
	[Tooltip("Finds all vertices within this radius")]
	public float radius = 2.0f;
	[Tooltip("The maximum depth that vertices will sink to")]
	public float depth = 0.5f;

	[Header("Rising")]
	[Tooltip("The speed of which the ground will rise again")]
	public float riseSpeed = 0.1f;
	[Tooltip("The amount of seconds before the ground will rise again")]
	public float riseDelay = 2.0f;

	private MeshCollider meshCollider;
	private MeshFilter meshFilter;
	private Mesh mesh;
	private Vector3[] originalVerticePositions;
	private Vector3[] currentVertices;
	private float[] verticeTimes;
	private bool verticesWaiting = false;
	private bool madeChange = false;

	private List<Pair<int, float>> nearestPoints = new List<Pair<int, float>>();
	
	void Start () {
		meshCollider = transform.GetChild(0).GetComponent<MeshCollider>();
		meshFilter = GetComponent<MeshFilter>();

		mesh = meshFilter.mesh;
		meshCollider.sharedMesh = mesh;
		originalVerticePositions = mesh.vertices;
		currentVertices = mesh.vertices;

		verticeTimes = new float[originalVerticePositions.Length];
	}
	
	void Update () 
	{
		if(verticesWaiting && Time.frameCount % updateRate == 0)
		{
			verticesWaiting = false;
			for (int i = 0; i < verticeTimes.Length; i++)
			{
				if (verticeTimes[i] != 0.0f)
				{
					verticesWaiting = true;

					if (Time.time > verticeTimes[i])
					{
						if (currentVertices[i].y + sinkSpeed < originalVerticePositions[i].y)
						{
							currentVertices[i].y += sinkSpeed;
						}
						else
						{
							currentVertices[i] = originalVerticePositions[i];
							verticeTimes[i] = 0.0f;
						}

						madeChange = true;
					}
				}
			}

			if (madeChange)
			{
				mesh.vertices = currentVertices;
				mesh.RecalculateNormals();

				meshCollider.sharedMesh = mesh;

				madeChange = false;
			}
		}
	}

	public override void OnTouchBegin(RaycastHit hit) 
	{
		nearestPoints.Clear();

		Vector3 pointHit = hit.point;
		pointHit = transform.InverseTransformPoint(pointHit);

		mesh = meshFilter.mesh;
		currentVertices = mesh.vertices;
		Vector3[] normals = mesh.normals;

		float distance;
		for(int i = 0; i < currentVertices.Length; i++)
		{
			distance = Vector3.Distance(currentVertices[i], pointHit);
			if (distance < radius)
			{
				Pair<int, float> newPoint = new Pair<int, float>(i, distance);
				nearestPoints.Add(newPoint);
			}
		}

		OrderPairList(ref nearestPoints);
	}
	
	public override void OnTouchHold(RaycastHit hit) 
	{
		if(Time.frameCount % updateRate == 0)
		{
			currentVertices = mesh.vertices;

			if(Vector3.Distance(currentVertices[nearestPoints[0].GetFirst()], originalVerticePositions[nearestPoints[0].GetFirst()]) < depth)
			{
				foreach (Pair<int, float> pair in nearestPoints)
				{
					currentVertices[pair.GetFirst()].y -= sinkSpeed * ((radius - pair.GetSecond()) / radius);
					verticeTimes[pair.GetFirst()] = Time.time + riseDelay;	

					verticesWaiting = true;
				}
				
				mesh.vertices = currentVertices;
				mesh.RecalculateNormals();

				meshCollider.sharedMesh = mesh;
			}
		}
	}
	
	public override void OnTouchReleased() 
	{

	}

	private void OrderPairList(ref List<Pair<int, float>> pairList)
	{
		Pair<int, float> temporary;
		for (int i = 1; i < pairList.Count; i++)
		{
			for(int j = 0; j < pairList.Count - i; j++)
			{
				if (pairList[j].GetSecond() > pairList[j + 1].GetSecond())
				{
					temporary = pairList[j];
					pairList[j] = pairList[j + 1];
					pairList[j + 1] = temporary;
				}
			}
		}
	}

	private struct Pair<T, G>
	{
		public Pair(T t, G g)
		{
			value1 = t;
			value2 = g;
		}

		T value1;
		G value2;

		public T GetFirst()
		{
			return value1;
		}

		public G GetSecond()
		{
			return value2;
		}

		public void SetFirst(T t)
		{
			value1 = t;
		}

		public void SetSecond(G g)
		{
			value2 = g;
		}

		public void Print()
		{
			Debug.Log("First: " + value1 + " - Second: " + value2);
		}
	}
}
