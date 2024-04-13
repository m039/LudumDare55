using UnityEngine;

namespace LD55
{
    public class Altar : MonoBehaviour
    {
        #region Inspector

        [SerializeField] Color _InsideColor = Color.white;

        [SerializeField] Color _ActiveColor = Color.white;

        #endregion

        Color _defaultColor;

        Renderer _renderer;

        bool _activated = false;

        public bool IsActivated => _activated;

        void Start()
        {
            _renderer = GetComponentInChildren<Renderer>();
            _defaultColor = _renderer.material.color;
        }

        public void Activate()
        {
            _activated = true;
            _renderer.material.color = _ActiveColor;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<Player>() is Player player)
            {
                if (!_activated)
                {
                    _renderer.material.color = _InsideColor;
                }
                player.OnEnter(this);
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<Player>() is Player player)
            {
                if (!_activated)
                {
                    _renderer.material.color = _defaultColor;
                }
                player.OnExit(this);
            }
        }
    }
}
