using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace DemoProject
{
    public class NavMeshBaker : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private NavMeshSurface surface;
        #endregion

        #region Methods
        private void Start()
        {
            GameManager.GenerateNavMesh += Generate;
        }
        private void Generate()
        {
            surface.BuildNavMesh();
        }
        #endregion


    }
}

