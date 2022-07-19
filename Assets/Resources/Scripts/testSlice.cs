using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicer;

public class testSlice : MonoBehaviour
{
    void Start()
    {
        IBzSliceable s = GetComponent<IBzSliceable>();

        s.Slice(new Plane(), null);
    }
}
