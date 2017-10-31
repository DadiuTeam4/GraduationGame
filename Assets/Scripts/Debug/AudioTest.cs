using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AkSoundEngine.PostEvent ("test1", gameObject); 
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			AkSoundEngine.SetRTPCValue ("testparameter", 0); 
		} else {
			AkSoundEngine.SetRTPCValue ("testparameter", 100); 
		}
		
	}
}
