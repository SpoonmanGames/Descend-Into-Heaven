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

        [HideInInspector]
        public int Fase = 1;

        private Vector3 HandPositon;

        private Player _playerController;        

        private float _attackTimer = 0.0f;
        private float _attackTimerLimit = 1.0f;

        private float _waitTimer = 0.0f;
        private float _waitTimerLimit = 1.0f;

        private int _roundCounter = 0;

        private Vector2 _spawnPosition;

        public void Hurt(int damage) {
            Life -= damage;
            Fase++;
            ChangePlayerState(PlayerState.Hurt);
        }

        override protected void Start() {
            base.Start();
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            } else if(!IsDead) {
                HandPositon = new Vector2(
                    this.transform.position.x - 0.392f,
                    this.transform.position.y + 0.311f
                );

                switch (Fase) {
                    case 1:
                        LateUpdateFase1();
                        break;
                    case 2:
                        if (MoveOutOfScene(1, 1.0f, 1.553f)) {
                            LateUpdateFase2();
                        }
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

        bool MoveOutOfScene(int direccion, float speed, float targetPosition) {
            float position = 0.0f;

            if (this.transform.position.x >= targetPosition) {
                return true;
            }

            _attackTimer = 0.0f;
            _waitTimer = 0.0f;

            position = this.transform.position.x;
            position += (speed * Time.deltaTime * direccion);
            this.transform.position = new Vector3(position, this.transform.position.y, this.transform.position.z);            

            return false;
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

                SpawnBulletInHand(HandPositon.x, HandPositon.y);
                _attackTimer = 0.0f;
            }
        }

        void LateUpdateFase2() {
            _waitTimer += Time.deltaTime;

            if (_waitTimer >= _waitTimerLimit) {
                _attackTimer += Time.deltaTime;

                if (_attackTimer >= _attackTimerLimit + 1.0f) {
                    if (_roundCounter == 0) {
                        HorizontalHoleShot(-1, 2);
                    } else if (_roundCounter == 1) {
                        VerticalHoleShot(-1, 1);
                    } else if (_roundCounter == 2) {
                        HorizontalHoleShot(1, 1);
                    } else if (_roundCounter == 3) {
                        VerticalHoleShot(1, 2);
                    }

                    _roundCounter++;
                    _roundCounter %= 4;



                    _attackTimer = 0.0f;
                }
            }
        }

        void HorizontalHoleShot(int direction, int hole) {
            float initialX;
            float finalX;
            float fixedY;

            if (direction == -1) {
                initialX = 1.256f;
                finalX = -1.437595f;
            } else {
                initialX = -1.437595f;
                finalX = 1.256f;
            }

            for (int i = 0; i < 5; i++) {
                fixedY = 0.8661139f - i * 0.4131139f;

                if (i != hole) {
                    SpawnBullet.GetComponent<BulletController>().Direccion = direction;
                    SpawnBullet.GetComponent<BulletController>().TargetPosition = new Vector2(finalX, fixedY);
                    SpawnBulletInHand(initialX, fixedY);
                }
            }
        }

        void VerticalHoleShot(int direction, int hole) {
            float initialY;
            float finalY;
            float fixedX;

            if (direction == -1) {
                initialY = 1.15f;
                finalY = -1.158f;
            } else {
                initialY = -1.158f;
                finalY = 1.15f;
            }

            for (int i = 0; i < 4; i++) {
                fixedX = -0.701f - i * -0.466f;

                if (i != hole) {
                    SpawnBullet.GetComponent<BulletController>().Direccion = direction;
                    SpawnBullet.GetComponent<BulletController>().TargetPosition = new Vector2(fixedX, finalY);
                    SpawnBulletInHand(fixedX, initialY);
                }
            }
        }

        void LateUpdateFase3() {

        }

        void LateUpdateFase4() {

        }

        void LateUpdateFase5() {

        }

        void SpawnBulletInHand(float position_x, float posittion_y) {
            _spawnPosition = new Vector3(
                position_x,
                posittion_y,
                this.transform.position.z
            );

            Instantiate(SpawnBullet, _spawnPosition, this.transform.rotation);
        }

        void Update() {

        }

    }
}
