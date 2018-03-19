﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // struct GunItemAttr
        /*
        GunItemAttr attr = ScriptManager.Instance.FindGunItemAttr(_itemID);
        _shotSpeed = attr.shotSpeed;
        _wayCount = attr.wayCount;
        */
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // script

    void ParsingAttr()
    {

    }

    // Interfaced

    protected GameObject _bulletPrefab;

    // csv에서 shotSpeed, wayCount를 스크립트 파싱해서 사용할 수 있도록 리펙토링 해볼것.
    protected string _itemID = "defaultGun";
    protected float _shotSpeed = 0.05f;
    protected int _wayCount = 3;

    public void SetBullet(GameObject bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }

    virtual public void Fire(Quaternion startRotation)
    {
        if (null != _bulletPrefab)
        {
            GameObject bulletObject = GameObject.Instantiate(_bulletPrefab, transform.position, startRotation);
            bulletObject.transform.localScale = Vector3.one;
        }
    }
    
    public float GetShotSpeed()
    {
        return _shotSpeed;
    }
}
