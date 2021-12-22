using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walking : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    public void Walk(Vector3 pos)
    {
        Vector3 moveDir = Camera.main.transform.right * pos.x + Camera.main.transform.forward * pos.y;
        moveDir.y = 0;
        transform.position += moveDir * speed * Time.deltaTime;
    }

}
