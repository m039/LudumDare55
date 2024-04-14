using UnityEngine;

namespace LD55
{
    public class Ghost : MonoBehaviour
    {
        #region Inspector

        [SerializeField] Transform _Visual;

        [SerializeField] float _Cooldown = 1;

        #endregion

        EnemyZone _enemyZone;

        float _cooldownTimer = 0;

        public static Ghost Create(Vector3 position)
        {
            var ghost = Instantiate(GameConfig.Instance.ghostPrefab, position, Quaternion.identity);
            return ghost;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<EnemyZone>() is EnemyZone enemyZone) {
                _enemyZone = enemyZone;
            }
        }

        void Update()
        {
            ProcessEnemies();
        }

        void ProcessEnemies()
        {
            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer < _Cooldown)
                return;

            _cooldownTimer = 0;

            if (_enemyZone == null)
                return;

            foreach (var enemy in _enemyZone.Enemies)
            {
                if (enemy == null)
                    continue;

                var direction = enemy.transform.position - transform.position;
                direction.Normalize();
                _Visual.rotation = Quaternion.LookRotation(direction, Vector3.up);
                Projectile.Create(transform.position, direction);
                return;
            }
        }
    }
}
