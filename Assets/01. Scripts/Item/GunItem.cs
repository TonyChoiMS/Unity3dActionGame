using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // struct GunItemAttr
	// GunAttribute Class를 struct로 구현할 경우 에러 발생.
	// 접근생성자에 의한 접근권한 오류 발생
        GunAttribute attr = ScriptManager.instance.FindGunItemAttr(_itemID);
        _shotSpeed = attr.shotSpeed;
        _wayCount = attr.wayCount;
	// 값이 제대로 적용됬는지 콘솔을 통해서 확인
        Debug.Log("ShotSpeed : " + _shotSpeed);
        Debug.Log("wayCount : " + _wayCount);

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
    // 일부러 초기화를 다른 값으로 해놓은 후, 
    protected string _itemID = "defaultGun";
    protected float _shotSpeed = 0.0f;
    protected int _wayCount = 5;

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
