using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float posX;
    [SerializeField] private float posY;
    [SerializeField] private float posZ;

    [SerializeField] private float m_Speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(
            player.position.x + posX,
            player.position.y + posY,
            player.position.z + posZ
        ), m_Speed * Time.deltaTime);
    }
}
