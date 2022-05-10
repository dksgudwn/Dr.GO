using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement2D movement2D;
    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
    }
    private void Update()
    {
        UpdateMove();
    }
    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        movement2D.MoveTo(x);
    }
}
