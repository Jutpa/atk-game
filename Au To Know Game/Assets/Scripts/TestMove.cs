using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMove : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    private float playerSpeedInit = 2.0f;
    private float playerSpeedTemp = 4.0f;
    public float playerRotSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float playerHealth = 100.0f;
    public float playerMaxHealth = 100.0f;
    public float playerHealthInit = 100.0f;
    public float playerStamina = 100.0f;
    public float playerMaxStamina = 100.0f;
    public float playerStaminaInit = 100.0f;
    public float playerLevel = 0.0f;
    public Transform playerTransform;
    public Transform cameraTransform;
    public Vector3 offset;
    public bool healthRegenerating = false;

    //public Vector3 offplay;
    //public int playerScore = 0;

    public AudioClip levelUp;
    public AudioSource audioSource;

    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public ScoreScript scoreScript;
    public HealthScript healthScript;
    public StaminaScript staminaScript;
    public GameObject particle;
    public PauseScript pauseScript;
    public ParticleSystem particleregen;
    public ParticleSystem staminaregen;

    private void Start()
    {
        playerHealth = playerMaxHealth;
        controller = gameObject.GetComponent<CharacterController>();
        healthBar.SetMaxHealth(playerMaxHealth);
        healthBar.SetHealth(playerHealth);
        staminaBar.SetMaxStamina(playerMaxStamina);
        staminaBar.SetStamina(playerStamina);
        playerSpeedTemp = playerSpeed * 2.0f;
        playerSpeedInit = playerSpeed;
        playerHealthInit = playerMaxHealth;
        playerStaminaInit = playerMaxStamina;
    }

    void Update()
    {
        // Checks if the player is on the ground
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //offplay = Vector3.RotateTowards(offset, playerTransform.position, 1.0f, 0.0f);
        //cameraTransform.position = Vector3.Lerp(cameraTransform.position, playerTransform.position + offplay, 0.01f);
        //cameraTransform.LookAt(playerTransform.position);
        
        // Moves the player using the WASD keys
        //Vector3 move =  new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //controller.Move(move * Time.deltaTime * playerSpeed);

        // Rotate player
        transform.Rotate(0, Input.GetAxis("Horizontal") * playerRotSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = playerSpeed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);

        // Jumping
        if (Input.GetButtonDown("Jump") && groundedPlayer && playerStamina > 0.0f)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            staminaLoss(2.0f);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Health regeneration, costs stamina and requires the player to not be moving
        if (playerHealth < playerMaxHealth && playerStamina > 0.0f && Input.GetAxis("Vertical") == 0.0f)
        {
            healthRegenerating = true;
            takeDamage(-0.01f);
            staminaLoss(0.02f);
            particleregen.Play();
            
        }
        else
        {
            healthRegenerating = false;
            particleregen.Stop();
        }

        // Stamina regeneration, requires the player to not be moving
        if (playerStamina < playerStaminaInit && Input.GetAxis("Vertical") == 0.0f && healthRegenerating == false)
        {
            staminaLoss(-0.01f);
            staminaregen.Play();
        }
        else
        {
            staminaregen.Stop();
        }

        // Sprinting, costs twice as much stamina as regeneration
        if (Input.GetKey("left shift") && playerStamina > 0.0f && Input.GetAxis("Vertical") != 0.0f)
        {
            playerSpeed = playerSpeedTemp;
            staminaLoss(0.04f);
        }
        else
        {
            playerSpeed = playerSpeedInit;
        }

        // Death
        if (playerHealth <= 0.0f)
        {
            pauseScript.GameOver();
            Destroy(gameObject);
        }

        // Temp stamina loss test
        /*if (Input.GetKeyDown("t") && playerStamina > 0.0f)
        {
            staminaLoss(10.0f);
        }*/

        // If player's health is significantly greater than player's maximum health, do the health decay
        if (playerHealth > playerMaxHealth*1.5f)
        {
            takeDamage(0.03f);
        }

        // Levelling system: for every 10 kills your maximum HP permanently increases by 10 and your maximum Stamina permanently increases by 5
        if (scoreScript.ScoreNotification() != playerLevel)
        {
            Debug.Log("Level up!");
            playerLevel = scoreScript.ScoreNotification();
            playerMaxHealth = playerHealthInit + playerLevel * 10.0f;
            playerMaxStamina = playerStaminaInit + playerLevel * 5.0f;
            Instantiate(particle, transform.position, transform.rotation);
            audioSource.PlayOneShot(levelUp, 0.75f);
        }

        healthScript.SetHealth((int) playerHealth, (int) playerMaxHealth);
        healthBar.SetMaxHealth(playerMaxHealth);
        staminaScript.SetStamina((int) playerStamina, (int) playerMaxStamina);
        staminaBar.SetMaxStamina(playerMaxStamina);
    }

    void staminaLoss (float stamina)
    {
        playerStamina -= stamina;
        staminaBar.SetStamina(playerStamina);
    }

    void takeDamage(float damage)
    {
        playerHealth -= damage;
        healthBar.SetHealth(playerHealth);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hazard"))
        {
            takeDamage(1.0f);
        }

        if (other.CompareTag("Enemy"))
        {
            takeDamage(1.0f + (scoreScript.trueScore / 100000));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            takeDamage(-20.0f);
            staminaLoss(-20.0f);

            if (playerStamina > playerMaxStamina)
            {
                playerStamina = playerMaxStamina;
            }
        }
        if (other.CompareTag("Stamina"))
        {
            staminaLoss(-20.0f);

            if (playerStamina > playerMaxStamina)
            {
                playerStamina = playerMaxStamina;
            }
        }
        if (other.CompareTag("Instant Kill"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
}
