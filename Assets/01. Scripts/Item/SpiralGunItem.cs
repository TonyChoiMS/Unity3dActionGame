using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralGunItem : GunItem
{
    override protected void CreateGunModule()
    {
        {
            GunModule gunModule = new SpiralGunModule();
            gunModule._rotateY = 0.6f;
            gunModule._wayCount = 5;
            gunModule.Init(this);
            _gunModuleList.Add(gunModule);
        }
        {
            GunModule gunModule = new SpiralGunModule();
            gunModule._wayCount = 3;
            gunModule._rotateY = -0.6f;
            gunModule.Init(this);
            _gunModuleList.Add(gunModule);
        }
        {
            GunModule gunModule = new SpiralGunModule();
            gunModule._wayCount = 2;
            gunModule._rotateY = -0.9f;
            gunModule.Init(this);
            _gunModuleList.Add(gunModule);
        }
        {
            GunModule gunModule = new SpiralGunModule();
            gunModule._wayCount = 9;
            gunModule._rotateY = 0.9f;
            gunModule.Init(this);
            _gunModuleList.Add(gunModule);
        }
    }
}
