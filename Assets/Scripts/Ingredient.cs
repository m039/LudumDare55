using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
    public class Ingredient : MonoBehaviour
    {
        #region Inspector

        [SerializeField] Color _Color = Color.white;

        #endregion

        public Color color => _Color;

        void Start()
        {
            var renderer = GetComponentInChildren<Renderer>();
            renderer.material.color = _Color;
        }

        public void Consume()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<Player>() is Player player)
            {
                player.OnEnter(this);
            }
        }
    }
}
