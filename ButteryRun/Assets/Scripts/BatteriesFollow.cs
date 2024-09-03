using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteriesFollow : MonoBehaviour
{
    [SerializeField] private Battery _mainBattery;
    [SerializeField] private Vector3 _offSet;
    [SerializeField] private float _followSpeed;
    private Transform _lastBatteryTransform; 
    public List<Battery> _otherBatteries = new List<Battery>();
    public bool NeedFollow = true;

    private void Update()
    {
        if(NeedFollow)
        {
            _lastBatteryTransform = _mainBattery.transform;
            foreach (var battery in _otherBatteries)
            {
                var batteryTransform = battery.transform;
                batteryTransform.position =
                    Vector3.Lerp(batteryTransform.position,
                    _lastBatteryTransform.position + _offSet,
                    _followSpeed * Time.deltaTime);
                _lastBatteryTransform = batteryTransform;
            }
        }
    }
}
