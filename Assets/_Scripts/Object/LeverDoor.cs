using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Jobs;

public class LeverDoor : MonoBehaviour
{
    [SerializeField] bool isOpening = false;
    [SerializeField] private List<Lever> levers= new();

    private Animator anim;
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (levers == null || levers.Count == 0)
        {
            Debug.LogWarning($"{name}: No levers assigned");
            return;
        }

        foreach (var plate in levers)
        {
            plate.OnActivatedChanged += OnLeverChanged;
        }

        CheckDoor();
    }

    private void OnDisable()
    {
        if (levers == null) return;

        foreach (var plate in levers)
        {
            plate.OnActivatedChanged -= OnLeverChanged;
        }
    }

    private void OnLeverChanged(bool _)
    {
        CheckDoor();
    }

    private void CheckDoor()
    {
        bool allActivated = levers.All(p => p.IsActivated);

        if (isOpening == allActivated) return;

        col.enabled = !allActivated;
        isOpening = allActivated;
        anim.SetBool("isOpening", isOpening);
        //Debug.Log(allActivated ? "open" : "close");
        AudioManager.Instance.PlaySFX(AudioClipNames.Door);
    }
}
