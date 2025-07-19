using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "SO / WeaponSO / Weapon Sway SO", fileName = "WeaponSwayData")]
    public class WeaponSwaySO : ScriptableObject
    {
        public Vector2 _Sway_min;
        public Vector2 _Sway_max;

        public float _Sway_speed_position;
        public float _Sway_speed_rotation;

        public float _Sway_amout_position;
        public float _Sway_amout_rotation;

        public float _Idle_sway_adjustment;
        public float _Idle_sway_rotation_strength;
        public float _Random_sway_amount;

        public float _IdleSwaySpeed;
    }
}
