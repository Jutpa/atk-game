using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    private Animator anim;
    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SwordSwing();
        }
        else
        {
            attacking = false;
            anim.Play("swordIdle");
        }

        anim.SetBool("attacking", attacking);
    }

    void SwordSwing()
    {
        attacking = true;
        Debug.Log("attacking!");
        anim.Play("swordSwing");
    }
}
