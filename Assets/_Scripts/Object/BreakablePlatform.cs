using System.Collections;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private PlatformState state = PlatformState.UnTouch;

    private Collider2D col;
    private Animator anim;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Player)) return;
        if (state != PlatformState.UnTouch) return;

        StartCoroutine(BreakRoutine());
        
    }

    private IEnumerator BreakRoutine()
    {
        // 1
        state = PlatformState.Triggered;
        yield return new WaitForSeconds(2f);

        // 2
        state = PlatformState.Breaking;
        anim.SetTrigger("Breaking");
        yield return new WaitForSeconds(1.0f);
        //yield return WaitForAnimation("Breaking");
        col.enabled = false;

        // 3
        state = PlatformState.Recovery;
        yield return new WaitForSeconds(3f);


        // 4. Recovering
        state = PlatformState.Recovering;
        anim.SetTrigger("Recovering");
        yield return new WaitForSeconds(0.75f);
        col.enabled = true;

        // 5. 
        state = PlatformState.UnTouch;
    }
}

public enum PlatformState
{
    UnTouch,
    Triggered,
    Breaking,
    Recovery,
    Recovering,
}
