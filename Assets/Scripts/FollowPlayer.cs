using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, -10f, 100f), transform.position.y, transform.position.z);
    }
}