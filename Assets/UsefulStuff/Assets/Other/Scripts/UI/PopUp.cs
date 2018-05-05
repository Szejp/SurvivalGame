using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopUp : MonoBehaviour {
    public Button btnOK;
    public Button btnCancel;

    public GameObject Parent {
        get {
            if (parent == null) parent = transform.parent.gameObject;
            return parent;
        }
    }

    protected GameObject parent;

    public void Show(Action OnOK = null, Action OnCancel = null) {
        gameObject.SetActive(true);
        btnOK.onClick.RemoveAllListeners();
        btnCancel.onClick.RemoveAllListeners();

        btnOK.onClick.AddListener(() => {
            if (OnOK != null)
                OnOK();
            Hide();
        });

        btnCancel.onClick.AddListener(() => {
            if (OnCancel != null)
                OnCancel();
            Hide();
        });
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
