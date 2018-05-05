using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fIRE : Command {
    public Transform gun;
    public float timeLimit = 0.1f;
    protected float lastFired = 0;

    public bool CheckFireFrequency() {
        if (Time.realtimeSinceStartup - lastFired > timeLimit) {
            lastFired = Time.realtimeSinceStartup;
            return true;
        }
        return false;
    }
}
