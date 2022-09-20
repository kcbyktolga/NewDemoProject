using UnityEngine;

namespace DemoProject
{
    [CreateAssetMenu(menuName = "Data/Shape Data", fileName = "Shape Data")]
    public class ShapeData : ScriptableObject
    {
        #region Fields
        public int columns = 0;
        public int rows = 0;
        public Color color;
        public float initScale = 0.5f;
        public SelectableObject selectableObject;
        #endregion

        #region Collections
        public Row[] board;
        #endregion

        #region Propertis
        public int TileCount { get { return rows * columns; } }
        #endregion

        #region Methods
        public void Clear()
        {
            for (int i = 0; i < rows; i++)
                board[i].ClearRow();
        }
        public void CreateNewBoard()
        {
            board = new Row[rows];
            for (int i = 0; i < rows; i++)
                board[i] = new Row(columns);
        }
        #endregion

    }

    [System.Serializable]
    public class Row
    {
        public bool[] column;
        private int size = 0;

        public Row() { }
        public Row(int size)
        {
            CreatRow(size);
        }

        public void CreatRow(int size)
        {
            this.size = size;
            column = new bool[this.size];
            ClearRow();
        }

        public void ClearRow()
        {
            for (int i = 0; i < size; i++)
                column[i] = false;
        }
    }
}

