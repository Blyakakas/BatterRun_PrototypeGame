using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private List<Transform> pointBatteryUse = new List<Transform>();
    [SerializeField] private BatteriesFollow _batteriesFollow;
    [SerializeField] private TouchControl _touchControl;
    [SerializeField] private float _movingSpeed;
    [SerializeField] private float _delayToMove;

    private List<Battery> _batteries = new List<Battery>();
    public bool _collision = false;
    private bool _firstCollision = false;
    private bool _firstSearch = true;

    private void Update()
    {
        if (_collision && _firstSearch)
        {
            _firstCollision = true;
            _firstSearch = false;

            for (int i = 0; i < pointBatteryUse.Count; i++)
            {
                Battery battery = _batteriesFollow._otherBatteries[_batteriesFollow._otherBatteries.Count - 1];
                _batteriesFollow._otherBatteries.RemoveAt(_batteriesFollow._otherBatteries.Count - 1);
                _batteries.Add(battery);
                battery.transform.SetParent(transform);
                battery.GetComponent<Collider>().enabled = false;
            }
        }

        if (_firstCollision)
        {
            MoveBatteries();
        }
    }

    private void MoveBatteries()
    {
        for (int i = 0; i < _batteries.Count; i++)
        {
            Battery battery = _batteries[i];
            Transform target = pointBatteryUse[i];
            battery.transform.position = Vector3.Lerp(battery.transform.position, target.position, _movingSpeed * Time.deltaTime);

            if (Vector3.Distance(battery.transform.position, target.position) < 0.01f)
            {
                battery.transform.position = target.position;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Battery battery) && !_firstCollision)
        {
            _collision = true;
            StartCoroutine(UsingBattery());
        }
    }

    private IEnumerator UsingBattery()
    {
        _touchControl.Stop();
        _batteriesFollow.NeedFollow = false;
        yield return new WaitForSeconds(_delayToMove);
        _batteriesFollow.NeedFollow = true;
        Destroy(gameObject);
        _touchControl.Move();
    }
}
