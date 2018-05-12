using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Linq;
using System.Text;

public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

public class AppClosingHandler : MonoBehaviour {

    public UnityEngine.UI.Text text;

    private IntPtr interactionWindow;
    private IntPtr hMainWindow;
    private IntPtr oldWndProcPtr;
    private IntPtr newWndProcPtr;
    private WndProcDelegate newWndProc;
    private bool isrunning = false;

    private const int WM_CLOSE = 0x0010;
    private const int WM_SYSCOMMAND = 0x0112;
    private const int SC_CLOSE = 0xF060;
    private const int WM_DESTROY = 0x0002;

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    private static extern System.IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll")]
    private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetWindowTextLength(HandleRef hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);

    private int capacity = 0;
    private void Start() {
        if (isrunning) return;

        hMainWindow = GetForegroundWindow();
        newWndProc = new WndProcDelegate(wndProc);
        newWndProcPtr = Marshal.GetFunctionPointerForDelegate(newWndProc);
        oldWndProcPtr = SetWindowLongPtr(hMainWindow, -4, newWndProcPtr);
        isrunning = true;

        capacity = GetWindowTextLength(new HandleRef(this, hMainWindow));
        StringBuilder stringBuilder = new StringBuilder(capacity);
        GetWindowText(new HandleRef(this, hMainWindow), stringBuilder, stringBuilder.Capacity);
        if (text != null) text.text = stringBuilder.ToString();
    }

    private static IntPtr StructToPtr(object obj) {
        var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(obj));
        Marshal.StructureToPtr(obj, ptr, false);
        return ptr;
    }

    private IntPtr wndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam) {
        if (msg == WM_CLOSE || msg == WM_DESTROY) {
            Application.Quit();
            return (IntPtr)0;
        }
        return CallWindowProc(oldWndProcPtr, hWnd, msg, wParam, lParam);
    }

    private void OnDisable() {
        Debug.Log("Uninstall Hook");
        if (!isrunning) return;
        SetWindowLongPtr(hMainWindow, -4, oldWndProcPtr);
        hMainWindow = IntPtr.Zero;
        oldWndProcPtr = IntPtr.Zero;
        newWndProcPtr = IntPtr.Zero;
        newWndProc = null;
        isrunning = false;
    }
}
