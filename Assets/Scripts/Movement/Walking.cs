using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walking : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    public void Walk(Vector3 Pos)
    {
        Vector3 moveDir = transform.right * Pos.x + transform.forward * Pos.y;
        transform.position += moveDir * speed * Time.deltaTime;
    }

}
