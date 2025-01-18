using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockFPS : MonoBehaviour
{
    public int frameRate;

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = frameRate;
    }
}
