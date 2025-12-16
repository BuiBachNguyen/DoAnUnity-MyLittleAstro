using UnityEngine;

public class Plate : MonoBehaviour
{

    [SerializeField] Door door;

    [SerializeField] bool isHolding = false;


    private void Awake()
    {
        
    }

    public bool IsHolding
    {
        get { return isHolding; }
        set { isHolding = value; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHolding = true;
        door.OnChecking();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        isHolding = true;
        door.OnChecking();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isHolding = false;
        door.OnChecking();
    }

}
