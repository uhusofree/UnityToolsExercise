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

        public GameObject spawnPoint;
        private Vector2[] pointsOnLine;

        SerializedObject so;
        SerializedProperty spRadius;
        SerializedProperty spSpawnerCount;
        SerializedProperty spPointsOnLine;
        SerializedProperty spSpawnPoint;

        public static void LaunchWindow()
        {
            EditorWindow editorWindow = (Editor_SpawnPoint)EditorWindow.GetWindow(typeof(Editor_SpawnPoint));
            editorWindow.Show();
        }

        private void OnEnable()
        {
            so = new SerializedObject(this);
            spRadius = so.FindProperty("radius");
            spSpawnerCount = so.FindProperty("spawnerCount");
            spPointsOnLine = so.FindProperty("pointsOnLine");
            spSpawnPoint = so.FindProperty("spawnPoint");

            GenerateRandomPoints();
            SceneView.duringSceneGui += RunDuringSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= RunDuringSceneGUI;
        }

        private void GenerateRandomPoints()

        {
            float diameter = 2 * radius;
            float circumfernce = Mathf.PI * diameter;
            float xRef = 0f;
            float yRef = 0f;

            pointsOnLine = new Vector2[spawnerCount];
            for (int i = 0; i < spawnerCount; i++)
            {
                //pointsOnLine[i] += new Vector2(xRef, yRef);
                pointsOnLine[i] += Vector2.ClampMagnitude(new Vector2(xRef - radius * .5f, yRef - radius * .5f), diameter);
                //pointsOnLine[i] += new Vector2(xRef - radius * .5f, yRef - radius * .5f);
                xRef += 1;
                yRef += 1;

                //Debug.Log(pointsOnLine[i].x + " " + pointsOnLine[i].y);


                /**float xRand = 1;
                 float yRand = 1;
                 Vector2 randomizePointsOffset = new Vector2(xRand, yRand);
                 randomizePointsOffset *= 4.5f;
                 pointsOnLine[i] = Random.insideUnitSphere * randomizePointsOffset;*/
            }
        }

        private void DrawPointsInToolSphere(Vector3 pos)
        {
            Handles.SphereHandleCap(-1, pos, Quaternion.identity, 0.50f, EventType.Repaint);
        }


        private void OnGUI()
        {
            so.Update();
            EditorGUILayout.LabelField("Spawn Point Tool", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("adjust radius to increase offsets between spawn points", EditorStyles.helpBox);

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

            EditorGUILayout.LabelField("Select or Drag in Spawn Prefab ", EditorStyles.helpBox);

            spSpawnPoint.FindPropertyRelative("spawnPoint");
            spawnPoint = (GameObject)EditorGUILayout.ObjectField("Spawn Point", spawnPoint, typeof(GameObject), true);

            Repaint();



            //click out field focus
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                GUI.FocusControl(null);
                Repaint();
            }
        }

        public void RunDuringSceneGUI(SceneView sceneView)
        {
            //var t = Matrix4x4.TRS(Vector2.zero, Quaternion.LookRotation(Vector2.right), Vector2.one);

            Transform camTransform = sceneView.camera.transform;

            if (Event.current.type == EventType.MouseMove)
            {
                sceneView.Repaint();
            }

            /** TODO: ROTATE POINTS ARRAY
             * if (Event.current.type == EventType.ScrollWheel)
             {
                 float scrollDir = Mathf.Sign(Event.current.delta.y);

                 so.Update();
                 spPointsOnLine.vector2Value = t.MultiplyPoint3x4(spPointsOnLine.vector2Value) * scrollDir;
             }*/

            //ray from mouse position
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 hitNormal = hit.normal;
                Vector3 tangent = Vector3.Cross(hitNormal, camTransform.up).normalized;
                Vector3 biTangent = Vector3.Cross(hitNormal, tangent);

                foreach (var point in pointsOnLine)
                {
                    Vector3 pointOffset = new Vector3(0, .5f);/**offset from ground*/

                    Vector3 worldPos = hit.point + (tangent * point.x + biTangent * point.y) * radius;
                    worldPos += hitNormal * 2;
                    Vector3 rayDir = -hitNormal;
                    Ray pointsRay = new Ray(worldPos, rayDir);

                    if (Physics.Raycast(pointsRay, out RaycastHit pointHit))
                    {
                        DrawPointsInToolSphere(pointHit.point);

                        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
                        {
                            Instantiate(spawnPoint, pointHit.point + pointOffset, Quaternion.identity);
                        }
                    }

                }

                Handles.color = new Color(0f, 0f, 1f, 1f);
                Handles.DrawAAPolyLine(10f, hit.point, hit.point + hit.normal);
                Handles.color = new Color(1f, 0f, 0f, 1f);
                Handles.DrawAAPolyLine(10f, hit.point, hit.point + tangent);
                Handles.color = new Color(0f, 1f, 0f, 1f);
                Handles.DrawAAPolyLine(10f, hit.point, hit.point + biTangent);
                Handles.color = Color.white;
                Handles.DrawWireDisc(hit.point, hit.normal, radius);
            }
        }


    }
}
