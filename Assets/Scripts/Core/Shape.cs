using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DemoProject
{
    public class Shape : MonoBehaviour
    {
        #region Fields    
        private float _mouseCoordZ;
        private Vector3 _mouseOffset;
        private BoxCollider _collider;
        private Vector2 _originalPosition;
        private Color _currentColor;
        private SelectableObject _selectableObject;
        private ShapeData _currentShapeData;
        #endregion

        #region Collections
        private readonly List<Tile> allTiles = new List<Tile>();
        #endregion

        #region Properties
        public int TotalTileCount { get; set; }
        public Color Color { get { return _currentColor; } }
        public ShapeData ShapeData { get { return _currentShapeData; } }

        public SelectableObject SelectableObject { get { return _selectableObject; }}

        #endregion

        #region Methods
        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();     
        }
        private void Start()
        {
            CreateShape();
            GameManager.SetShape += SetShape;
        }

        private void CreateShape()
        {
            for (int i = 0; i < GameManager.MaxTileCountOnShape(); i++)
            {
                Tile tile = Instantiate(GameManager.GetTilePrefab(),Vector3.zero,Quaternion.Euler(Vector3.right*90), transform);
                tile.SetSortingLayer("Shape");
                tile.TileType = TileType.ShapeTile;
                allTiles.Add(tile);
                tile.transform.localPosition = Vector3.zero;
                tile.gameObject.SetActive(false);
            }
            transform.eulerAngles = GameManager.MapAngle;
        }
        private void SetShape(ShapeData shapeData)
        {
            SetShapeInactive();
            _currentShapeData = shapeData;
            TotalTileCount = GetTileCount(shapeData);
            _selectableObject = shapeData.selectableObject;

            for (int i = 0; i < allTiles.Count; i++)
            {
                allTiles[i].transform.localPosition = Vector3.zero;
                _currentColor = shapeData.color;
                Color _color = shapeData.color;
                _color.a = 0.5f;
                allTiles[i].SetColor(_color);
            }

            var distance = Vector2.one;

            int index = 0;

            for (int row = 0; row < shapeData.rows; row++)
                for (int column = 0; column < shapeData.columns; column++)
                    if (shapeData.board[row].column[column])
                    {                      
                        allTiles[index].gameObject.SetActive(true);
                        allTiles[index].transform.localPosition = GetTilePosition(shapeData,new Vector2(column,row),distance);

                        index++;
                    }

            _collider.size = new Vector3(shapeData.columns,1,shapeData.rows);
        }
        public void SetShapeInactive()
        {
            foreach (var tile in allTiles)
                tile.gameObject.SetActive(false);

            transform.localPosition = _originalPosition;
        }

        private Vector3 GetTilePosition(ShapeData shapeData, Vector2 size, Vector2 distance)
        {
            float shiftOnX = 0f;
            if (shapeData.columns > 1)
            {
                float startXPos;
                if (shapeData.columns % 2 != 0)
                    startXPos = (shapeData.columns / 2) * distance.x * -1;
                else
                    startXPos = ((shapeData.columns / 2) - 1) * distance.x * -1 - distance.x / 2;
                shiftOnX = startXPos + size.x * distance.x;

            }
       
            float shiftOnY = 0f;
            if (shapeData.rows > 1)
            {
                float startYPos;
                if (shapeData.rows % 2 != 0)
                    startYPos = (shapeData.rows / 2) * distance.y;
                else
                    startYPos = ((shapeData.rows / 2) - 1) * distance.y + distance.y / 2;
                shiftOnY = startYPos - size.y * distance.y;
            }

            return new Vector3(shiftOnX,0,shiftOnY);
        }
        private Vector3 GetMouseWorldPos()
        {          
            Vector3 mousePoint = Input.mousePosition;
         
            mousePoint.z = _mouseCoordZ;

            Vector3 pos = GameManager.Camera.ScreenToWorldPoint(mousePoint);

            return new Vector3(pos.x,transform.position.y,pos.z);
        }
        private int GetTileCount(ShapeData shapeData)
        {
            int count = 0;

            foreach (var rowData in shapeData.board)
                foreach (var active in rowData.column)
                    if (active)
                        count++;

            return count;
        }
        private void OnMouseDown()
        {
            _mouseCoordZ = GameManager.Camera.WorldToScreenPoint(transform.position).z;
        }
        private void OnMouseDrag()
        {
            Vector3 currentPos = GetMouseWorldPos() + _mouseOffset;
            transform.DOMove(currentPos, .25f);
        }
        private void OnMouseUp()
        {
            GameManager.CheckIfShapeCanBePlaced(this);
        }
        #endregion
    }
}

