using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VideoSlider : Slider, IPointerDownHandler, IPointerUpHandler
{

    public bool IsBeingUsed;
    public Action<bool> OnScrollToggled;
  
    public void OnPointerDown(PointerEventData eventData)
    {
        IsBeingUsed = true;
        OnScrollToggled?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventdata)
    {
        IsBeingUsed = false;
        OnScrollToggled?.Invoke(false);
    }
}
