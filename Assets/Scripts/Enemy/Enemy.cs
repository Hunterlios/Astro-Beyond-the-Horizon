using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform resourcePref;
    public float moveSpeed = 50;
    public float enemyHP;
    public float damage = 50;
    public AudioClip enemyExplodeClip;
    public AudioClip playerHitClip;
    public GameObject explosionEffect;

    public delegate void EnemyDestroyed(GameObject enemy);
    public event EnemyDestroyed OnDestroyed;

    void OnDestroy()
    {
        OnDestroyed?.Invoke(gameObject);
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            transform.position = Vector3.MoveTowards(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, moveSpeed * Time.deltaTime);
        }
    
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
            if (enemyHP <= 0)
            {
                Destroy(gameObject);
                Destroy(other.gameObject);
                Explode();
                SoundFXManager.instance.PlaySoundFXClip(enemyExplodeClip, transform, 0.5f);
                Instantiate(resourcePref, transform.position, transform.rotation);
            }
            else
            {
                enemyHP--;
                if (enemyHP <= 0)
                {
                    Destroy(gameObject);
                    Destroy(other.gameObject);
                    Explode();
                    SoundFXManager.instance.PlaySoundFXClip(enemyExplodeClip, transform, 0.5f);
                    Instantiate(resourcePref, transform.position, transform.rotation);
                }

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().health -= damage;
            Destroy(gameObject);
            Explode();
            SoundFXManager.instance.PlaySoundFXClip(playerHitClip, transform, 0.5f);
            Instantiate(resourcePref, transform.position, transform.rotation);
        }
        else if (!collision.gameObject.CompareTag("Resource") && !collision.gameObject.CompareTag("Enemy"))
        {
            if (enemyHP <= 0)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                Explode();
                SoundFXManager.instance.PlaySoundFXClip(enemyExplodeClip, transform, 0.5f);
                Instantiate(resourcePref, transform.position, transform.rotation);
            }
            else
            {
                enemyHP--;
                if (enemyHP <= 0)
                {
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                    Explode();
                    SoundFXManager.instance.PlaySoundFXClip(enemyExplodeClip, transform, 0.5f);
                    Instantiate(resourcePref, transform.position, transform.rotation);
                }
            }
        }
    }

}
