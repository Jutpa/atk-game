using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 1.0f;
    public float playerRot = 1.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public float health = 100.0f;
    public float maxHealth = 100.0f;
    
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }
    
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float translation = Input.GetAxis("Vertical") * playerSpeed;
        float rotation = Input.GetAxis("Horizontal") * playerRot;

        rotation *= Time.deltaTime;

        transform.Rotate(0, rotation, 0);
        playerVelocity.z = translation;
        
        // Changes the height position of the player.
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
