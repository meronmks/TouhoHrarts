using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class ShowToggleDebugCanvasPresenter : MonoBehaviour {
    //View

    //Model
    public Canvas DebugCanvas;

    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.F12))
            .Subscribe(_ => DebugCanvas.enabled = !DebugCanvas.enabled);
    }
}
