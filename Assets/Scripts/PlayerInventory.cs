using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int _resourcesCollected { get; private set; }
    public static PlayerInventory instance;
    public UnityEvent<PlayerInventory> OnResourceCollected;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ResourcesCollected()
    {
        _resourcesCollected += Random.Range(1, 10);
        OnResourceCollected.Invoke(this);
    }
}
