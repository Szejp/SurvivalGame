using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnMousePosition : MonoBehaviour {

    private void Update() {
        Vector3 position = RectTransformUtility.ScreenPointToRay(Camera.main, Input.mousePosition).origin;
        position = new Vector3(position.x, position.y, 0);
        transform.rotation = Quaternion.LookRotation(position - transform.position);
    }
}
