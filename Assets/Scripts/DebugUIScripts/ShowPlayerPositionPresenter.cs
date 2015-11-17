using UnityEngine;
using UniRx;
using System.Collections;
using UniRx.Triggers;
using UnityEngine.UI;

public class ShowPlayerPositionPresenter : MonoBehaviour
{
    //View
    public Text PlayerPositionText;

    //Model
    public GameObject Player;
    [InspectorDisplay] public IntReactiveProperty PlayerPosition;
 
    void Start()
    {
        this.UpdateAsObservable()
            .Subscribe(_ => PlayerPositionText.text = Player.transform.position.ToString());
        PlayerPosition.SubscribeToText(PlayerPositionText);
    }
}
