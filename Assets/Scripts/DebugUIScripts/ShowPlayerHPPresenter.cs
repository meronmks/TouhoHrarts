using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class ShowPlayerHPPresenter : MonoBehaviour {
    //View
    public Text PlayerHPText;

    //Model
    public GameObject Player;
    [InspectorDisplay] public IntReactiveProperty PlayerHP;
    private CharaStatus status;

    void Start()
    {
        status = Player.GetComponent<CharaStatus>();
        this.UpdateAsObservable()
            .Subscribe(_ => PlayerHPText.text = "PlayerHP:" + status.HP);
        PlayerHP.SubscribeToText(PlayerHPText);
    }
}
