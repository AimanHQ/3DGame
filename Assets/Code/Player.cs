using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float AxisH;
    float AxisV;
    bool DownWalk;
    bool jump;
    Rigidbody rb;

    Vector3 moveVec;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
    }

    void GetInput() 
    {
        AxisH = Input.GetAxisRaw("Vertical");
        AxisV = Input.GetAxisRaw("Horizontal");
        DownWalk =Input.GetButton("Walk");  
        jump = Input.GetButtonDown("Jump");      
    }

    void Move()
    {
        moveVec = new Vector3(AxisH, 0, AxisV).normalized;

        transform.position += moveVec * speed * (DownWalk ? 0.3f : 1f) * Time.deltaTime;


        anim.SetBool("IsRun", moveVec != Vector3.zero);
        anim.SetBool("IsWalk", DownWalk);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jump) {
            rb.AddForce(Vector3.up * 15 , ForceMode.Impulse);
        }
    }
}
