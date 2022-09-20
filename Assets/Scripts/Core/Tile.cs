using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DemoProject
{
    public class Tile : SelectableBase
    {
        #region Fields
        private SpriteRenderer _renderer;
        private Color _originalColor;
        private Color _currentColor;
        #endregion

        #region Properties
        public TileType TileType { get; set; }
        public bool IsFull { get; set; }
        public bool IsSelect { get; set; }
        public int TileIndex { get; set; }
        #endregion

        #region Methdos
        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        protected override void Start()
        {
            base.Start();

            string _text = IsFull ? "Full" : "Empty";
            ObjectData data = new ObjectData()
            {
                
                description = $"Position:(x={transform.position.x},y={transform.position.z}), \n Status: {_text}",
                name = "Target",
                icon = GameManager.PointSprite,
                productionType = ProductionType.None
            };

            Data = data;

            GameManager.SetDefaulColor += SetDefaultColor;
        }

        protected override void Focus(SelectableBase _base)
        {
            base.Focus(_base);

            if (TileType != TileType.GridTile)
                return;

            Color _color = _isOn ? Color.blue : (IsFull ? _currentColor : _originalColor);
            _renderer.color = _color;
        }

        public void SetColor(Color color)
        {
            _originalColor = color;
            _renderer.color = color;
        }
        private void SetDefaultColor()
        {
            if (TileType != TileType.GridTile)
                return;

            Color _color = IsFull ? _currentColor : _originalColor;
            SetColor2(_color);
            IsSelect = false;
        }
        public void SetSortingLayer(string layerName)
        {
            _renderer.sortingLayerName = layerName;
        }
        public void PlaceShapeOnBoard(Color color, bool isOn)
        {
            if (isOn)
            {
                _renderer.color = color;
                _currentColor = color;
            }
            else
                _renderer.DOColor(color, 0.1f);

            IsFull = isOn;

        }
        private void SetColor2(Color color)
        {
            _renderer.DOColor(color, 0.1f);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (TileType != TileType.GridTile)
                return;
                
            SetColor2(GameManager.GetColor(IsFull));
            IsSelect = true;
        }

        private void OnTriggerExit(Collider other)
        {
           SetDefaultColor();        
        }

        #endregion

    }

}

