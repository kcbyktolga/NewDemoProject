using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoProject
{
    public abstract class SelectableBase : MonoBehaviour
    {

        #region Fields
        [Header("Selectable Base")]
        [SerializeField]
        private ObjectData _data;

        protected bool _isOn;
        #endregion

        #region Properties
        public ObjectData Data { get { return _data; } protected set { _data = value; } }
       
        #endregion

        #region Methods
        protected virtual void Start()
        {
            GameManager.OnFocus += Focus;
            GameManager.AllDeselect += Deselect;
        }
        protected virtual void SetData(ObjectData data)
        {
            _data = data;
        }
        protected virtual void OnMouseDown()
        {
            if (GameManager.IsTouchUI())
                return;

            OnFocus();
        }
        protected virtual void Deselect()
        {
            _isOn = false;
        }
        protected virtual void OnFocus()
        {
            _isOn = !_isOn;
            GameManager.OnFocus(this);
            
        }
        protected virtual void Focus(SelectableBase _base)
        {         
            if (_base != this)
                if (_isOn)
                    _isOn = false;

            GameManager.ShowInformation(_base._data, _base._isOn);
 
        }
   
        #endregion

    }
}

