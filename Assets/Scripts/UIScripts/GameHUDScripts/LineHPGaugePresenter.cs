using UnityEngine;
using UniRx;
using System.Collections;
using UniRx.Triggers;
using UnityEngine.UI;

public class LineHPGaugePresenter : MonoBehaviour {
    //View
    public Image lineGauge;

    //Model
    public GameObject player;
    private CharaStatus status;

    void Start()
    {
        status = player.GetComponent<CharaStatus>();
        this.UpdateAsObservable()
            .Subscribe(_ => lineGauge.fillAmount = status.HP > 75 ? (status.HP - 75)/100.0f : 0.0f);

    }
}
