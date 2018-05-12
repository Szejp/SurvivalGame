using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour {

    public abstract void MoveLeft();
    public abstract void MoveRight();
    public abstract void MoveForwards();
    public abstract void MoveBackwards();
    public abstract void MoveUp();
    public abstract void MoveDown();
    public abstract void Teleport(Vector3 position);
}
