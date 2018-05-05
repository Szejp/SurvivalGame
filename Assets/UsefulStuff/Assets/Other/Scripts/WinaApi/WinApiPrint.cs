using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WinApiPrint {
    public static void Print(string path) {
        try {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = path;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();
        }
        catch (System.Exception e) {
            UnityEngine.Debug.LogError(e.Message);
        }
    }
}
