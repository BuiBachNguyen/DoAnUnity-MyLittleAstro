using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField] Image selector;
    Vector3 offset = new Vector3(250, 0, 0);

    [SerializeField] List<Button> butons;
    //[SerializeField] Button startBtn;
    //[SerializeField] Button optionBtn;
    //[SerializeField] Button quitBtn;
    [SerializeField] int index = 0;   // Start = 0, Option = 1, Quit = 2
    [SerializeField] int maxIndex = 2;


    float moveCooldown = 0.2f;
    float lastMoveTime;

    private void Awake()
    {
        index = 0;
        selector.transform.position = butons[index].transform.position;
    }

    void Update()
    {
        if (Time.time - lastMoveTime < Time.deltaTime)
            return;

        // A , W, up, left
        if ((Input.GetKeyDown(KeyCode.W)) || Input.GetKeyDown(KeyCode.A)
             || (Input.GetKeyDown(KeyCode.UpArrow)) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index--;
            lastMoveTime = Time.time;
        }
        // D hoặc S
        if ((Input.GetKeyDown(KeyCode.S)) || Input.GetKeyDown(KeyCode.D)
             || (Input.GetKeyDown(KeyCode.DownArrow)) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;
            lastMoveTime = Time.time;
        }

        MoveToIndex();

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ActivateCurrent();
        }
    }

    void ActivateCurrent()
    {
        //switch (index)
        //{
        //    case 0: startBtn.onClick.Invoke(); break;
        //    case 1: optionBtn.onClick.Invoke(); break;
        //    case 2: quitBtn.onClick.Invoke(); break;
        //}

        butons[index].onClick.Invoke();
    }

    public void fStart()
    {
        Debug.Log("ST");
        index = 0;
        SceneMng.Instance.NextLevel();
    }
    public void fOption()
    {
        Debug.Log("OP");
        index = 1;
    }
    public void fQuit()
    {
        index = 2;
        Application.Quit();
    }

    void MoveToIndex()
    {

        // wrap index
        if (index < 0)
            index = maxIndex;
        else if (index > maxIndex)
            index = 0;

        selector.transform.position = butons[index].transform.position;
        
    }    

}


