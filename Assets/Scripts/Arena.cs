using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform topWall;
    [SerializeField] private Transform ground;

    public float farFromGround { get; private set; } = 8;
    public float farFromWall { get; private set; }  = 8;
    public float farFromCeiling { get; private set; }  = 6;

    public Vector3 GetLeftWallPos() => leftWall.position;
    public Vector3 GetRightWallPos() => rightWall.position;
    public Vector3 GetTopWallPos() => topWall.position;
    
    public Vector3 ClampInsideArena(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, leftWall.position.x + farFromWall, rightWall.position.x - farFromWall);
        position.y = Mathf.Clamp(position.y, ground.position.y, topWall.position.y - farFromCeiling);
        return position;
    }
}
