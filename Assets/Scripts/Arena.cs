using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;

    public Vector3 GetLeftWallPos() => leftWall.position;
    public Vector3 GetRightWallPos() => rightWall.position;
}
