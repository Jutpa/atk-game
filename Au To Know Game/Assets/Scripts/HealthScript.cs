using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Text textt;
    public int trueHealth = 100;
    public int trueMaxHealth = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        textt.text = "Health: " + trueHealth + "/" + trueMaxHealth;
    }

    public void SetHealth(int health, int maxHealth)
    {
        trueHealth = health;
        trueMaxHealth = maxHealth;
        textt.text = "Health: " + trueHealth + "/" + trueMaxHealth;
    }
}
