using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupControl : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        //control the rotation of the canvas
        transform.LookAt(Camera.main.transform);

        //remove after 3 seconds
        Destroy(gameObject, 1f);
    }
}
