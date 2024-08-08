using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float AxisH;
    float AxisV;
    bool DownWalk;
    bool jump;
    bool isJump;
    bool isDodge;
    Rigidbody rb;

    Vector3 moveVec;
    Vector3 dodgeVec;
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
        Dodge();
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

        if(isDodge)
            moveVec = dodgeVec;
            
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
        if (jump && moveVec == Vector3.zero && !isJump && !isDodge) {
            rb.AddForce(Vector3.up * 15 , ForceMode.Impulse);
            anim.SetBool("IsJump", true);
            anim.SetTrigger("DoJump");
            isJump = true;
        }
    }

    void Dodge()
    {
        if (jump && moveVec != Vector3.zero && !isJump && !isDodge) {
            dodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("DoDodge");
            isDodge = true;

            Invoke("DodgeOut", 0.5f); 
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

   void OnCollisionEnter(Collision collision)
   {
        if(collision.gameObject.tag == "Floor") {
            anim.SetBool("IsJump", false);
            isJump = false;
        }
   }
}
