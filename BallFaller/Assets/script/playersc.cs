using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersc : MonoBehaviour
{
    [SerializeField] float tiltSensitivity = 1f;

    PhysicMaterial matfizik;
    gamemanager manager;
    Rigidbody rb;
    AudioClip hitsound, deathsound, smallwinsound, failsound;
    AudioSource sesci;
    bool tiltmode = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = gamemanager.mangersc;
        matfizik = manager.fizikmat;       
        hitsound = manager.hitsound;
        deathsound = manager.deathsound;
        sesci = manager.sesci;
        smallwinsound = manager.smallwinsound;
        failsound = manager.failsound;
    }

    private void FixedUpdate()
    {
        if (!tiltmode)
            return;

        Vector3 acceleration = Input.acceleration;
        float tilt = acceleration.x * tiltSensitivity * Time.deltaTime;
        Vector3 force = new Vector3(tilt, 0, 0);
        rb.velocity = new Vector3(rb.velocity.x + force.x, rb.velocity.y, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        matfizik.bounciness = Random.Range(0.48f, 0.7f);

        if (collision.gameObject != null)
            sesci.PlayOneShot(hitsound);

        if (collision.gameObject.tag == "death")
        {
            manager.tophazir = true;
            sesci.PlayOneShot(failsound);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "kap")
        {
            manager.oyunbitti(this.gameObject, other.gameObject.transform.parent.GetComponent<kapsc>().takekapvalue());
            rb.velocity = Vector3.zero;
            tiltmode = false;
        }

        if (other.gameObject.tag == "coin")
        {
            manager.updatepuan(2);
            sesci.PlayOneShot(smallwinsound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "saw")
        {
            manager.updatepuan(-2);
            sesci.PlayOneShot(deathsound);
            Destroy(other.gameObject);
        }
    }
}
