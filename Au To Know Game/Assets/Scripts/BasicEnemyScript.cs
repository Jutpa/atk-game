using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 enemyVelocity;
    public float enemySpeed = 1.0f;
    public float enemyHealth = 100.0f;
    public float enemyMaxHealth = 100.0f;
    public float gravityValue = -9.81f;
    private bool playerFound = true;
    private bool groundedEnemy = false;
    //private Collider coll;
    private GameObject eclone;
    private GameObject potiondrop;
    public int score = 0;

    public AudioClip idle;
    public AudioSource audioSource;

    //public GameObject cap;
    public SubEnemyScript other;
    public Transform target;
    public Transform spawnpoint;
    public Transform sp2;
    public Transform enemypos;
    public GameObject eeeee;
    public GameObject hpot;
    public GameObject particle;
    public Vector3 ransp;
    public ScoreScript scoreScript;

    void Start()
    {
        //coll = cap.GetComponent<Collider>();
        controller = gameObject.AddComponent<CharacterController>();
        enemyHealth = enemyMaxHealth;

        StartCoroutine(IdleCoroutine());
    }

    void Update()
    {
        controller = gameObject.GetComponent<CharacterController>();

        enemypos = gameObject.transform;
        
        Vector3 targetrot = new Vector3(target.position.x, 1.58f, target.position.z);

        // Checks if the enemy is on the ground
        groundedEnemy = controller.isGrounded;
        if (groundedEnemy && enemyVelocity.y < 0)
        {
            enemyVelocity.y = 0f;
        }

        enemyVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(enemyVelocity * Time.deltaTime);

        // Debug in case of enemy spawning a kilometer below the play area
        if (gameObject.transform.position.y < 0)
        {
            gameObject.transform.position += new Vector3 (0.0f, 10.0f, 0.0f);
        }

        if (playerFound == true)
        {
            // Move enemy position a step closer to the target.
            float step = enemySpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            // Determine which direction to rotate towards
            Vector3 targetDirection = targetrot - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = enemySpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        if (enemyHealth <= 0)
        {
            Death();
        }

        if (other.HazardCheck() == true)
        {
            takeDamage(1);
        }

        if (other.WeaponCheck() == true)
        {
            takeDamage(10);
        }
    }

    void takeDamage(int damage)
    {
        enemyHealth -= damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerFound = true;
            target = other.gameObject.transform;
        }
    }

    IEnumerator IdleCoroutine()
    {
        while (true)
        {
            float ranint = Random.value;
            ranint *= 5;
            Debug.Log("Coroutine start! Expected wait time: " + ranint);
            yield return new WaitForSeconds(ranint);
            audioSource.PlayOneShot(idle, 0.75f);
            Debug.Log("Coroutine end!");
        }
    }

    void Death()
    {
        if (scoreScript.trueScore <= 800)
        {
            for (int i = 0; i < 2; i++)
            {
                // Sets a local variable for the loot table
                float ranint = Random.value;
                ranint *= 20;
                Debug.Log(ranint.ToString());

                // Code for spawning loot, happens 1/10th of the time
                if (ranint > 19)
                {
                    Debug.Log("spawned");
                    potiondrop = Instantiate(hpot, enemypos.position, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
                }
                
                if (i == 0)
                {
                    eclone = Instantiate(eeeee, spawnpoint.position, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
                    ransp = new Vector3(Random.Range(-20.0f, 10.0f), 0.0f, Random.Range(-20.0f, 10.0f));
                    Debug.Log(ransp.ToString());
                    eclone.transform.position = spawnpoint.position + ransp;
                }
                else
                {
                    eclone = Instantiate(eeeee, sp2.position, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
                    ransp = new Vector3(Random.Range(-10.0f, 20.0f), 0.0f, Random.Range(-20.0f, 10.0f));
                    Debug.Log(ransp.ToString());
                    eclone.transform.position = sp2.position + ransp;
                }
            }

            score += 100;
            scoreScript.SetScore(score);
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else
        {
            if (scoreScript.trueScore >= 9000)
            {
                score += 100;
                scoreScript.SetScore(score);
                Destroy(gameObject);
            }
            else
            {
                float ranint = Random.value;
                ranint *= 10;
                Debug.Log(ranint.ToString());

                // Code for spawning loot, happens 1/10th of the time
                if (ranint >= 9)
                {
                    Debug.Log("spawned");
                    potiondrop = Instantiate(hpot, enemypos.position, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
                }

                if ((int)ranint % 2 == 0)
                {
                    eclone = Instantiate(eeeee, spawnpoint.position, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
                    ransp = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f));
                    eclone.transform.position = spawnpoint.position + ransp;
                }
                else
                {
                    eclone = Instantiate(eeeee, sp2.position, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
                    ransp = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f));
                    eclone.transform.position = sp2.position + ransp;
                }

                score += 100;
                scoreScript.SetScore(score);
                Destroy(gameObject);
            }
        }
    }
}
