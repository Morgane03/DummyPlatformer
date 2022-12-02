using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    public float DashSpeed;

    public int MaxNumberOfDash = 4;
    private int NumberOfDash = 0;
    Rigidbody2D rb;
    public float DashPower;
    public float DashTime;
    public float DashWait = 1f;
    bool IsDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        

    }
    void Update()
    {
        if (NumberOfDash == MaxNumberOfDash)
        {
            IsDash = false;
           
        }
        if (NumberOfDash > MaxNumberOfDash)
        {
            IsDash = true;
        }

        if (IsDash == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                rb.velocity = new Vector3( );
                NumberOfDash = NumberOfDash + 1;
                
            }
            if (Input.GetKeyDown(KeyCode.A))
            {

                NumberOfDash = NumberOfDash + 1;

            }
            if (Input.GetKeyDown(KeyCode.Z))
            {

                NumberOfDash = NumberOfDash + 1;

            }
            if (Input.GetKeyDown(KeyCode.S))
            {


                NumberOfDash = NumberOfDash + 1;

            }
            else
            {
                return;
            }
        }



    }
}