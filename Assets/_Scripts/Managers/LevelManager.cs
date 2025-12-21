using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<LevelBlock> levels;

    private void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for(int i = 0; i < unlockedLevel; i++)
        {
            levels[i].ChangeStatus(false);
        } 
            
    }

    void HandleLevelClicked(int levelIndex)
    {
        if (loadRoutine != null) return;

        loadRoutine = StartCoroutine(Load(levelIndex));
    }

    private void OnEnable()
    {
        LevelBlock.OnLevelClicked += HandleLevelClicked;
    }

    private void OnDisable()
    {
        LevelBlock.OnLevelClicked -= HandleLevelClicked;
    }

    Coroutine loadRoutine;
    IEnumerator Load(int levelIndex)
    {
        Debug.Log("load " + levelIndex);
        SceneMng.Instance.LeadSceneWithIndex(levelIndex);
        yield return new WaitForSeconds(2.0f);
        loadRoutine = null;
    }
}
