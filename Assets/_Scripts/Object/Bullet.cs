using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject portal;
    float speed = 10.0f;
    float offset = -0.25f;


    void Start()
    {

    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void SetRef(GameObject portal)
    {
        if (portal == null) return;
        if (this.portal == null || this.portal != portal)
            this.portal = portal;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tags.Ground)) return;

        Debug.Log("Collide");

        portal.SetActive(true);
        gameObject.SetActive(false);

        Vector2 delta = transform.position - collision.bounds.center;

        // 1. Snap hướng va chạm về 4 hướng
        Vector2 dir;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            // LEFT / RIGHT
            dir = new Vector2(Mathf.Sign(delta.x), 0);
        }
        else
        {
            // UP / DOWN
            dir = new Vector2(0, Mathf.Sign(delta.y));
        }

        // 2. Đẩy portal ra khỏi tường
        portal.transform.position =
            transform.position + (Vector3)(dir * offset);

        // 3. Xoay portal đúng góc
        portal.transform.rotation = DirectionToRotation(dir);
    }
    Quaternion DirectionToRotation(Vector2 dir)
    {
        if (dir == Vector2.right) return Quaternion.Euler(0, 0, 180f);
        if (dir == Vector2.left) return Quaternion.Euler(0, 0, 0f);
        if (dir == Vector2.up) return Quaternion.Euler(0, 0, -90f);
        if (dir == Vector2.down) return Quaternion.Euler(0, 0, 90f);

        return Quaternion.identity;
    }

}
