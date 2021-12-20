using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveTransform : MonoBehaviour
{
    [SerializeField] private GameObject selectedObject;

    [SerializeField] private Vector3 mousePositionWorld;

    void Start()
    {
        
    }

    void Update()
    {
        mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mousePositionWorld);
    }
}
