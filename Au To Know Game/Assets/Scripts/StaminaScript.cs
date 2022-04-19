using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour
{
    public Text textt;
    public int trueStamina = 100;
    public int trueMaxStamina = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        textt.text = "Stamina: " + trueStamina + "/" + trueMaxStamina;
    }

    public void SetStamina(int stamina, int maxStamina)
    {
        trueStamina = stamina;
        trueMaxStamina = maxStamina;
        textt.text = "Stamina: " + trueStamina + "/" + trueMaxStamina;
    }
}
