using UnityEngine;
using System.Collections;

public class RayCast3 : MonoBehaviour
{
    public float distance3 = 5;
    public RaycastHit hit;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if(hit.collider.tag == "chunk")
            distance3 = hit.distance;
        }
        else
        {
            distance3 = 10;
        }
    }
}
