using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using System;
using TMPro;

namespace DemoProject
{
    public class TouchButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        #region Fields
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _buttonName;

        private Button button;
        private Vector2 originalScale;
        #endregion

        #region Methods
        private void Awake()
        {
            button = GetComponent<Button>();
            originalScale = transform.localScale;
        }

        // for Animation..
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.DOScale(originalScale * 0.9f, 0.25f);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            transform.DOScale(originalScale, 0.25f);
        }

        public void SetButtonInfo(ObjectData data)
        {
            _icon.sprite = data.icon;
            _buttonName.text = data.name;
        }
        public void OnClick(Action action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => action.Invoke());
        }
        public void OnClick<T>(T type, Action<T> action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => action.Invoke(type));
        }

        #endregion

    }
}

