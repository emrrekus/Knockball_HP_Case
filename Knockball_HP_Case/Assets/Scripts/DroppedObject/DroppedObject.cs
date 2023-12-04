using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedObject : MonoBehaviour
{
    private int _droppedObject;

    public int DropObject(int fallingObject)
    {
        Debug.Log(_droppedObject);
       return _droppedObject = fallingObject;
    }
}
