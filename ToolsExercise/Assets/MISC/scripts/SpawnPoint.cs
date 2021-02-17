using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityTools.SpawnPoint
{
    public class SpawnPoint : MonoBehaviour
    {
        public GameObject spawnObject;
        private int spawnCount = 0;
        [SerializeField] private float radius = 5f;

        private void Update()
        {
            HandleSpawn();
        }

        private void HandleSpawn()
        {
            if (spawnCount < 1)
            {
                Instantiate(spawnObject, transform.position, Quaternion.identity);
                spawnCount++;
            }
            else
            {
                return;
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 1f, 0f, .5f);
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}

