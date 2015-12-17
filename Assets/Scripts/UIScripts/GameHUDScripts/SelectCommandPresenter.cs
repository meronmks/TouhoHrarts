using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

public class SelectCommandPresenter : MonoBehaviour
{
    private GameObject commandUI;
    private RectTransform UIRect;

    // Use this for initialization
    void Start ()
	{
        this.UpdateAsObservable()
            .Select(_ => EventSystem.current.currentSelectedGameObject)
            .DistinctUntilChanged()
            .Subscribe(selectObject =>
            {
                SlideSelectMenu(selectObject);
            });

	}

    /// <summary>
    /// 選択されたメニュー項目を横にずらす奴
    /// </summary>
    /// <param name="selectObject">選択中の項目</param>
    void SlideSelectMenu(GameObject selectObject)
    {
        if (selectObject == null) return;
        var rectTransform = this.GetComponent<RectTransform>();
        if (this.name.Equals(selectObject.name))
        {
            rectTransform.Translate(new Vector3(10, 0, 0));
        }
        else
        {
            if (rectTransform.localPosition.x <= -526) return;
            rectTransform.Translate(new Vector3(-10, 0, 0));
        }
    }
}
