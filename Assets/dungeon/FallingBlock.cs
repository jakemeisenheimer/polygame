using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour {

    public List<GameObject> parts;
    public float timer = 0;
    public bool active;
	void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            timer += Time.deltaTime;
            if (timer >= .2f && parts.Count > 0)
            {
                int random = Random.Range(0, parts.Count - 1);
                parts[random].AddComponent<Rigidbody>();
                parts.RemoveAt(random);
                timer = 0;
            }
            else if (parts.Count == 0)
            {
                Destroy(gameObject.GetComponent<Collider>());
            }
        }
    }
}
