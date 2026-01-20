using System.Collections;
using UnityEngine;

public class TelePortal : MonoBehaviour
{
    [SerializeField] TelePortal other;
    GameObject player;
    Rigidbody2D rigidPlayer;
    Animator animPlayer;
    public TelePortal Other { 
        get { return other; } 
        set { other = value; }
    }

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>()?.gameObject;

        if(player == null)
        {
            Debug.Log("Null Ref ả Portal");
        }    
        rigidPlayer = player.GetComponent<Rigidbody2D>();
        animPlayer = player.GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (other == null) return;
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            if (Vector2.Distance(transform.position, player.transform.position) > 0.3f)
            {
                StartCoroutine(PortalIn());
            }
        }
    }

    IEnumerator PortalIn()
    {
        rigidPlayer.simulated = false;
        player.GetComponent<PlayerController>()
            .Fsm.ChangeState(new PortalInState());
        StartCoroutine(MoveToPortal());
        yield return new WaitForSeconds(0.5f);
        player.transform.position = other.transform.position;
        
        yield return new WaitForSeconds(0.5f);
        rigidPlayer.simulated = true;
        rigidPlayer.linearVelocity = Vector2.zero;
    }
    IEnumerator MoveToPortal()
    {
        float timer = 0;
        float speed = 2.5f;
        while (timer < 0.3f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }    
    }    
}
