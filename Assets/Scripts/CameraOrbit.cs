using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour {
    public Transform target;
    public float distance;
    public float maxDistance;
    public RayCast3 Distance;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;
    public Quaternion rotation;
    private Rigidbody rigidbody;
    public KeyCode right;
    public KeyCode left;
    public  float x = 0.0f;
    public   float y = 15f;

    // Use this for initialization
    void Start()
    {
        distance = 15.0f;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            y = ClampAngle(y, yMinLimit, yMaxLimit);

             rotation = Quaternion.Euler(y, x, 0);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    void Update() {
        y = 15;
        distance = Distance.distance3;
        if (distance >maxDistance) {
            distance = maxDistance;
        }
        if (Input.GetKey(right))
            {
            x += 3;
            }
            if (Input.GetKey(left))
            {
            x -= 3;
        }
        
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
