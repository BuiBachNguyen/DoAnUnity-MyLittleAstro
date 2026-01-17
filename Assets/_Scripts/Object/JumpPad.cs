using Unity.VisualScripting;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float boostForce = 10.0f;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Player))
            return;
        Rigidbody2D _rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (_rb == null) return;

        _rb.linearVelocity = new Vector2(0, 0);
        _rb.AddForce(new Vector2 (0.0f, boostForce), ForceMode2D.Impulse);
        anim.SetTrigger("Triggered");
        AudioManager.Instance.PlaySFX(AudioClipNames.JumpPad);
    }

}
