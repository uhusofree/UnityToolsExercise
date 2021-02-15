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
            float movement = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
            float rotation = Input.GetAxis("Vertical") * Time.deltaTime * rotationSpeed;

            transform.Translate(0, 0, movement);
            transform.Rotate(0, rotation, 0);
        }
    }

}
