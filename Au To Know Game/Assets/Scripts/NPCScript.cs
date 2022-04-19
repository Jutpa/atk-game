using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    //private Vector3 npcVelocity;
    public float npcHealth = 20.0f;
    public float npcSpeed = 15.0f;
    public float gravityValue = -9.81f;
    //private bool groundedNPC = false;

    public AudioClip idle;
    public AudioClip death;
    public AudioSource audioSource;

    //private Transform enemy;
    public Transform originpos;
    public PauseScript pauseScript;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IdleCoroutine());
        StartCoroutine(ActionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        // Debug in case of NPC falling off the map
        if (gameObject.transform.position.y < 0)
        {
            gameObject.transform.position += new Vector3(0.0f, 10.0f, 0.0f);
        }

        if (npcHealth <= 0)
        {
            Death();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            npcHealth -= 1.0f;
        }
    }

    private void Move()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target.GetComponent<TargetMovement>().NewPosition();
        Debug.Log("Position get!");
        agent.destination = target.transform.position;
        Debug.Log("Position reached!");
    }

    IEnumerator IdleCoroutine()
    {
        while (true)
        {
            float ranint = Random.value;
            ranint *= 5;
            //Debug.Log("Sound coroutine start! Expected wait time: " + ranint);
            yield return new WaitForSeconds(ranint);
            audioSource.PlayOneShot(idle, 0.75f);
            //Debug.Log("Sound coroutine end!");
        }
    }

    IEnumerator ActionCoroutine()
    {
        while (true)
        {
            float ranint = Random.value;
            //Debug.Log("Action coroutine start! Value produced: " + ranint);
            if (ranint >= 0.5f)
            {
                Move();
            }
            else
            {

            }
            yield return new WaitForSeconds(5);
            //Debug.Log("Action coroutine end!");
        }
    }

    void Death()
    {
        audioSource.PlayOneShot(death, 0.75f);
        Debug.Log("losing condition");
        pauseScript.Lose();
        Destroy(gameObject);
    }
}
