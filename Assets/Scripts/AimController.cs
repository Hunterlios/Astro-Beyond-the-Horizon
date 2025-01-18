using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    //public Transform projectileSpawnPoint;
    //private Vector3 offset = new Vector3(0f, 50f, 100f);

    public float moveSpeed = 0.2f;
    private Vector3 targetPosition;
    

    void Start()
    {
        float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        targetPosition = new Vector3(screenWidth / 2f, (screenHeight / 2f) * 1.1f, 0f);

    }
    void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            transform.position = Vector3.Lerp(targetPosition, Input.mousePosition, moveSpeed);
        }
        //transform.position = projectileSpawnPoint.position + (projectileSpawnPoint.rotation * offset);
        //transform.LookAt(projectileSpawnPoint, projectileSpawnPoint.up);


    }

}
