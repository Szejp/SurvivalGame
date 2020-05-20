using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour, IDragHandler {

	public static UnityAction<Vector2> OnDragEvent = new UnityAction<Vector2>(p => { });

	public void OnDrag(PointerEventData eventData) {
		OnDragEvent.Invoke(eventData.delta);
	}
}