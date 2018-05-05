using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
    public GameObject obj;
    public bool toExecute = false;
    public virtual void Execute() { }
    public virtual void Reverse() { }
}
