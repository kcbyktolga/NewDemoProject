using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DemoProject
{
    public class ProductContainer : MonoBehaviour
    {
        #region Fields
        [SerializeField]
        private TouchButton _touchButton;
        #endregion

        #region Methods
        private void Start()
        {
            GenerateButton();
        }
        private void GenerateButton()
        {
            for (int i = 0; i < GameManager.GetProductCount(); i++)
            {
                ObjectData _objectData = GameManager.GetShapeData(i, out ProductionType type);

                if (type == ProductionType.None)
                    continue;

                TouchButton touchButton = Instantiate(_touchButton, transform.GetChild(0));
                touchButton.SetButtonInfo(_objectData);
                touchButton.OnClick(i,OnSelect);
            }
        }
        private void OnSelect(int index)
        {
            GameManager.AllDeselect();
            GameManager.SetShapeData(index);
        }
    
        #endregion

    }
}

