using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraContoller : MonoBehaviour {

    public Camera playerCamera;
    // Use this for initialization
    void Start () {
		
	}
    Vector3 movement;
    float rotate;
    // Update is called once per frame
    void Update () {
        movement = Input.GetAxis("Vertical") * playerCamera.transform.TransformDirection(Vector3.forward) + (Input.GetAxis("Horizontal") * playerCamera.transform.TransformDirection(Vector3.right)) ;
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(movement.x/5, 0, movement.z/5));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "chunk") {
           other.GetComponentInParent<HexGridChunk>().loadTrigger.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "chunk")
        {
            other.GetComponentInParent<HexGridChunk>().loadTrigger.SetActive(false);
        }
    }
}
