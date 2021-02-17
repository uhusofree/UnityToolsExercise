using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace UnityTools.SpawnPoint
{
    public class Editor_SpawnPoint : EditorWindow
    {
        public float radius = 2f;
        public int spawnerCount = 5;

        SerializedObject so;
        SerializedProperty spRadius;
        SerializedProperty spSpawnerCount;

        private Vector2[] randPoints;
        public static void LaunchWindow()
        {
            EditorWindow editorWindow = (Editor_SpawnPoint)EditorWindow.GetWindow(typeof(Editor_SpawnPoint), true, "Place Spawn");
        }

        private void OnEnable()
        {
            so = new SerializedObject(this);
            spRadius = so.FindProperty("radius");
            spSpawnerCount = so.FindProperty("spawnerCount");

            GenerateRandomPoints();
            SceneView.duringSceneGui += RunDuringSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= RunDuringSceneGUI;
        }

        private void GenerateRandomPoints()

        {
            float xref = 0f;
            float yref = 0f;
            randPoints = new Vector2[spawnerCount];
            for (int i = 0; i < spawnerCount; i++)
            {
                xref += i;
                yref += i;
                randPoints[i] += new Vector2(xref, yref);
                Debug.Log(randPoints[i].x + " " + randPoints[i].y);

                //float xRand = 1;
                //float yRand = 1;

                //Vector2 randomizePointsOffset = new Vector2(xRand, yRand);
                //randomizePointsOffset *= 4.5f;
                //randPoints[i] = Random.insideUnitSphere * randomizePointsOffset;
            }
        }

        private void DrawPointsInToolSphere(Vector3 pos)
        {
            Handles.SphereHandleCap(-1, pos, Quaternion.identity, 0.1f, EventType.Repaint);
        }


        private void OnGUI()
        {
            so.Update();
            EditorGUILayout.LabelField("My Test", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Increase radius with spawner count", EditorStyles.helpBox);

            spRadius.floatValue = Mathf.Max(1, spRadius.floatValue);
            EditorGUILayout.PropertyField(spRadius);
            spSpawnerCount.intValue = Mathf.Max(1, spSpawnerCount.intValue);
            spSpawnerCount.intValue = Mathf.Min(5, spSpawnerCount.intValue);
            EditorGUILayout.PropertyField(spSpawnerCount);
            if (so.ApplyModifiedProperties())
            {
                GenerateRandomPoints();
                SceneView.RepaintAll();
            }
            Repaint();
        }

        public void RunDuringSceneGUI(SceneView sceneView)
        {

            Transform camTransform = sceneView.camera.transform;
            Ray ray = new Ray(camTransform.position, camTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 hitNormal = hit.normal;
                Vector3 tangent = Vector3.Cross(hitNormal, camTransform.up).normalized;
                Vector3 biTangent = Vector3.Cross(hitNormal, tangent);

                foreach (var point in randPoints)
                {
                    Vector3 worldPos = hit.point + (tangent * point.x + biTangent * point.y);
                    worldPos += hitNormal * 2;
                    Vector3 rayDir = -hitNormal;
                    Ray pointsRay = new Ray(worldPos, rayDir);

                    if (Physics.Raycast(pointsRay, out RaycastHit pointHit))
                    {
                        DrawPointsInToolSphere(pointHit.point);
                    }

                }

                Handles.color = new Color(1f, 0f, 0f, 1f);
                Handles.DrawAAPolyLine(10f, hit.point, hit.point + hit.normal);
                Handles.color = new Color(0f, 1f, 0f, 1f);
                Handles.DrawAAPolyLine(10f, hit.point, hit.point + tangent);
                Handles.color = new Color(0f, 0f, 1f, 1f);
                Handles.DrawAAPolyLine(10f, hit.point, hit.point + biTangent);
                Handles.color = Color.white;
                Handles.DrawWireDisc(hit.point, hit.normal, radius);
            }
        }


    }
}
