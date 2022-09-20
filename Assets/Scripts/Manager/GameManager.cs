using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace DemoProject
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        private static GameManager current;
        private void Awake()
        {
            if (current != null && current != this)
            {
                Destroy(gameObject);
                return;
            }

            current = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        #region Collections
        [SerializeField]
        private List<ShapeData> shapeDatas;
        [SerializeField]
        private List<Color> colors;

        #endregion

        #region Fields
        [SerializeField]
        private Sprite _pointIcon;
        [SerializeField]
        private Tile _tilePrefab;
        [SerializeField]
        private Vector2 _mapSize;
        [SerializeField]
        private Vector3 _mapAngle;
        #endregion

        #region Properties
        public static Camera Camera { get { return Camera.main; } }
        public static Sprite PointSprite { get { return current._pointIcon; } }
        public static Vector2 MapSize { get { return current._mapSize; } }
        public static Vector3 MapAngle { get { return current._mapAngle; } }

        #endregion

        #region Methods
        public static int MaxTileCountOnShape()
        {
            int count = current.shapeDatas.Count;
            int[] tileCounts = new int[count];

            for (int i = 0; i < count; i++)
                tileCounts[i] = current.shapeDatas[i].TileCount;

            Array.Sort(tileCounts);
            return tileCounts[tileCounts.Length - 1];
        }
        public static int GetProductCount()
        {
            return current.shapeDatas.Count;
        }
        public static Tile GetTilePrefab()
        {
            return current._tilePrefab;
        }
        public static ObjectData GetShapeData(int index, out ProductionType type)
        {
            ObjectData _objectData = current.shapeDatas[index].selectableObject.Data;
            type = _objectData.productionType;
            return _objectData;
        }
        public static void SetShapeData(int index)
        {
            SetShape(current.shapeDatas[index]);
        }
        public static Color GetColor(bool isOn)
        {
            int index = isOn ? 1 : 0;
            return current.colors[index];
        }
        public static bool IsTouchUI()
        {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_WIN)
return Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject( Input.GetTouch( 0 ).fingerId );
#else
            return EventSystem.current.IsPointerOverGameObject();
#endif
        }
        #endregion

        #region Events
        public static Action<ShapeData> SetShape;
        public static Action<Shape> CheckIfShapeCanBePlaced;
        public static Action<ObjectData, bool> ShowInformation;
        public static Action<SelectableObject, Color> SetSelectableObject;
        public static Action GenerateNavMesh;
        public static Action<SelectableBase> OnFocus;
        public static Action SetDefaulColor;
        public static Action<SelectableObject> SpawnProudct;
        public static Action AllDeselect;

        
        #endregion
    }

    #region Enums
    public enum TileType
    {
        GridTile,
        ShapeTile
    }
    public enum ProductionType
    {
        None,
        Solider,
        Energy
    }
    #endregion
}

