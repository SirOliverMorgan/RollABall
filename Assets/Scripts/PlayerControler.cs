using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControler : MonoBehaviour
{
    public float speed = 0;
    public float distToGround = 0.5f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementZ;
    private int jmpCnt = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround);
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    void OnJump()
    {
        if (jmpCnt == 0)
        {
            jmpCnt = 1;
            rb.velocity = new Vector3(0, 10, 0);
        }
        else if (jmpCnt == 1)
        {
            jmpCnt = 2;
            rb.velocity = new Vector3(0, 10, 0);
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isGrounded())
        {
            jmpCnt = 0;
        }

        Vector3 movement = new Vector3(movementX, 0.0f, movementZ);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
