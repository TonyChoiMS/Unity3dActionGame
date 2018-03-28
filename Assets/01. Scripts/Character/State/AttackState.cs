using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    bool _isShoot = false;
    Quaternion _characterRotation;

    float _attackTime = 3.0f;
    float _attackDuration = 0.0f;

    override public void Start()
    {
        _characterRotation = _character.CharacterModel.transform.localRotation;

        _attackTime = Random.Range(18.0f, 20.0f);
        _attackDuration = 0.0f;
        // 더 생각해볼 주제 : 총이나 총알을 바꾸어 준다.

        _isShoot = false;
        _shotTime = _character.GetShotSpeed();
        _character.GetAnimationModule().Play("attack", null, null, () =>
        {
            _isShoot = true;
        });
    }

    override public void Stop()
    {
        _character.CharacterModel.transform.localRotation = _characterRotation;
    }

    public override void Update()
    {
        base.Update();
        _character.CharacterModel.transform.localPosition = Vector3.zero;

        if(_attackTime <= _attackDuration)
        {
            _character.ChangeState(Character.eState.IDLE);
        }
        else
        {
            UpdateShoot();
            _character.UpdateMove();
        }
        _attackDuration += Time.deltaTime;
    }


    // Shoot

    float _shotTime = 0.0f;

    void UpdateShoot()
    {
        if (false == _isShoot)
            return;

        if(_character.GetShotSpeed() <= _shotTime)
        {
            _shotTime = 0.0f;
            Shot();
        }
        _shotTime += Time.deltaTime;
    }

    void Shot()
    {
        _character.Shot();
    }
}
