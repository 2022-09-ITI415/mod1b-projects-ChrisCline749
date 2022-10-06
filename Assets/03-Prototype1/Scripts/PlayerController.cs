using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float jumpPower = 10;
    public Text countText;
    public GameObject winText;

    private Rigidbody rb;
    private float moveX;
    private float moveY;
    private float moveZ;
    private int count;

    private bool hasJumped;

    // Start is called before the first frame update
    void Start()
    {
        moveX = 0;
        moveY = 0;
        moveZ = 0;

        hasJumped = false;

        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winText.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        moveX = movementVector.x;
        moveY = movementVector.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp") == true)
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") == true)
        {
            hasJumped = false;
        }
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && hasJumped == false)
        {
            moveZ = 1;
            hasJumped = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveX * speed, moveZ * jumpPower, moveY * speed);
        if (moveZ != 0)
        {
            moveZ = 0;
        }

        rb.AddForce(movement);
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 7)
        {
            winText.SetActive(true);
        }
    }
}
