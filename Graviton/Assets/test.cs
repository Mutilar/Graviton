using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    float counter = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counter+= .001f;
        this.transform.Rotate(new Vector3(0, 0, -counter));
        this.transform.Translate(new Vector3(0, counter * counter));
		
	}
}
