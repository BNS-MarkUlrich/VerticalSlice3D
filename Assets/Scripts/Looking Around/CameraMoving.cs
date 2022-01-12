using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Camera _camera;

    [Header("Sensitivity")]
    public float sensX;
    public float sensY;

    [Header("Clamping")]
    public float minY;
    public float maxY;

    [Header("Specatator")]
    public float rotX;
    public float rotY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Start()
    {
        _camera = Camera.main;
    }

    public void Looking(Vector2 pos)
    {
        rotX += pos.x * sensX;
        rotY += pos.y * sensY;

        rotY = Mathf.Clamp(rotY, minY, maxY);

        _camera.transform.rotation = Quaternion.Euler(-rotY, rotX, 0);
        
    }
}
