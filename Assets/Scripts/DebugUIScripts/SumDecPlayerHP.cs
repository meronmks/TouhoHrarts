using UnityEngine;
using UniRx;
using System.Collections;
using UniRx.Triggers;

public class SumDecPlayerHP : MonoBehaviour {
    //Model
    public GameObject player;
    private CharaStatus status;

    void Start()
    {
        status = player.GetComponent<CharaStatus>();

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                if (Input.GetKeyDown(KeyCode.F10))
                {
                    status.HP++;
                }
                else if (Input.GetKeyDown(KeyCode.F11))
                {
                    status.HP--;
                }
            });
    }
}
