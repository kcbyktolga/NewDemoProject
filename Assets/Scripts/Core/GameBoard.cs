using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoProject
{
    public class GameBoard : MonoBehaviour
    {
        #region Fields        
        [SerializeField]
        private Transform _navMeshFloor;
        private Vector2 _mapSize;

        #endregion

        #region Collections
        [SerializeField]
        private Color[] tileColors;
        private readonly List<Tile> _allTiles = new List<Tile>();
        #endregion

        #region Methods
        void Start()
        {
            _mapSize = GameManager.MapSize;

            CreateBoard();
            GameManager.CheckIfShapeCanBePlaced += CheckIfShapeCanBePlaced;
        }
        private void CreateBoard()
        {
            Transform boardHolder = new GameObject("Board").transform;
            boardHolder.parent = transform;

            int index = 0;
            for (int x = 0; x < _mapSize.x; x++)
            {
                for (int y = 0; y < _mapSize.y; y++)
                {
                    Vector3 tilePosition = new Vector3(-_mapSize.x / 2 + 0.5f + x, 0,-_mapSize.y / 2 + 0.5f + y);
                    Tile tile = Instantiate(GameManager.GetTilePrefab(),tilePosition, Quaternion.Euler(Vector3.right * 90), boardHolder);
                    tile.TileIndex = index;
                    tile.TileType = TileType.GridTile;
                    tile.SetColor(tileColors[index%2==0 ? 1 : 0]);
                    index++;
                    _allTiles.Add(tile);
                }
            }

            _navMeshFloor.localScale = new Vector3(_mapSize.x,1,_mapSize.y);
            transform.eulerAngles = GameManager.MapAngle;
            GameManager.GenerateNavMesh();
        }
        private void CheckIfShapeCanBePlaced(Shape currentShape)
        {
            var tileIndexes = new List<int>();
            var transforms = new List<Transform>();

            foreach (var tile in _allTiles)
            {
                if (!tile.IsFull && tile.IsSelect)
                {
                    tileIndexes.Add(tile.TileIndex);
                    transforms.Add(tile.transform);       
                }
            }

            if (currentShape.TotalTileCount == tileIndexes.Count)
            {               
                foreach (var index in tileIndexes)
                    _allTiles[index].PlaceShapeOnBoard(currentShape.Color, true);
                
                var _prefab = currentShape.SelectableObject;

                if (_prefab !=null)
                {
                    SelectableObject obj = Instantiate(_prefab,GetSpawnPoint(transforms), Quaternion.identity,transform);

                    obj.SetSelectableObject(currentShape.ShapeData);
                   // obj.transform.eulerAngles = transform.eulerAngles;
                }
            }
            else
            {
                GameManager.SetDefaulColor();
            }

            currentShape.SetShapeInactive();
        }


        private Vector3 GetSpawnPoint(List<Transform> transforms)
        {
            Vector3 _total = new Vector3();

            for (int i = 0; i < transforms.Count; i++)
                _total += transforms[i].position;
         
            Vector3 _result = _total / transforms.Count;
            _result.y = .5f;
            return _result;    
        }
        #endregion
    }

}

