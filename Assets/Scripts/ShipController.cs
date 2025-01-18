using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f, boostSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public AudioClip boostSoundClip;
    public float maxSpeed = 80f;
    public float minSpeed = 0f;
    private float currentSpeed;
    public float minPitch;
    public float maxPitch;
    private float pitchFromShip;
    private AudioSource shipAudioSource;

    public float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;
    private bool _coolingDown;
    public float _cooldownTime;


    void Start()
    {
        shipAudioSource = GetComponent<AudioSource>();
        screenCenter.x = Screen.width * 0.5f;
        screenCenter.y = Screen.height * 0.5f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void EngineSound(float activeSpeed)
    {
        currentSpeed = activeSpeed;
        pitchFromShip = activeSpeed / 100f;

        if (currentSpeed < minSpeed)
        {
            shipAudioSource.pitch = minPitch;
        }
        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            shipAudioSource.pitch = minPitch + pitchFromShip;
        }
        if (currentSpeed > maxSpeed)
        {
            shipAudioSource.pitch = maxPitch;
        }
    }

    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y - 70f;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.deltaTime, mouseDistance.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);


        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed,  Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime) + (transform.forward * activeForwardSpeed * Time.deltaTime);

        Debug.Log(activeForwardSpeed);
        EngineSound(activeForwardSpeed);
        if (!_coolingDown && Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.W))
            {
                SoundFXManager.instance.PlaySoundFXClip(boostSoundClip, transform, 0.3f);
                BoosterCooldown();
                activeForwardSpeed *= boostSpeed;
            }
        }

    }

    private void BoosterCooldown()
    {
        _coolingDown = true;
        Invoke("BoosterCooldownFinished", _cooldownTime);
    }

    private void BoosterCooldownFinished()
    {
        _coolingDown = false;
    }
}
