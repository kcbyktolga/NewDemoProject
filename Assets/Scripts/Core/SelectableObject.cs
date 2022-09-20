using UnityEngine;

namespace DemoProject
{
    [RequireComponent(typeof(MaterialController))]
    public abstract class SelectableObject : SelectableBase
    {
        #region Fields
        protected MaterialController _matController;
        protected BoxCollider _collider;
        #endregion

        #region Methods
        protected virtual void Awake()
        {
            _matController = GetComponent<MaterialController>();
            _collider = GetComponent<BoxCollider>();
        }
        public virtual void SetSelectableObject(ShapeData shapeData)
        {
            _matController.SetColor(shapeData.color);
        }   
        #endregion
    }

    [System.Serializable]
    public struct ObjectData
    {
        public ProductionType productionType;
        public string name;
        public string description;
        public Sprite icon;

        public ShapeData GetProduct()
        {
            string path = $"ScriptableObjects/{productionType}";
            switch (productionType)
            {
                default:
                case ProductionType.None:
                    return null;
                case ProductionType.Solider:

                    var result = Resources.Load<ShapeData>(path);

                    if (result != null)
                        return result;

                    return null; 
            }
        }

    }
}

