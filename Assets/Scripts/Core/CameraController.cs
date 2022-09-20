using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DemoProject
{
    public class CameraController : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private float _panSpeed = 20f;
        [SerializeField]
        private Vector2 panBorderTickness;
        [SerializeField]
        private Vector2 _panMultiple;
        [SerializeField]
        private bool _mouseDrag;
        private Vector3 _pos;

        private const float _duration = .2f;

        #endregion

        #region Methods
   
        private void Update()
        {          
            Pan();
        }
  
        private void Pan()
        {
            _pos = transform.position;

            if (Input.GetKey(KeyCode.W) || (_mouseDrag && Input.mousePosition.y >= Screen.height - panBorderTickness.y))
            {
                _pos.z += _panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S) || (_mouseDrag && Input.mousePosition.y <= panBorderTickness.y))
            {
                _pos.z -= _panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D) || (_mouseDrag && Input.mousePosition.x >= Screen.width - panBorderTickness.x))
            {
                _pos.x += _panSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A) || (_mouseDrag && Input.mousePosition.x <= panBorderTickness.x))
            {
                _pos.x -= _panSpeed * Time.deltaTime;
            }

            _pos.x = Mathf.Clamp(_pos.x, -PanLimit().x, PanLimit().x);
            _pos.z = Mathf.Clamp(_pos.z, -PanLimit().y, PanLimit().y);

            //transform.position = _pos;
        }
        private void LateUpdate()
        {
            transform.DOMove(_pos, _duration);
        }
        private Vector2 PanLimit()
        {
            return new Vector2(_panMultiple.x * GameManager.MapSize.x, _panMultiple.y * GameManager.MapSize.y);
        }
        #endregion

    }
}

