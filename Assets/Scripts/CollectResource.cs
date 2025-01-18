using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectResource : MonoBehaviour
{

    public GameObject lightEffect;
    private GameObject effect;
    public AudioClip collectSound;

    private void Start()
    {
        effect = Instantiate(lightEffect, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory _inventory = other.GetComponent<PlayerInventory>();
        if (_inventory != null && other.CompareTag("Player"))
        {
            _inventory.ResourcesCollected();
            SoundFXManager.instance.PlaySoundFXClip(collectSound, transform, 0.5f);
            Destroy(effect);
            Destroy(gameObject);
        }
    }
}
