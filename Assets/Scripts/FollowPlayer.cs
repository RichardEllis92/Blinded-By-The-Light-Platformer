using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, -10f, 100f), Mathf.Clamp(player.position.y, 23f, 1000f), transform.position.z);
    }
}