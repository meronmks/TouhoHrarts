using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class ShowPlayerHPMaxPresenter : MonoBehaviour {
    //View
    public Text PlayerHPMaxText;

    //Model
    public GameObject Player;
    [InspectorDisplay] public IntReactiveProperty PlayerHPMax;
    private CharaStatus status;

    void Start()
    {
        status = Player.GetComponent<CharaStatus>();
        this.UpdateAsObservable()
            .Subscribe(_ => PlayerHPMaxText.text = "PlayerHPMax:" + status.Max_HP);
        PlayerHPMax.SubscribeToText(PlayerHPMaxText);
    }
}
