using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class BaseCircleHPGaugePresenter : MonoBehaviour {
    //View
    public Image baseCircleGauge;

    //Model
    public GameObject player;
    private CharaStatus status;

    void Start()
    {
        status = player.GetComponent<CharaStatus>();
        this.UpdateAsObservable()
            .Subscribe(_ => baseCircleGauge.fillAmount = status.Max_HP > 75 ? 0.75f : status.Max_HP / 100.0f);
    }
}
