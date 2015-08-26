using UnityEngine;
using System.Collections;

namespace Player {

    public class CrossEnemy : Player {

        [Header("Cross Setup")]
        [SerializeField] private Player PlayerController;
        [SerializeField] private GameObject BulletToSpawn;
        [SerializeField] private Bounds DetectionBlock;
        public float AttackDelay = 1.0f;
        public bool ShootAtTarget = false;
        public bool ChooseShootDirections = false;
        public bool ShootLeft = false;
        public bool ShootUp = false;
        public bool ShootRight = false;

        private Bounds _realDetectionBlock;
        private Vector3 _leftSpawnPosition = new Vector3(-0.2283f, -0.1545f);
        private Vector3 _upSpawnPosition = new Vector3(0.0f, 0.0141f);
        private Vector3 _rightSpawnPosition = new Vector3(0.2283f, -0.1545f);
        private float _attackDelayCounter = 0.0f;

        private GameObject _BulletToSpawn;
        
        protected override void Update() {
            base.Update();

            _realDetectionBlock = DetectionBlock;
            _realDetectionBlock.center = DetectionBlock.center + this.transform.position;

            if (EditorDebugMode) {
                DebugExtension.DebugBounds(_realDetectionBlock, Color.red);
            }

            if (_realDetectionBlock.Contains(PlayerController.transform.position)) {
                Debug.Log("In Range");
                if (ShootAtTarget) {

                } else {
                    

                    if (ChooseShootDirections) {

                    } else {
                        _attackDelayCounter += Time.deltaTime;

                        if (_attackDelayCounter >= AttackDelay) {
                            _attackDelayCounter = 0;

                            _BulletToSpawn = Instantiate(BulletToSpawn, this.transform.position + _leftSpawnPosition, this.transform.rotation) as GameObject;
                            _BulletToSpawn.GetComponent<BulletController>().TargetPosition = new Vector2(_realDetectionBlock.min.x, _BulletToSpawn.transform.position.y);
                            _BulletToSpawn.GetComponent<BulletController>().SetVelocity();

                            _BulletToSpawn = Instantiate(BulletToSpawn, this.transform.position + _upSpawnPosition, this.transform.rotation) as GameObject;
                            _BulletToSpawn.GetComponent<BulletController>().TargetPosition = new Vector2(_BulletToSpawn.transform.position.x, _realDetectionBlock.max.y);
                            _BulletToSpawn.GetComponent<BulletController>().Direccion = 1;
                            _BulletToSpawn.GetComponent<BulletController>().SetVelocity();

                            _BulletToSpawn = Instantiate(BulletToSpawn, this.transform.position + _rightSpawnPosition, this.transform.rotation) as GameObject;
                            _BulletToSpawn.GetComponent<BulletController>().TargetPosition = new Vector2(_realDetectionBlock.max.x, _BulletToSpawn.transform.position.y);
                            _BulletToSpawn.GetComponent<BulletController>().Direccion = 1;
                            _BulletToSpawn.GetComponent<BulletController>().SetVelocity();
                        }
                    }
                }
            }

        }

        public override void Hurt(int damage) {
            Life -= damage;
            this.ChangePlayerState(PlayerState.Hurt);
        }
    }
}