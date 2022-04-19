using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public ScoreScript scoreScript;
    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 10000;
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = scoreScript.trueScore;
    }
}
