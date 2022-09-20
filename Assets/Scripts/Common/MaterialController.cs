using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoProject
{
    public class MaterialController : MonoBehaviour
    {
        #region Fields
        private Renderer _renderer;
        private MaterialPropertyBlock _mpb;
        #endregion

        #region Methods
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _mpb = new MaterialPropertyBlock();
        }

        public void SetColor(Color color)
        {
            _mpb.SetColor("_Color", color);
            _renderer.SetPropertyBlock(_mpb);
        }
        #endregion

    }

}
