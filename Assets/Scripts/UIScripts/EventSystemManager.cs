using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// EventSystemの定義が面倒だからつめあわせた奴
/// </summary>
public abstract class EventSystemManager :
    MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerClickHandler,
    IBeginDragHandler,
    IEndDragHandler,
    IDragHandler,
    IDropHandler,
    IScrollHandler,
    IMoveHandler,
    ISelectHandler,
    IDeselectHandler,
    IUpdateSelectedHandler,
    ISubmitHandler,
    ICancelHandler
{
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        ;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        ;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        ;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        ;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        ;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        ;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        ;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        ;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        ;
    }

    public virtual void OnScroll(PointerEventData eventData)
    {
        ;
    }

    public virtual void OnMove(AxisEventData eventData)
    {
        ;
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        ;
    }

    public virtual void OnDeselect(BaseEventData eventData)
    {
        ;
    }

    public virtual void OnUpdateSelected(BaseEventData eventData)
    {
        ;
    }

    public virtual void OnSubmit(BaseEventData eventData)
    {
        ;
    }

    public virtual void OnCancel(BaseEventData eventData)
    {
        ;
    }
}
