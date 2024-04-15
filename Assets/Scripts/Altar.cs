using UnityEngine;

namespace LD55
{
    public class Altar : MonoBehaviour
    {
        #region Inspector

        [SerializeField] Color _InsideColor = Color.white;

        [SerializeField] Color _ActiveColor = Color.white;

        [SerializeField] Ingredient[] _Ingredients;

        [SerializeField] Transform _SpellBarLocation;

        [SerializeField] float _ActivationDuration = 3f;

        #endregion

        Color _defaultColor;

        Renderer _renderer;

        bool _activated = false;

        public bool IsActivated => _activated;

        public Ingredient[] Ingredients
        {
            get
            {
                return _Ingredients;
            }

            set
            {
                _Ingredients = value;
            }
        }

        SpellBar _spellBar;

        float _activateTimer = 0;

        void Start()
        {
            _renderer = GetComponentInChildren<Renderer>();
            _defaultColor = _renderer.material.color;
        }

        public void Activate()
        {
            if (_spellBar == null)
            {
                _spellBar = SpellBarManager.Instance.GetSpellBar();
            }

            _spellBar.Attach(_SpellBarLocation);
            _spellBar.Value = _activateTimer / _ActivationDuration;

            _activateTimer += Time.deltaTime;

            if (_activateTimer > _ActivationDuration)
            {
                SpellBarManager.Instance.ReleaseSpellBar(_spellBar);

                _activated = true;
                _renderer.material.color = _ActiveColor;

                Ghost.Create(transform.position);
            }            
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
