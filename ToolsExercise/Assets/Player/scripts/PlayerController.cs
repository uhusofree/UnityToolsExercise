using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

namespace UnityTools.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public float movementSpeed;
        public float rotationSpeed;

        // Start is called before the first frame update

        // Update is called once per frame
        void Update()
        {
            movementSpeed = Input.GetAxis("Horizontal") * Time.deltaTime;
            rotationSpeed = Input.GetAxis("Vertical") * Time.deltaTime;

            transform.Translate(0, 0, movementSpeed);
            transform.Rotate(0, rotationSpeed, 0); 
        }
    }

}
