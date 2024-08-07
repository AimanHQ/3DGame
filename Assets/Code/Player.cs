using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float AxisH;
    float AxisV;
    bool DownWalk;

    Vector3 moveVec;
    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AxisH = Input.GetAxisRaw("Vertical");
        AxisV = Input.GetAxisRaw("Horizontal");
        DownWalk =Input.GetButton("Walk");

        moveVec = new Vector3(AxisH, 0, AxisV).normalized;

        transform.position += moveVec * speed * (DownWalk ? 0.3f : 1f) * Time.deltaTime;


        anim.SetBool("IsRun", moveVec != Vector3.zero);
        anim.SetBool("IsWalk", DownWalk);

        transform.LookAt(transform.position + moveVec);

    }
}
