using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTeste : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * 10, Color.green);
        if (Physics.Raycast(transform.position, Vector3.down * 10, out hit, Mathf.Infinity))
        {
            float yPos = hit.point.y;
            Debug.Log(yPos);
        }
    }
}
