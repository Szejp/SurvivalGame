using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Reflection;

public class DisplayCounter {

    private delegate bool MonitorEnumProc(IntPtr hDesktop, IntPtr hdc, ref Rect pRect, int dwData);
    [DllImport("user32")]
    private static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lpRect, MonitorEnumProc callback, int dwData);

    private static MonitorEnumProc callback;
    private static int monCount;
    public static List<IntPtr> windowPointers;

    static DisplayCounter() {
        windowPointers = new List<IntPtr>();
        callback = (IntPtr hDesktop, IntPtr hdc, ref Rect prect, int d) => {
            windowPointers.Add(hDesktop);
            return ++monCount > 0;
        };
    }

    public static int Count() {
        monCount = 0;
        windowPointers.Clear();
        EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, 0);
        Debug.Log(monCount);
        return monCount;
    }

    public static void RefreshUnityDisplaysCount() {
        Count();
        Type type = typeof(Display);
        MethodInfo recreateDisplayList = type.GetMethod("RecreateDisplayList", BindingFlags.Static | BindingFlags.NonPublic);
        MethodInfo fireDisplaysUpdated = type.GetMethod("FireDisplaysUpdated", BindingFlags.Static | BindingFlags.NonPublic);
        recreateDisplayList.Invoke(null, new object[] { windowPointers.ToArray() });
        fireDisplaysUpdated.Invoke(null, new object[] { });

        foreach (Display d in Display.displays) {
            d.Activate();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}
