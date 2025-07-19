using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class WeaponSwayDS
    {
        public Vector3 _Position;
        public Vector3 _Rotation;
        public Vector3 _Scale;

        public float _IdleSwayAdjustment;
        public float _IdleRandomSwayAmount;
        public float _IdleRandomSwayStrength;

        public Vector2 _SwayMin;
        public Vector2 _SwayMax;

        public float _SwayPositionAmount;
        public float _SwayRotationAmount;

        public float _SwayPositionSpeed;
        public float _SwayRotationSpeed;

        public float _IdleSwaySpeed;
    }
}
