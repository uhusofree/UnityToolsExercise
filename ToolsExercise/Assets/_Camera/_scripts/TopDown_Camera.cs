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
        [SerializeField] private float angle = 45f;

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
            Vector3 stablePosition = target.position;
            stablePosition.y = 0f;
            Vector3 finalCamPos = stablePosition + rotatedVec;
            transform.position = finalCamPos;
            transform.LookAt(target.position);

        }

        private void OnDrawGizmos()
        {

            Gizmos.color = new Color(0f, 0f, 1f, .25f);
            if (target)
            {
                Gizmos.DrawLine(transform.position, target.position);
                Gizmos.DrawSphere(target.position, 1.75f);
            }

            Gizmos.DrawSphere(transform.position, .75f);

            #region GIZMOS FOR VECTOR LINES FROM CAMERA TO TARGET
            ////cameras dist and height debug line
            //Gizmos.color = Color.green;
            //Gizmos.DrawLine(target.position, (Vector3.forward * -dist) + (Vector3.up * height));

            ////cameras rotation debug line
            //Gizmos.color = Color.red;
            //Gizmos.DrawLine(target.position, Quaternion.AngleAxis(angle, Vector3.up)
            //    * (Vector3.forward * -dist) + (Vector3.up * height));
            ////cameras finalPos debug line
            //Gizmos.color = Color.blue;
            //Gizmos.DrawLine(target.position, (new Vector3(target.position.x, 0f, target.position.z) + target.position) + (Quaternion.AngleAxis(angle, Vector3.up)
            //    * ((Vector3.forward * -dist) + (Vector3.up * height))));
            #endregion
        }
    }
}
