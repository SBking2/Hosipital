using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "SO / WeaponSO / Weapon Bob SO", fileName = "WeaponBobSO")]
    public class WeaponBobSO : ScriptableObject
    {
        public float _WeaponBobStartSpeed;
        public float _WeaponBobEndSpeed;

        public float _WeaponBobSpeed;
        public float _WeaponHoriMuti;
        public float _WeaponVertiMuti;
        public float _WeaponBobStartClamp;
    }
}
