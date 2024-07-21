using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinsaw : MonoBehaviour
{
    bool aktif;

    private void Start()
    {
        Invoke("aktifet", 1f);
    }

    public void aktifet()
    {
        aktif = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!aktif)
            return;

        if(other.gameObject.tag == "coin")
            Destroy(other.gameObject);
        else if(other.gameObject.tag == "saw")
            Destroy(other.gameObject);
    }
}
