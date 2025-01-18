using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject _projectilePrefab;
    public float _shootForce;
    public Transform _projectileSpawnPoint;
    public AudioClip _shootSoundClip;

    public float _timeBetweenShooting, _timeBetweenShots, _cooldownTime;
    public int _magazineSize;
    public bool _allowButtonHold;

    int _projectilesLeft, _projectilesShot;

    bool _isShooting, _readyToShoot, _coolingDown;

    public TextMeshProUGUI _ammunitionDisplay;

    public bool _allowInvoke = true;

    private void Awake()
    {
        _projectilesLeft = _magazineSize;
        _readyToShoot = true;
    }

    private void Update()
    {
        MyInpout();  
       
        if (_ammunitionDisplay != null)
            _ammunitionDisplay.SetText(_projectilesLeft + " / " + _magazineSize);
    }

    private void MyInpout()
    {
        if (_allowButtonHold) _isShooting = Input.GetKey(KeyCode.Mouse0);
        else _isShooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (_readyToShoot && _isShooting && !_coolingDown && _projectilesLeft <= 0) Reload();

        if (_readyToShoot && _isShooting && !_coolingDown && _projectilesLeft > 0)
        {
            _projectilesShot = 0;
            Shoot();
        }

        if (_readyToShoot && !_isShooting && !_coolingDown && _projectilesLeft > 0 && Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

    }

    private void Shoot()
    {
        _readyToShoot = false;

        var _projectile = Instantiate(_projectilePrefab, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
        _projectile.GetComponent<Rigidbody>().velocity = _projectileSpawnPoint.forward * _shootForce;

        SoundFXManager.instance.PlaySoundFXClip(_shootSoundClip, transform, 0.5f);

        _projectilesLeft--;
        _projectilesShot++;

        if (_allowInvoke)
        {
            Invoke("ResetShot", _timeBetweenShooting);
            _allowInvoke = false;
        }
    }

    private void ResetShot()
    {
        _readyToShoot = true;
        _allowInvoke = true;
    }

    private void Reload()
    {
        _coolingDown = true;
        Invoke("ReloadFinished", _cooldownTime);
    }

    private void ReloadFinished()
    {
        _projectilesLeft = _magazineSize;
        _coolingDown = false;
    }

}

