using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace UnityTools.SpawnPoint
{
    public class Menu_SpawnPoint
    {
        [MenuItem("Uhuru's Tool/Object Placer")]

        public static void PlaceSpawnPoint()
        {
            Editor_SpawnPoint.LaunchWindow();
        }
    }
}

