using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public Transform resourcePref;
    public int asteroidHP;
    public float damage;
    public AudioClip asteroidExplodeClip;
    public AudioClip playerHitClip;
    public GameObject explosionEffect;


    public delegate void AsteroidDestroyed(GameObject asteroid);
    public event AsteroidDestroyed OnDestroyed;

    void OnDestroy()
    { 
        OnDestroyed?.Invoke(gameObject);
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().health -= damage;
            Destroy(gameObject);
            Explode();
            SoundFXManager.instance.PlaySoundFXClip(playerHitClip, transform, 0.5f);
            Instantiate(resourcePref, transform.position, transform.rotation);
        }
        else if (!other.CompareTag("Resource") && !other.CompareTag("Enemy"))
        {
            if (asteroidHP <= 0)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
                Explode();
                SoundFXManager.instance.PlaySoundFXClip(asteroidExplodeClip, transform, 0.5f);
                Instantiate(resourcePref, transform.position, transform.rotation);
            }
            else
            {
                asteroidHP--;
                if (asteroidHP <= 0)
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                    Explode();
                    SoundFXManager.instance.PlaySoundFXClip(asteroidExplodeClip, transform, 0.5f);
                    Instantiate(resourcePref, transform.position, transform.rotation);
                }
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(resourcePref, transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().health -= damage;
            Destroy(gameObject);
            Explode();
            SoundFXManager.instance.PlaySoundFXClip(playerHitClip, transform, 0.5f);
            Instantiate(resourcePref, transform.position, transform.rotation);
        }
        else if (!collision.gameObject.CompareTag("Resource") && !collision.gameObject.CompareTag("Enemy"))
        {
            if (asteroidHP <= 0)
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
                Explode();
                SoundFXManager.instance.PlaySoundFXClip(asteroidExplodeClip, transform, 0.5f);
                Instantiate(resourcePref, transform.position, transform.rotation);
            }
            else
            {
                asteroidHP--;
                if (asteroidHP <= 0)
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                    Explode();
                    SoundFXManager.instance.PlaySoundFXClip(asteroidExplodeClip, transform, 0.5f);
                    Instantiate(resourcePref, transform.position, transform.rotation);
                }
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Explode();
            Instantiate(resourcePref, transform.position, transform.rotation);
        }
        
    }


}
