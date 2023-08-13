﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    bool alive = true;

    public float speed = 5;
    [SerializeField] Rigidbody rb;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;

    private void FixedUpdate ()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Update () {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)){
            jump();
        }
        if (Input.GetKeyDown(KeyCode.W)){
            jump();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            jump();
        }

        if (transform.position.y < -5) {
            Die();
        }
	}

    public void Die ()
    {
        alive = false;
        FindObjectOfType<AudioManager>().PlaySound("GameOver");
        // Restart the game
        Invoke("Restart", 2);
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void jump(){
        //ground check
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGround = Physics.Raycast(transform.position,Vector3.down, (height / 2)+ 0.1f, groundMask);

        //if on jump
        rb.AddForce(Vector3.up * jumpForce);
    }
}