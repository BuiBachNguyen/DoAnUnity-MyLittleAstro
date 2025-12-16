using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] List<Plate> Plates;
    Collider2D _col;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
    }

    void Start()
    {
        if(Plates.Count <= 0)
        {
            Debug.Log("Null ref of plates");
        }
    }

    public void OnChecking()
    {
        foreach (var plate in Plates)
        {
            if (plate.IsHolding == true)
                continue;
            else
            {
                _col.enabled = true;
                Debug.Log("close");
                return;
            }
        }
        _col.enabled = false;
        Debug.Log("open");
    }
}
