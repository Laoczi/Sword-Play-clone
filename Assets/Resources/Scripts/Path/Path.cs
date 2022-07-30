using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] Transform[] _pathPoints;
    public int lenght { get { return _pathPoints.Length; } }

    public Transform GetPoint(int index)
    {
        if (index < 0) return _pathPoints[0];
        else if (index >= _pathPoints.Length) return _pathPoints[_pathPoints.Length - 1];
        else return _pathPoints[index];
    }
}
