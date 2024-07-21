using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playersc : MonoBehaviour
{
    [SerializeField] float tiltSensitivity = 1f;
    [SerializeField] float cooldown = 3f;

    PhysicMaterial matfizik;
    gamemanager manager;
    Rigidbody rb;
    AudioClip hitsound, deathsound, smallwinsound, failsound;
    AudioSource sesci;
    bool tiltmode = true;
    [SerializeField] bool gorunur;
    [SerializeField] float zmn;

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
        matfizik.bounciness = Random.Range(0.48f, 0.65f);
    }

    private void FixedUpdate()
    {
        if (!gorunur)
        {
            zmn += Time.deltaTime;

            if (zmn > cooldown)
            {
                sesci.pitch = 1.2f;
                sesci.volume = 1f;
                sesci.PlayOneShot(failsound);
                Destroy(this.gameObject);
                zmn = 0f;
            }
        }
        else
            zmn = 0f;

        if (!tiltmode)
            return;

        Vector3 acceleration = Input.acceleration;
        float tilt = acceleration.x * tiltSensitivity * Time.deltaTime;
        Vector3 force = new Vector3(tilt, 0, 0);
        rb.velocity = new Vector3(rb.velocity.x + force.x, rb.velocity.y, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        matfizik.bounciness = Random.Range(0.48f, 0.65f);

        if (collision.gameObject != null)
        {
            sesci.pitch = 1.2f;
            sesci.volume = 0.5f;
            sesci.PlayOneShot(hitsound);
        }

        if (collision.gameObject.tag == "temas")
        {
            collision.transform.GetComponent<Animator>().Play("daire");
        }

        if (collision.gameObject.tag == "death")
        {
            sesci.pitch = 1.2f;
            sesci.volume = 1f;
            sesci.PlayOneShot(failsound);
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        gorunur = false;
    }

    private void OnBecameVisible()
    {
        gorunur = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "kap")
        {
            transform.GetComponent<SphereCollider>().enabled = false;
            other.transform.parent.GetComponent<Animator>().Play("kapanim");
            manager.oyunbitti(this.gameObject, other.gameObject.transform.parent.GetComponent<kapsc>().takekapvalue(), other.transform.parent.GetComponent<kapsc>());
            sesci.pitch = 1f;
            sesci.volume = 1f;
            sesci.PlayOneShot(smallwinsound);
            rb.velocity = Vector3.zero;
            tiltmode = false;
        }

        if (other.gameObject.tag == "coin")
        {
            if(other.transform.parent != null)
                Destroy(other.transform.parent.gameObject);

            manager.piramidsc.testereteklispawn();
            manager.updatepuan(1);
            sesci.pitch = 1f;
            sesci.volume = 1f;
            sesci.PlayOneShot(smallwinsound);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "saw")
        {
            Destroy(other.gameObject);
            manager.piramidsc.testereteklispawn();
            sesci.pitch = 1.2f;
            sesci.volume = 1f;
            sesci.PlayOneShot(failsound);
            Destroy(this.gameObject);
        }
    }
}
