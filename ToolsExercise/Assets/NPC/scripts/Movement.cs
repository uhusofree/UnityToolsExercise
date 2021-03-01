using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{



    [SerializeField] float speed = 10f;
    [SerializeField] float tiltAngle = 50f;
    [SerializeField] float smooth = 10f;
    [SerializeField] float radius = 10f;

    public Transform targetPos;
    public Transform wayPoint;
    Position pos;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //move to targeting function

        targetPos.position = GameObject.FindObjectOfType<Position>().transform.position;

        if (Vector3.Distance(transform.position, targetPos.position) < radius)
        {
            Vector3 targetDirection = targetPos.position - transform.position;

            float singleStep = tiltAngle * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            Debug.DrawRay(transform.position, newDirection, Color.red);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            //float moveDeltaTime = speed * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, wayPoint.position, moveDeltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }







        //transform.position += Vector3.forward * speed * Time.deltaTime;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
