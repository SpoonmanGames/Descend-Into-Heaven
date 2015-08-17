using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossFightController : MonoBehaviour {

    public Player.Player PlayerController;
    public Player.Player BossController;

    private List<GameObject> _plataforms = new List<GameObject>();
    private Player.Boss _enemyBoss;

    void Start() {
        PlayerController.IsFreeToMove = false;
        BossController.IsFreeToMove = false;
        _enemyBoss = (Player.Boss)BossController;

        _plataforms.Add(GameObject.FindGameObjectWithTag("Plataform1"));
        _plataforms.Add(GameObject.FindGameObjectWithTag("Plataform2"));
        _plataforms.Add(GameObject.FindGameObjectWithTag("Plataform3"));
        _plataforms.Add(GameObject.FindGameObjectWithTag("Plataform4"));

        for (int i = 0; i < _plataforms.Count; i++) {
            if (_plataforms[i] == null) {
                Debug.LogError(string.Format("Plataform{0} not found in the Scene. Please check tags and GameObjects.",i));
            } else {
                _plataforms[i].GetComponent<BulletController>().TargetPosition = 
                    new Vector2(
                        _plataforms[i].transform.position.x, 
                        _plataforms[i].transform.position.y
                    );
            }
        }
    }

    void Update() {
        PlayerController.IsFreeToMove = BossController.IsFreeToMove;

        ReposicionarPlataformas(_enemyBoss.Fase);
    }

    void ReposicionarPlataformas(int Fase) {
        switch (Fase) {
            case 2:
                MoverPlataforma(
                    0,
                    new Vector2(
                        _plataforms[0].transform.position.x,
                        -0.086f * 3
                    )
                );
                MoverPlataforma(
                    1,
                    new Vector2(
                        _plataforms[1].transform.position.x,
                        0.091f * 3
                    )
                );
                MoverPlataforma(
                    2,
                    new Vector2(
                        _plataforms[2].transform.position.x,
                        -0.086f * 3
                    )
                );
                MoverPlataforma(
                    3,
                    new Vector2(
                        _plataforms[3].transform.position.x,
                        0.091f * 3
                    )
                );
                break;
            case 3:
                MoverPlataforma(
                    0,
                    new Vector2(
                        _plataforms[0].transform.position.x,
                        -0.28f * 3
                    )
                );
                MoverPlataforma(
                    1,
                    new Vector2(
                        _plataforms[1].transform.position.x,
                        -0.148f * 3
                    )
                );
                MoverPlataforma(
                    2,
                    new Vector2(
                        _plataforms[2].transform.position.x,
                        -0.016f * 3
                    )
                );
                MoverPlataforma(
                    3,
                    new Vector2(
                        _plataforms[3].transform.position.x,
                        0.116f * 3
                    )
                );
                break;
        }
    }

    void MoverPlataforma(int id, Vector2 targetPosition){
        _plataforms[id].GetComponent<BulletController>().TargetPosition = targetPosition;
        _plataforms[id].GetComponent<BulletController>().SetVelocity();
    }
}
