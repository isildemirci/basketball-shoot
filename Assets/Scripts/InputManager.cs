using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float throwForceInXY = 1f;
    [SerializeField] private float throwForceInZ = 50f;
    
    private Vector2 _startPos, _endPos, _direction;
    private float _touchTimeStart, _touchTimeFinish, _timeInterval;
    private bool _isTouchEnded = false;
    public bool IsTouchEnded => _isTouchEnded;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.touchCount <= 0) return;
        Touch touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                _touchTimeStart = Time.time;
                _startPos = Input.GetTouch(0).position;
                _isTouchEnded = false;
                break;
            case TouchPhase.Ended:
                _isTouchEnded = true;
                _touchTimeFinish = Time.time;
                _endPos = Input.GetTouch(0).position;
            
                _timeInterval = _touchTimeFinish - _touchTimeStart;
                _direction = _startPos - _endPos;
                _rb.isKinematic = false;

                _rb.AddForce(-_direction.x * throwForceInXY, -_direction.y * throwForceInXY, throwForceInZ / _timeInterval);
                gameObject.tag = "Spawned";
                throwForceInZ = 0;
                throwForceInXY = 0;
                Invoke("DisableBall", 3f);
                break;
        }
    }

    private void DisableBall()
    {
        gameObject.SetActive(false);
    }
}