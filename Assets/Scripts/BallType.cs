using UnityEngine;

[CreateAssetMenu(fileName = "New Ball Type",menuName = "Ball Type")]
public class BallType : ScriptableObject
{
    public Material ballColor;
    public Vector3 ballVolume;
    public float ballMass;
}
