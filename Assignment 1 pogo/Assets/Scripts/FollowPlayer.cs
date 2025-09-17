using JetBrains.Annotations;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    Vector3 offset = new Vector3(0, 1, -10);
    void LateUpdate()
    {
        if (Player != null)
        {
            transform.position = Player.position + offset;
        }
        
    }
}
