using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolderPickup : MonoBehaviour
{
    public GameObject selectedGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<PerspectiveTransform>().SetSelectedGameObject(selectedGameObject);
    }
}
