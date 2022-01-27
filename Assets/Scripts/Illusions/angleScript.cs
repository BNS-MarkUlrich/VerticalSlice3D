using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angleScript : MonoBehaviour
{
    [SerializeField] private GameObject lookObject;
    private void Update()
    {
        Vector3 lookAtPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        transform.LookAt(lookObject.transform);
    }
}
