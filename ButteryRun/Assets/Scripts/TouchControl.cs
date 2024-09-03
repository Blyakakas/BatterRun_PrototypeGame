using UnityEngine;

public class TouchControl : MonoBehaviour
{
    [SerializeField] private float _targetHorizontalSpeed = 10.0f;
    [SerializeField] private float _targetVerticalSpeed = 5.0f;
    [SerializeField] private float _minX = -5.0f;
    [SerializeField] private float _maxX = 5.0f;
    private float _horizontalSpeed;
    public float _verticalSpeed;
    private float _touchX;

    private void Start()
    {
        _horizontalSpeed = _targetHorizontalSpeed;
        _verticalSpeed = _targetVerticalSpeed;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                _touchX = touch.deltaPosition.x;
                transform.Translate(0f, _touchX * _horizontalSpeed * Time.deltaTime, 0f);
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, _minX, _maxX),
                    transform.position.y,
                    transform.position.z
                );
            }
        }
        transform.Translate(0f,0f,-_verticalSpeed * Time.deltaTime);
    }

    public void Stop()
    {
        _horizontalSpeed = 0f;
        _verticalSpeed = 0f;
    }
    
    public void Move()
    {
        _horizontalSpeed = _targetHorizontalSpeed;
        _verticalSpeed = _targetVerticalSpeed;
    }
}