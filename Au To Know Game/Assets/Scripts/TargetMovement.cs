using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    private Vector3 newpos = new Vector3(0, 0, 0);
    public Transform origin;
    public float range = 20.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewPosition()
    {
        float ranint = Random.Range(-1.0f, 1.0f) * range;
        float ranint2 = Random.Range(-1.0f, 1.0f) * range;
        newpos = new Vector3(ranint, 0, ranint2) + origin.position;
        transform.position = newpos;
    }
}
