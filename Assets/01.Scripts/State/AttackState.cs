using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    bool _isShoot = false;

    public override void Start()
    {
        //_character.ChacterModel.GetComponent<Animator>().SetTrigger("Attack");
        _isShoot = false;
        _shotTime = 0.0f;
        _character.GetAnimationModule().Play("Attack", () =>
        {
            Debug.Log("Start");
        },
        () =>
        {
            Debug.Log("MIddle");
        },
            () =>
        {
            Debug.Log("End");
            _isShoot = true;
        });
    }

    public override void Update()
    {
        base.Update();
        //_character.CharacterModel.transform.position = Vector3.zero;

        UpdateShoot();
    }

    // Shoot
    float _shotTime = 0.0f;

    void UpdateShoot()
    {
        if (false == _isShoot)
            return;

        if (_character.GetShotSpeed() <= _shotTime)
        {
            _shotTime = 0.0f;
            Shot();
        }
        _shotTime += Time.deltaTime;
    }

    void Shot()
    {
        Debug.Log("Shot");
    }
}
