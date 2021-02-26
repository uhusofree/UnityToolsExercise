using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    


    [SerializeField] float speed = 10f;
    [SerializeField] float tiltAngle = 50f;
    [SerializeField] float smooth = 10f;
    
    Vector3 targetPos;
    Position pos;

    private void Start()
    {
        targetPos = pos.GetPosition();
    }

    private void Update()
    {
        //move to targeting function
        float turnX = targetPos.x * tiltAngle;
        float turnZ = targetPos.z * tiltAngle;
        Quaternion target = Quaternion.Euler(turnX, 0, turnZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, smooth * Time.deltaTime);

        transform.position += Vector3.forward  * speed * Time.deltaTime;
        //transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }


}
