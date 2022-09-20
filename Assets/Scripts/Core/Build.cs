using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoProject
{
    public class Build : SelectableObject
    {
        #region Fields
        [SerializeField]
        private Transform _spawnT;
        #endregion

        #region Methods
        protected override void Awake()
        {
            base.Awake();
            GameManager.SpawnProudct += SpawnnProduct;
        }
        public override void SetSelectableObject(ShapeData shapeData)
        {
            base.SetSelectableObject(shapeData);

            Transform parent = transform.parent;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, parent.eulerAngles.y, transform.eulerAngles.z);
            Vector3 _size = new Vector3(shapeData.columns, 1, shapeData.rows);
            // _collider.size = _size;
            transform.localScale = .75f * _size;

            if (!shapeData.selectableObject.GetType().Equals(typeof(Solider)))
                GameManager.GenerateNavMesh();

        }
        public virtual void SpawnnProduct(SelectableObject obj)
        {
            if (!_isOn)
                return;

            SelectableObject _obj = Instantiate(obj, _spawnT.position, Quaternion.identity);
            _obj.transform.parent = transform;
        }
        #endregion

    }
}

