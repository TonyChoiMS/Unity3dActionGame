﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : MonoBehaviour
{

	void Start() {
		GunAttribute attr = ScriptManager.instance.FindGunItemAttr (_itemID);
        _shotSpeed = float.Parse(attr.getShotSpeed());
        //_wayCount = int.Parse(attr.getWayCount());
	}

    void Awake()
    {
        CreateGunModule();
    }

    // Interfaced

    protected GameObject _bulletPrefab;
    protected Character.eGroupType _ownerGroupType;

    protected string _itemID = "default_gun";

    protected float _shotSpeed = 0.3f;

    protected List<GunModule> _gunModuleList = new List<GunModule>();

    virtual protected void CreateGunModule()
    {
        {
            GunModule gunModule = new GunModule();
            gunModule.Init(this);
            _gunModuleList.Add(gunModule);
        }
        {
            GunModule gunModule = new SpiralGunModule();
            gunModule.Init(this);
            _gunModuleList.Add(gunModule);
        }
        {
            GunModule gunModule = new NWayGunModule();
            gunModule.Init(this);
            _gunModuleList.Add(gunModule);
        }
    }


    public void InitGroupType(Character.eGroupType groupType)
    {
        _ownerGroupType = groupType;
    }

    public void SetBullet(GameObject bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }

    public float GetShotSpeed()
    {
        return _shotSpeed;
    }

    public void Fire(Quaternion startRotation)
    {
        if(null != _bulletPrefab)
        {
            for (int i = 0; i < _gunModuleList.Count; i++)
            {
                _gunModuleList[i].Fire(startRotation);
            }
            //CreateBullet(startRotation);
        }
    }

    public void CreateBullet(Quaternion startRotation)
    {
        GameObject bulletObject = GameObject.Instantiate(_bulletPrefab, transform.position, startRotation);
        bulletObject.transform.localScale = Vector3.one;

        bulletObject.GetComponent<BulletItem>().SetOwnerGroupType(_ownerGroupType);
    }
}
