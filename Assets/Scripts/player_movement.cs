using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{

    // Variables that are setup at Start()
    public float speed = 5;
    private Vector3 axisMovement;
    private Rigidbody2D     body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Catch player movement inputs
        axisMovement.x = Input.GetAxisRaw("Horizontal");
        axisMovement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        body.velocity = axisMovement.normalized * speed;
        CheckForFlipping();
    }

    private void CheckForFlipping()
    {
        if(axisMovement.x < 0)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y);
        }
        else if(axisMovement.x > 0)
        {
            transform.localScale = new Vector3(1f, transform.localScale.y);
        }
    }
}