using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private LayerMask collisionLayer;
    [SerializeField]
    private float moveSpeed = 5;
    private Vector3 velocity;
    private float skinWidth = 0.015f;
    private CapsuleCollider2D capsuleCollider2D;
    private void Awake()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        Vector3 currentVelocity = velocity * Time.deltaTime;
        
        if( currentVelocity.x != 0)
        {
            RaycastsHorizontal(ref currentVelocity);
        }

        transform.position += currentVelocity;
    }
    private void OnDrawGizmos()
    {
        Bounds bounds = capsuleCollider2D.bounds;
        bounds.Expand(skinWidth * -2);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector2(bounds.min.x, bounds.center.y), 0.1f);
        Gizmos.DrawSphere(new Vector2(bounds.max.x, bounds.center.y), 0.1f);
    }
    public void MoveTo(float x)
    {
        velocity.x = x * moveSpeed;
    }
private void RaycastsHorizontal(ref Vector3 velocity)
{
    float direction = Mathf.Sign(velocity.x);
    float distance = Mathf.Abs(velocity.x) + skinWidth;

    Bounds bounds = capsuleCollider2D.bounds;
    bounds.Expand(skinWidth * -2);

    Vector2 leftPosition = new Vector2(bounds.min.x, bounds.center.y);
    Vector2 rightPosition = new Vector2(bounds.max.x, bounds.center.y);
    Vector2 rayPosition = direction==1?rightPosition:leftPosition;

    RaycastHit2D hit = Physics2D.Raycast(rayPosition, Vector2.right * direction, distance, collisionLayer);

    if(hit)
    {
        velocity.x = (hit.distance - skinWidth) * direction;
    }
    Debug.DrawLine(rayPosition, rayPosition + Vector2.right*direction*distance, Color.yellow);
}
}