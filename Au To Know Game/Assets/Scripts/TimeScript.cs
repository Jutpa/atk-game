using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    public Text textt;
    public RotateScript dayCycle;
    public int timePassed = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        textt.text = "Time Elapsed: " + timePassed;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed = (int)dayCycle.rotXval;
        textt.text = "Time Elapsed: " + timePassed;
    }
}
