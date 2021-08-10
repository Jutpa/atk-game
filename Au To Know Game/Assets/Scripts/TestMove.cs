using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public int playerHealth = 100;
    public int playerMaxHealth = 100;
    public int playerStamina = 100;
    public int playerMaxStamina = 100;
    private bool playerMoving = true;

    public HealthBar healthBar;
    public StaminaBar staminaBar;

    private void Start()
    {
        playerHealth = playerMaxHealth;
        controller = gameObject.AddComponent<CharacterController>();
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    void Update()
    {
        // Checks if the player is on the ground
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Moves the player using the WASD keys
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);


        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer && playerStamina > 0)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            staminaLoss(2);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Heal test
        if (Input.GetButtonDown("Fire1") && playerHealth < playerMaxHealth)
        {
            takeDamage(-10);
        }

        // Stamina heal test
        if (Input.GetButtonDown("Fire1") && playerStamina < playerMaxStamina)
        {
            staminaLoss(-10);
        }

        // Death test
        if (playerHealth <= 0)
        {
            Debug.Log("DIED");
            playerHealth = 0;
            int remaining_stamina = playerStamina;
            staminaLoss(remaining_stamina);
            playerSpeed = 0.0f;
        }
        else
        {
            playerSpeed = 5.0f;
        }

        // Stamina regen
        // Checks if player is moving
        if (playerVelocity.x == 0.0f && playerVelocity.z == 0.0f)
        {
            playerMoving = false;
            Debug.Log("NOT MOVING");
        }
        else
        {
            playerMoving = true;
            Debug.Log("MOVING");
        }

        // If player is not moving, regenerates stamina
        //if (!playerMoving && playerStamina < playerMaxStamina)
        //{
        //    playerStamina += 1;
        //}

        //if (playerHealth > playerMaxHealth)
        //{
        //    healthDecay(1);
        //}
    }

    void staminaLoss (int stamina)
    {
        playerStamina -= stamina;
        staminaBar.SetStamina(playerStamina);
    }

    void takeDamage(int damage)
    {
        playerHealth -= damage;
        healthBar.SetHealth(playerHealth);
    }

    //void healthDecay(int decay_rate)
    //{
    //    playerHealth -= decay_rate / Time.deltaTime;
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hazard")
        {
            takeDamage(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Health")
        {
            takeDamage(-20);
        }
        if (other.tag == "Stamina")
        {
            playerStamina += 20;

            if (playerStamina > playerMaxStamina)
            {
                playerStamina = playerMaxStamina;
            }
        }
    }
}
