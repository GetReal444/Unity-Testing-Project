using UnityEngine;

public class DrawVisuals : MonoBehaviour
{
    [Rename("Player")]
    [SerializeField]private CapsuleCollider cc;

    private void Awake()
    {
        cc = GetComponent<CapsuleCollider>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 center = transform.position + cc.center;
        Vector3 rectangle = new Vector3(1f, 2.55f, 1f);

        Gizmos.DrawWireCube(center, rectangle);
    }
}

