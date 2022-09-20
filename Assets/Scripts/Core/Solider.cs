using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DemoProject
{
    public class Solider : SelectableObject
    {
        #region Fields
        [SerializeField]
        private Transform _target;
        private NavMeshAgent _agent;
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();
            _agent = GetComponent<NavMeshAgent>();
        }
        public override void SetSelectableObject(ShapeData shapeData)
        {
            base.SetSelectableObject(shapeData);

            // transform.eulerAngles = new Vector3(0,0,0);
        }

        private void Update()
        {
            if (!_isOn && !_agent.isStopped)
            {
                _agent.isStopped = true;
                _agent.ResetPath();
            }

            if (Input.GetMouseButtonDown(1) && _isOn)
            {
                Ray ray = GameManager.Camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                    _agent.SetDestination(hit.point);
            }
        }
        #endregion

    }
}

