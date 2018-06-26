using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 5f); //allow customization
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
