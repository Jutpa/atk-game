using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaIndicatorScript : MonoBehaviour
{
    public Color color;
    public Image image;
    public TestMove player;

    // Start is called before the first frame update
    void Start()
    {
        color.a = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.playerStamina / player.playerMaxStamina) <= 0.5f)
        {
            color.a = 0.25f - (player.playerStamina / player.playerMaxStamina / 2);
            image.color = color;
        }
        else
        {
            color.a = 0.0f;
            image.color = color;
        }
    }
}
