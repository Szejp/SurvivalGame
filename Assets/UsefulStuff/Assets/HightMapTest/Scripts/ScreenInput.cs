using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ScreenInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public static event Action<Vector2> OnPointerDownEvent;
    public static event Action<Vector2> OnPointerUpEvent;

    public void OnPointerDown(PointerEventData pointerData) {
        OnPointerDownEvent?.Invoke(pointerData.position);
        
    }

    public void OnPointerUp(PointerEventData pointerData) {
        OnPointerUpEvent?.Invoke(pointerData.position);
        
    }
}
