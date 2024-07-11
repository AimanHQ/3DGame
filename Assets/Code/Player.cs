using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float AxisH;
    float AxisV;

    Vector3 moveVec;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AxisH = Input.GetAxisRaw("Horizontal");
        AxisV = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(AxisH, 0, AxisV).normalized;

        transform.position += moveVec * speed * Time.deltaTime;
    }
}
