using UnityEngine;
using UnityEditor;

namespace DemoProject
{
    [CustomEditor(typeof(ShapeData))]
    [CanEditMultipleObjects]
    [System.Serializable]
    public class ShapeEditor : Editor
    {
        #region Properties
        private ShapeData ShapeData => target as ShapeData;
        #endregion

        #region Methods
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            ClearBoardButton();
            EditorGUILayout.Space();

            DrawColumnsInputFields();
            EditorGUILayout.Space();

            if (ShapeData.board != null && ShapeData.columns > 0 && ShapeData.rows > 0)
                DrawBoardTable();
            serializedObject.ApplyModifiedProperties();
            if (GUI.changed)
                EditorUtility.SetDirty(ShapeData);
        }
        private void ClearBoardButton()
        {
            if (GUILayout.Button("Clear Board"))
                ShapeData.Clear();
        }
        private void DrawColumnsInputFields()
        {
            var columsTemp = ShapeData.columns;
            var rowsTemp = ShapeData.rows;
            var colorTemp = ShapeData.color;

            ShapeData.columns = EditorGUILayout.IntField("Colums", ShapeData.columns);
            ShapeData.rows = EditorGUILayout.IntField("Rows", ShapeData.rows);
            ShapeData.color = EditorGUILayout.ColorField("Color", ShapeData.color);
            ShapeData.initScale = EditorGUILayout.FloatField("InitScale", ShapeData.initScale);
            ShapeData.selectableObject = (SelectableObject)EditorGUILayout.ObjectField("Selectable Object", ShapeData.selectableObject, typeof(SelectableObject), true);

            if ((ShapeData.columns != columsTemp || ShapeData.rows != rowsTemp) && (ShapeData.columns > 0 || ShapeData.rows > 0))
                ShapeData.CreateNewBoard();


        }
        private void DrawBoardTable()
        {
            var tableStyle = new GUIStyle("box");
            tableStyle.padding = new RectOffset(10, 10, 10, 10);
            tableStyle.margin.left = 32;

            var headerColumnStyle = new GUIStyle();
            headerColumnStyle.fixedWidth = 65;
            headerColumnStyle.alignment = TextAnchor.MiddleCenter;

            var rowStyle = new GUIStyle();
            rowStyle.fixedHeight = 25f;
            rowStyle.alignment = TextAnchor.MiddleCenter;

            var dataFieldStyle = new GUIStyle(EditorStyles.miniButtonMid);
            dataFieldStyle.normal.background = Texture2D.grayTexture;
            dataFieldStyle.onNormal.background = Texture2D.whiteTexture;

            for (int row = 0; row < ShapeData.rows; row++)
            {
                EditorGUILayout.BeginHorizontal(headerColumnStyle);

                for (int column = 0; column < ShapeData.columns; column++)
                {
                    EditorGUILayout.BeginHorizontal(rowStyle);
                    var data = EditorGUILayout.Toggle(ShapeData.board[row].column[column], dataFieldStyle);
                    ShapeData.board[row].column[column] = data;
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndHorizontal();
            }

        }
        #endregion
       
        

    }
}

