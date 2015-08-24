using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Player {
    public class Boss : Player {

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
        private int _waveCounter = 0;
        private List<int> holeList = new List<int>();

        private Vector2 _spawnPosition;
        private bool _isOutofScene = false;
        private bool _isMovingIntoTheScene = false;

        public void Hurt(int damage) {            
            Life -= damage;
            Fase++;
            _isOutofScene = false;
            _isMovingIntoTheScene = false;
            ChangePlayerState(PlayerState.Hurt);
        }

        void Start() {
            this._currentDirection = "left";
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            SpawnBullet.GetComponent<BulletController>().Direccion = -1;
            SpawnBullet.GetComponent<BulletController>().Tiempo = 1.0f;
            SpawnBullet.GetComponent<ChangeByTime>().LifeTime = 0.9f;
            SpawnBullet.GetComponent<BulletController>().SetVelocity();
        }

        void LateUpdate() {
            if (!IsFreeToMove) {
                IsFreeToMove = MoveAroundTheScene(-1, Velocity, PuntoFinal);
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
                        if (!_isOutofScene) {
                            _isOutofScene = MoveAroundTheScene(1, 1.0f, 1.553f);
                        }
                        if(_isOutofScene){
                            LateUpdateFase2();
                        }                        
                        break;
                    case 3:
                        if (!_isOutofScene) {
                            _isOutofScene = MoveAroundTheScene(-1, 1.0f, -1.444f);
                        }
                        if (_isOutofScene) {
                            LateUpdateFase3();
                        }   
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

        bool MoveAroundTheScene(int direccion, float speed, float targetPosition) {
            float position = 0.0f;

            if (direccion == 1 && this.transform.position.x >= targetPosition) {
                return true;
            }

            if (direccion == -1 && this.transform.position.x <= targetPosition) {
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

                    ChangePlayerState(PlayerState.Attacking);

                    if (_roundCounter == 0) {
                        HorizontalHoleShot(-1, 1.0f, 0.8661139f, 0.4131139f, 5, 2);
                    } else if (_roundCounter == 1) {
                        VerticalHoleShot(-1, 1);
                    } else if (_roundCounter == 2) {
                        HorizontalHoleShot(1, 1.0f, 0.8661139f, 0.4131139f, 5, 1);
                    } else if (_roundCounter == 3) {
                        VerticalHoleShot(1, 2);
                    }

                    _roundCounter++;
                    _roundCounter %= 4;

                    if (_roundCounter == 0 && !_isMovingIntoTheScene) {
                        this.ChangePlayerDirection("right");
                        this.transform.position = new Vector3(-1.444f, -0.209f, this.transform.position.z);
                        _isMovingIntoTheScene = true;
                    }

                    _attackTimer = 0.0f;
                }
            }

            if (_isMovingIntoTheScene) {
                if (!MoveAroundTheScene(1, 1.0f, -0.91f)) {
                    _waitTimer = _waitTimerLimit;
                    _attackTimer = _attackTimerLimit;
                }
            }
        }

        void HorizontalHoleShot(int direction, float time, float initialY, float gap, int numberfShots, params int[] holes) {
            float initialX;
            float finalX;
            float fixedY;
            bool dontSpawn = false;

            if (direction == -1) {
                initialX = 1.256f;
                finalX = -1.437595f;
            } else {
                initialX = -1.437595f;
                finalX = 1.256f;
            }

            for (int i = 0; i < numberfShots; i++) {
                fixedY = initialY - i * gap;

                for (int j = 0; j < holes.Length; j++) {
                    if (holes[j] == i) {
                        dontSpawn = true;
                        break;
                    }   
                }

                if (!dontSpawn) {
                    SpawnBullet.GetComponent<BulletController>().Direccion = direction;
                    SpawnBullet.GetComponent<BulletController>().Tiempo = time;
                    SpawnBullet.GetComponent<ChangeByTime>().LifeTime = 0.6f;
                    SpawnBullet.GetComponent<BulletController>().TargetPosition = new Vector2(finalX, fixedY);
                    SpawnBullet.GetComponent<BulletController>().SetVelocity();
                    SpawnBulletInHand(initialX, fixedY);
                } else {
                    dontSpawn = false;
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
            float speed = 1.0f;

            _waitTimer += Time.deltaTime;

            if (_waitTimer >= _waitTimerLimit) {
                if (!_isMovingIntoTheScene) {
                    this.ChangePlayerDirection("left");
                    this.transform.position = new Vector3(1.553f, 0.264f, this.transform.position.z);
                    _isMovingIntoTheScene = true;
                    _roundCounter = 0;
                }
            }

            if (_isMovingIntoTheScene) {
                if (MoveAroundTheScene(-1, 1.0f, 0.982f)) {
                    _attackTimer += Time.deltaTime;

                    if(_attackTimer >= _attackTimerLimit){

                        ChangePlayerState(PlayerState.Attacking);

                        if (_waveCounter == 0) {
                            speed = 1.2f;
                            holeList.Clear();
                            holeList.Add(7);
                            holeList.Add(8);
                        } else if (_waveCounter == 1) {
                            speed = 1.1f;
                            holeList.Clear();
                            holeList.Add(4);
                            holeList.Add(5);
                            holeList.Add(6);
                            holeList.Add(7);
                        } else if (_waveCounter == 2) {
                            speed = 1.0f;
                            holeList.Clear();
                            holeList.Add(7);
                            holeList.Add(8);
                        } else if (_waveCounter == 3) {
                            speed = 0.9f;
                            holeList.Clear();
                            holeList.Add(4);
                            holeList.Add(5);
                            holeList.Add(6);
                            holeList.Add(7);
                        } else if (_waveCounter == 4) {
                            speed = 0.8f;
                            holeList.Clear();
                            holeList.Add(2);
                            holeList.Add(3);
                            holeList.Add(4);
                        } else if (_waveCounter == 5) {
                            speed = 0.9f;
                            holeList.Clear();
                            holeList.Add(5);
                            holeList.Add(6);
                            holeList.Add(7);
                        } else if (_waveCounter == 6) {
                            speed = 1.0f;
                            holeList.Clear();
                            holeList.Add(7);
                            holeList.Add(8);
                        } else if (_waveCounter == 7) {
                            speed = 0.9f;
                            holeList.Clear();
                            holeList.Add(5);
                            holeList.Add(6);
                            holeList.Add(7);
                        } else if (_waveCounter == 8) {
                            speed = 0.8f;
                            holeList.Clear();
                            holeList.Add(2);
                            holeList.Add(3);
                            holeList.Add(4);
                        } else if (_waveCounter == 9) {
                            speed = 0.7f;
                            holeList.Clear();
                            holeList.Add(0);
                            holeList.Add(1);
                            holeList.Add(2);
                            holeList.Add(3);
                        }

                        HorizontalHoleShot(
                                -1,
                                speed,
                                0.8661139f,
                                0.4131139f / 2,
                                13,
                                holeList.ToArray()
                            );

                        _roundCounter++;
                        _attackTimer = 0.7f;

                        if (_roundCounter % 7 == 0) {
                            _waveCounter++;
                            _attackTimer = 0.4f;

                            if (_waveCounter % 10 == 0) {
                                _waveCounter = 0;
                                _attackTimer = 0.0f;
                            }
                        }
                    }
                }
            }
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
    }
}
