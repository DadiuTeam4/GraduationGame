using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ImageEffectAllowedInSceneView]
[ExecuteInEditMode]

public class FogImageEffect : MonoBehaviour {

	private Camera camera;
	public Material effectMaterial;

	// Use this for initialization
	void Start () {
		camera.GetComponent<Camera>();
		camera.depthTextureMode = DepthTextureMode.Depth;
	}
[ImageEffectOpaque]	
	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit(src, dst, effectMaterial);
	}
}
