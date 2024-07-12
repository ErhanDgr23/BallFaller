using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinsaw : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "coin")
            Destroy(other.gameObject);
        else if(other.gameObject.tag == "saw")
            Destroy(other.gameObject);
    }
}
