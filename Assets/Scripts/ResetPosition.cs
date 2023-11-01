using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [Rename("Respawn Point")]
    [SerializeField] private Transform rp;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //gets player's transform component and changes player's location to respawn point's location
            Transform playerTransform = collision.transform;

            playerTransform.position = rp.position;
        }
    }
}