using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace UnityTools.Cam
{

    [CustomEditor(typeof(TopDown_Camera))]
    public class Edtor_TopDownCamera : Editor
    {
        private TopDown_Camera targetCam;



        private void OnEnable()
        {
            targetCam = target as TopDown_Camera;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        private void OnSceneGUI()
        {
            if (!targetCam.target)
            {
                return;
            }

            Transform camTranform = targetCam.target;

            Handles.color = new Color(1f, 0f, 1f, .30f);
            Handles.DrawSolidDisc(camTranform.position, Vector3.up, targetCam.dist);

            Handles.color = new Color(1f, 0f, 0f, 1f);
            Handles.DrawWireDisc(camTranform.position, Vector3.up, targetCam.dist);


            Handles.color = new Color(1f, 0f, 0f, .5f);
            targetCam.dist = Mathf.Clamp(targetCam.dist, 5f, float.MaxValue);
            targetCam.dist = Handles.ScaleSlider(targetCam.dist, camTranform.position, -camTranform.forward, Quaternion.identity, targetCam.dist, 1f);

            Handles.color = new Color(0f, 0f, 1f, .5f);
            targetCam.height = Mathf.Clamp(targetCam.height, 10f, float.MaxValue);
            targetCam.height = Handles.ScaleSlider(targetCam.height, camTranform.position, Vector3.up, Quaternion.identity, targetCam.height, 1f);


            GUIStyle handlesStyle = new GUIStyle();
            handlesStyle.fontSize = 12;
            handlesStyle.alignment = TextAnchor.MiddleCenter;
            handlesStyle.normal.textColor = Color.black;

            Handles.Label(camTranform.position + (-camTranform.forward * targetCam.dist), "Distance", handlesStyle);
            Handles.Label(camTranform.position + (Vector3.up * targetCam.height), "Height", handlesStyle);

        }


    }
}
