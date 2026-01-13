using System;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public void _fStart()
    {
        Debug.Log("ST");
        SceneMng.Instance.NextLevel();
    }
}
