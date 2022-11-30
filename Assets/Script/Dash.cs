using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dash : MonoBehaviour
{
    //private bool CanDash = true;
    private bool DashE;
    public float DashSpeed;
    private float StartDashCount;
    public float DashCount;
    private int side;

    private Rigidbody2D rb;
    //private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E) && CanDash)
        //   {
        //       StartCoroutine(DashE());
        //   }

        //if (Dashing)
        //   {
        //       return;
        //   }

        //DASH
        if (side == 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                side = 1;

            }

            else if (Input.GetKeyDown(KeyCode.E))
            {
                side = 2;
            }

            else
            {
                if (DashCount <= 0)
                {
                    side = 0;
                    DashCount = StartDashCount;
                    rb.velocity = Vector2.zero;
                    DashCount = Time.deltaTime;
                }

                if (side == 1)
                {
                    rb.velocity = Vector2.left * DashSpeed;
                }
                else if (side == 2)
                {
                    rb.velocity = Vector2.right * DashSpeed;
                }
            }
        }
    }
    //private void FixedUpdate()
    //{
    //    //if (Dashing)
    //    //{ 
    //    //    return; 
    //    //}
    //}
    //private IEnumerator DashE()
    //{
    //    CanDash = false;
    //    Dashing = true;
    //    float originalGravity = rb.gravityScale;
    //   rb.gravityScale = 0f;
    //    rb.velocity = new Vector2(transform.localScale.x * DashForce, 0f);
    //    tr.emitting = true;
    //    yield return new WaitForSeconds(DashTime);
    //    tr.emitting = false;
    //    rb.gravityScale = originalGravity;
    //    Dashing = false;
    //    yield return new WaitForSeconds(DashWait);
    //    CanDash = true;
    //}


}
