using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransformOnMousePosition : MonoBehaviour {

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT {
        public int x;
        public int y;

        public POINT(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }

    private Vector3 position;
    private Vector3 screenPos;
    private IntPtr hwnd;
    private POINT point;

    private void Start() {
        hwnd = GetForegroundWindow();
    }

    private void Update() {
        GetCursorPos(out point);
        ScreenToClient(hwnd, ref point);
        screenPos = new Vector3(point.x, Screen.height - point.y, 0);
        Debug.Log(screenPos);
        position = Camera.main.ScreenToWorldPoint(screenPos);
        position = new Vector3(position.x, position.y, 0);
        transform.position = position;
    }
}
