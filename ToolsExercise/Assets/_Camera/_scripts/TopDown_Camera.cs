using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.Cam

{
    public class TopDown_Camera : MonoBehaviour
    {
        public Transform target;
        public float height = 10f;
        public float dist = 20f;
        public float angle = 45f;

        // Start is called before the first frame update
        void Start()
        {
            HandleCam();
        }

        // Update is called once per frame
        void Update()
        {
            HandleCam();
        }

        private void HandleCam() 
        {
            if (target == null)
            {
                return;
            }
            Vector3 wPosition = (Vector3.forward * -dist) + (Vector3.up * height);

            Vector3 rotatedVec = Quaternion.AngleAxis(angle, Vector3.up) * wPosition;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green; 
            Gizmos.DrawLine(target.position, (Vector3.forward * -dist) + (Vector3.up * height));
            Gizmos.color = Color.red;
            Gizmos.DrawLine(target.position, Quaternion.AngleAxis(angle, Vector3.up) 
                * (Vector3.forward * -dist) + (Vector3.up * height));
        }
    }
}
