using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubEnemyScript : MonoBehaviour
{
    private bool inHazard = false;
    private bool inWeapon = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HazardCheck();
        WeaponCheck();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hazard")
        {
            inHazard = true;
        }
        else
        {
            inHazard = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            inWeapon = true;
        }
        else
        {
            inWeapon = false;
        }
    }

    public bool HazardCheck()
    {
        if (inHazard)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool WeaponCheck()
    {
        if (inWeapon)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
