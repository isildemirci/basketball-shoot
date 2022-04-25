using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 _startingPosition;
    [SerializeField] private Vector3 movementVector;
    private float _movementFactor;
    [SerializeField] private float period = 2f;
    
    void Start()
    {
        _startingPosition = transform.position;
    }

    void Update()
    {
        if(period <= Mathf.Epsilon) { return; } //if period is <= Mathf.Epsilon, just dont do any of this following stuff
        float cycles = Time.time / period; //continually growing over time
        
        const float tau = (Mathf.PI * 2); //constant value for 6.283
        float rawWaveSin = Mathf.Sin(cycles * tau); //going from -1 to 1

        _movementFactor = (rawWaveSin + 1) / 2; //recalculated to go from 0 to 1 so its clear
        
        Vector3 offset = movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}