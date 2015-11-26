using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class CircleHPGaugePresenter : MonoBehaviour {
    //View
    public Image circleGauge;

    //Model
    public GameObject player;
    private CharaStatus status;

    void Start()
    {
        status = player.GetComponent<CharaStatus>();
        this.UpdateAsObservable()
            .Subscribe(_ => circleGauge.fillAmount = status.HP > 75 ? 0.75f : status.HP / 100.0f);
    }
}
