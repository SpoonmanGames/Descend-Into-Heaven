using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class BossFightController : MonoBehaviour {

    public Player.Player PlayerController;
    public Player.Player BossController;

    [Header("Audio Setup")]
    public AudioMixerSnapshot endSnapshot;
    public AudioClip victoryAudio;
    public float bpm = 150;
    public AudioSource endLoopSource;

    private List<GameObject> _plataforms = new List<GameObject>();
    private Player.Boss _enemyBoss;
    private float _transitionOut;
    private float _quarterNote;
    private bool _victoryAudio = false;




    void Start() {

        _quarterNote = 60 / bpm;
        _transitionOut = _quarterNote * 1; // cuantas negras de transición al otro tema
        endLoopSource = GetComponent<AudioSource>();

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

        if (BossController.IsDead && !PlayerController.IsDead) {
            PlayerController.IsFreeToMove = false;
            PlayerController.ChangePlayerState(Player.PlayerState.Victory);
            if (!_victoryAudio){
                GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioSource>().Stop();
                endLoopSource.PlayOneShot(victoryAudio,1F);
                endSnapshot.TransitionTo(_transitionOut);
                _victoryAudio = true;

            }

        }
    }

    void ReposicionarPlataformas(int Fase) {
        switch (Fase) {
            case 2:
                MoverPlataforma(
                    0, -1,
                    new Vector2(
                        _plataforms[0].transform.position.x,
                        -0.086f * 3
                    )
                );
                MoverPlataforma(
                    1, 1,
                    new Vector2(
                        _plataforms[1].transform.position.x,
                        0.091f * 3
                    )
                );
                MoverPlataforma(
                    2, -1,
                    new Vector2(
                        _plataforms[2].transform.position.x,
                        -0.086f * 3
                    )
                );
                MoverPlataforma(
                    3, 1,
                    new Vector2(
                        _plataforms[3].transform.position.x,
                        0.091f * 3
                    )
                );
                break;
            case 3:
                MoverPlataforma(
                    0, -1,
                    new Vector2(
                        _plataforms[0].transform.position.x,
                        -0.28f * 3
                    )
                );
                MoverPlataforma(
                    1, -1,
                    new Vector2(
                        _plataforms[1].transform.position.x,
                        -0.148f * 3
                    )
                );
                MoverPlataforma(
                    2, 1,
                    new Vector2(
                        _plataforms[2].transform.position.x,
                        -0.016f * 3
                    )
                );
                MoverPlataforma(
                    3, 1,
                    new Vector2(
                        _plataforms[3].transform.position.x,
                        0.116f * 3
                    )
                );
                break;
            case 4:
                MoverPlataforma(
                    0, 1,
                    new Vector2(
                        _plataforms[0].transform.position.x,
                        0.0f
                    )
                );
                MoverPlataforma(
                    1, 1,
                    new Vector2(
                        _plataforms[1].transform.position.x,
                        0.0f
                    )
                );
                MoverPlataforma(
                    2, -1,
                    new Vector2(
                        _plataforms[2].transform.position.x,
                        0.0f
                    )
                );
                MoverPlataforma(
                    3, -1,
                    new Vector2(
                        _plataforms[3].transform.position.x,
                        0.0f
                    )
                );
                break;
        }
    }

    void MoverPlataforma(int id, int direccion, Vector2 targetPosition){
        _plataforms[id].GetComponent<BulletController>().TargetPosition = targetPosition;
        _plataforms[id].GetComponent<BulletController>().Direccion = direccion;
        _plataforms[id].GetComponent<BulletController>().SetVelocity();
    }
}
