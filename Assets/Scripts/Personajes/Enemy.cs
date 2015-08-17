using UnityEngine;
using System.Collections;

namespace Player {
    public class Enemy : Player {

        [Header("Boss Intro")]
        public float Velocity = 1f;
        public float PuntoFinal = 0f;
        public float PuntoInicial = 0f;
        [Space(10)]
        public GameObject SpawnBullet;

        private Player _playerController;
        private int _fase = 1;

        private float _attackTimer = 0.0f;
        private float _attackTimerLimit = 1.0f;

        private Vector3 _spawnPosition;

        public void Hurt(int damage) {
            Life -= damage;
            ChangePlayerState(PlayerState.Hurt);
        }

        override protected void Start() {
            base.Start();
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            SpawnBullet.GetComponent<BulletController>().PlayerController = _playerController;
        }

        void LateUpdate() {
            if (!IsFreeToMove) {
                float newPosition;

                newPosition = this.transform.position.x;

                newPosition += (Velocity * Time.deltaTime * -1);
                newPosition = Mathf.Clamp(newPosition, PuntoFinal, PuntoInicial);

                this.transform.position = new Vector3(newPosition, this.transform.position.y, this.transform.position.z);

                if (this.transform.position.x == PuntoFinal) {
                    IsFreeToMove = true;
                }
            } else {
                switch (_fase) {
                    case 1:
                        LateUpdateFase1();
                        break;
                    case 2:
                        LateUpdateFase2();
                        break;
                    case 3:
                        LateUpdateFase3();
                        break;
                    case 4:
                        LateUpdateFase4();
                        break;
                    case 5:
                        LateUpdateFase5();
                        break;
                }
            }
        }

        void LateUpdateFase1() {
            _attackTimer += Time.deltaTime;

            if (_attackTimer >= _attackTimerLimit) {

                ChangePlayerState(PlayerState.Attacking);

                SpawnBullet.GetComponent<BulletController>().TargetPosition = 
                    new Vector2(
                        _playerController.transform.position.x, 
                        _playerController.transform.position.y + 0.12f
                    );

                SpawnBulletInHand();
                _attackTimer = 0.0f;
            }
        }

        void LateUpdateFase2() {

        }

        void LateUpdateFase3() {

        }

        void LateUpdateFase4() {

        }

        void LateUpdateFase5() {

        }

        void SpawnBulletInHand() {
            _spawnPosition = new Vector3(
                this.transform.position.x - 0.392f,
                this.transform.position.y + 0.311f,
                this.transform.position.z
            );

            Instantiate(SpawnBullet, _spawnPosition, this.transform.rotation);
        }

        void Update() {

        }

    }
}
