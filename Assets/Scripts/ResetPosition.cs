using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [Rename("Respawn Point")]
    [SerializeField] private Transform rp;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform playerTransform = collision.transform;

            playerTransform.position = rp.position;
        }
    }
}
