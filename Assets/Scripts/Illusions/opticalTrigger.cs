using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opticalTrigger : MonoBehaviour
{
    private GameObject playerCamera;
    [SerializeField] private GameObject illusionSpawn;
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerStay(Collider col)
    {
        playerCamera = GameObject.Find("Main Camera");

        if (col.gameObject.tag == "Player")
        {
            if(playerCamera.transform.eulerAngles.y <= transform.eulerAngles.y + 3 && playerCamera.transform.eulerAngles.y >= transform.eulerAngles.y - 3 && playerCamera.transform.eulerAngles.x <= transform.eulerAngles.x + 3 && playerCamera.transform.eulerAngles.x >= transform.eulerAngles.x - 3)
            {
                Debug.Log("Trigger");
                Instantiate(illusionSpawn, spawnPoint.position, spawnPoint.rotation);
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
