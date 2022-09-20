using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace DemoProject
{
    public class InformationContainer : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _name;
        [SerializeField]
        private TMP_Text _description;
        [SerializeField]
        private GameObject _productionPanel;
        //[SerializeField]
        //private TMP_Text _productName;
        //[SerializeField]
        //private Image _productIcon;
        [SerializeField]
        private TouchButton _productButton;
        [SerializeField]
        private RectTransform _basePanel;
        [SerializeField]
        private HorizontalOrVerticalLayoutGroup _layout;
        #endregion

        #region Methods
        private void Start()
        {
            _basePanel.gameObject.SetActive(false);
            GameManager.ShowInformation += ShowInformation;
            GameManager.AllDeselect += Deselect;
        }
        private void ShowInformation(ObjectData data, bool isOn)
        {
            _basePanel.gameObject.SetActive(isOn);

            _icon.sprite = data.icon;
            _name.text = data.name;
            _description.text = data.description;

            var _product = data.GetProduct();

            _basePanel.DOKill();
            _basePanel.DOScaleY(1.01f, .1f).SetEase(Ease.OutElastic).OnComplete(() => _basePanel.DOScaleY(1, .1f).SetEase(Ease.OutElastic));

            if (_product != null)
            {
                _productButton.SetButtonInfo(_product.selectableObject.Data);
                _productButton.OnClick(_product.selectableObject, GameManager.SpawnProudct);
                _productionPanel.SetActive(true);
            }
            else
                _productionPanel.SetActive(false);

            // Bunu hiç yapmak istemezdim :(
            Canvas.ForceUpdateCanvases();
            _layout.enabled = false;
            Canvas.ForceUpdateCanvases();
            _layout.enabled = true;
            Canvas.ForceUpdateCanvases();

        }
        private void Deselect()
        {
            _basePanel.gameObject.SetActive(false);
        }
        #endregion
    }

}
