using UnityEngine;
using System.Collections;

namespace Player {

    public class CrossEnemy : Player {

        [Header("Cross Setup")]
        [SerializeField] private Player PlayerController;
        [SerializeField] private GameObject BulletToSpawn;
        public float AttackDelay = 1.0f;
        public bool ShootAlways = false;
        [SerializeField] private Bounds DetectionBlock;
        [SerializeField] private Bounds ShootRangeBlock;
        public bool ShootAtTarget = false;
        public bool ChooseShootDirections = false;
        public bool ShootLeft = false;
        public bool ShootUp = false;
        public bool ShootRight = false;

        private Bounds _realDetectionBlock;
        private Bounds _realShootRangeBlock;
        private Vector3 _leftSpawnPosition = new Vector3(-0.2283f, -0.1545f);
        private Vector3 _upSpawnPosition = new Vector3(0.0f, 0.0141f);
        private Vector3 _rightSpawnPosition = new Vector3(0.2283f, -0.1545f);
        private float _attackDelayCounter = 0.0f;

        private GameObject _BulletToSpawn;

        protected override void Awake() {
            base.Awake();

            ChangePlayerState(PlayerState.Idle);
        }

        protected override void Update() {
            base.Update();

            if (EditorDebugMode) {
                if (!ShootAlways) {
                    DebugExtension.DebugBounds(_realDetectionBlock, Color.red);
                }
                DebugExtension.DebugBounds(_realShootRangeBlock, Color.green);
            }

            _realDetectionBlock = DetectionBlock;
            _realDetectionBlock.center = DetectionBlock.center + this.transform.position;
            _realShootRangeBlock = ShootRangeBlock;
            _realShootRangeBlock.center = ShootRangeBlock.center + this.transform.position;

            if (!IsDead && (ShootAlways || _realDetectionBlock.Contains(PlayerController.transform.position)) ) {

                _attackDelayCounter += Time.deltaTime;

                if (_attackDelayCounter >= AttackDelay) {

                    _attackDelayCounter = 0;
                    ChangePlayerState(PlayerState.Attacking);

                    if (ShootAtTarget) {
                        _BulletToSpawn = Instantiate(BulletToSpawn, this.transform.position + _upSpawnPosition, this.transform.rotation) as GameObject;
                        _BulletToSpawn.GetComponent<BulletController>().TargetPosition = PlayerController.transform.position;
                        _BulletToSpawn.GetComponent<BulletController>().Direccion = -1;
                        _BulletToSpawn.GetComponent<BulletController>().SetVelocity();
                    } else {
                        if (!ChooseShootDirections || ShootLeft) {
                            _BulletToSpawn = Instantiate(BulletToSpawn, this.transform.position + _leftSpawnPosition, this.transform.rotation) as GameObject;
                            _BulletToSpawn.GetComponent<BulletController>().TargetPosition = new Vector2(_realShootRangeBlock.min.x, _BulletToSpawn.transform.position.y);
                            _BulletToSpawn.GetComponent<BulletController>().SetVelocity();
                        }

                        if (!ChooseShootDirections || ShootUp) {
                            _BulletToSpawn = Instantiate(BulletToSpawn, this.transform.position + _upSpawnPosition, this.transform.rotation) as GameObject;
                            _BulletToSpawn.GetComponent<BulletController>().TargetPosition = new Vector2(_BulletToSpawn.transform.position.x, _realShootRangeBlock.max.y);
                            _BulletToSpawn.GetComponent<BulletController>().Direccion = 1;
                            _BulletToSpawn.GetComponent<BulletController>().SetVelocity();
                        }

                        if (!ChooseShootDirections || ShootRight) {
                            _BulletToSpawn = Instantiate(BulletToSpawn, this.transform.position + _rightSpawnPosition, this.transform.rotation) as GameObject;
                            _BulletToSpawn.GetComponent<BulletController>().TargetPosition = new Vector2(_realShootRangeBlock.max.x, _BulletToSpawn.transform.position.y);
                            _BulletToSpawn.GetComponent<BulletController>().Direccion = 1;
                            _BulletToSpawn.GetComponent<BulletController>().SetVelocity();
                        }
                    }
                }
            }
        }

        public override void Hurt(int damage) {
            this.Life -= damage;
            this.ChangePlayerState(PlayerState.Hurt);
        }
    }
}