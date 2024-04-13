using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{

    public class Player : MonoBehaviour
    {
        #region Inspector

        [SerializeField] float _Speed = 5f;

        [SerializeField] Transform _Visual;

        #endregion

        readonly PlayerInput _input = new();

        Altar _activeObject;
        
        void Update()
        {
            ProcessInput();
        }

        void ProcessInput()
        {
            var move = _input.GetMove();
            if (move.sqrMagnitude > 0)
            {
                var offset = new Vector3(move.x, 0, move.y);
                _Visual.rotation = Quaternion.LookRotation(offset, Vector3.up);
                transform.position = transform.position + offset * _Speed * Time.deltaTime;
            }

            if (_activeObject != null &&
                _input.ActionKeyDown() &&
                !_activeObject.IsActivated)
            {
                _activeObject.Activate();
            }
        }

        public void OnEnter(Altar altar)
        {
            _activeObject = altar;
        }

        public void OnExit(Altar altar)
        {
            if (altar == _activeObject)
            {
                _activeObject = null;
            }
        }
    }

}
