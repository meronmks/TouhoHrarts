using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class BaseLineHPGaugePresenter : MonoBehaviour {
    //View
    public Image baseLineGauge;

    //Model
    public GameObject player;
    private CharaStatus status;

    void Start()
    {
        status = player.GetComponent<CharaStatus>();
        this.UpdateAsObservable()
            .Subscribe(_ => baseLineGauge.fillAmount = status.Max_HP > 75 ? (status.Max_HP - 75) / 100.0f : 0.0f);

    }
}
