using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCapsule : MonoBehaviour
{
    const string ROCKET_TAG = "Rocket";

    [SerializeField] AudioClip consume;

    private MeshRenderer mr;
    private CapsuleCollider cc;
    private AudioSource audio;
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        cc = GetComponent<CapsuleCollider>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case ROCKET_TAG:
                disappear();
                break;
        }
    }

    private void disappear()
    {
        mr.enabled = false;
        cc.enabled = false;
        audio.PlayOneShot(consume);
    }
}
