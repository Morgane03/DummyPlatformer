using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    public float Dashspeed;
    public int MaxNumberOfDash = 3;
    Rigidbody2D rb;
    float DoubleTap;
    KeyCode LastKeyCode;

    private float DashCount;
    public float StartDashCount;
    private int side;


    void Start()
    {
        DashCount = StartDashCount;
    }
    void Update()
    {
        //dash

        if (side == 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (DoubleTap > Time.time && LastKeyCode == KeyCode.A)
                {
                    side = 1;
                }
                else
                {
                    DoubleTap = Time.time + 0.5f;
                }
                LastKeyCode = KeyCode.A;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (DoubleTap > Time.time && LastKeyCode == KeyCode.E)
                    {
                        side = 2;
                    }
                    else
                    {
                        DoubleTap = Time.time + 0.5f;
                    }
                    LastKeyCode = KeyCode.E;
                }
            }
            else
            {
                if (DashCount <= 0)
                {
                    side = 0;
                    DashCount = StartDashCount;
                    rb.velocity = Vector2.zero;
                }
                else
                {
                    DashCount = Time.deltaTime;
                    if (side == 1)
                    {
                        rb.velocity = Vector2.left * Dashspeed;
                    }
                    else if (side == 2)
                    {
                        rb.velocity = Vector2.right * Dashspeed;
                    }
                }
            }
        }
    }
}