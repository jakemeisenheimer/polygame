using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {

    public float movemnetAmount;
    private float timer;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool up;
    private bool end;
    public float waitTime = 2;
    public float startDelay;
	void Start () {
        startPos = transform.position;
        endPos = transform.position + Vector3.up * movemnetAmount;
        timer += startDelay;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
      
        if (timer > waitTime)
        {
            if (Approximately( transform.position.y,endPos.y))
            {
                up = false;
                timer = 2.05f;
            }
            if (Approximately(transform.position.y, startPos.y) && !end)
            {
                end = true;
                up = true;
                timer = 2.05f;
            }else if(Approximately(transform.position.y, startPos.y) && end)
            {
                end = false;
                up = true;
                timer = .05f;
            }
            if (up)
            {
                transform.position = Vector3.Lerp(startPos, endPos, (timer-2) * 4);
            }
            else
            {
                transform.position = Vector3.Lerp(endPos, startPos, timer-2);
            }
        }
    }

    bool Approximately(float x, float y) {
        if(Mathf.Abs(x-y) < .01)
        {
            return true;
        }
        return false;
    }
}
