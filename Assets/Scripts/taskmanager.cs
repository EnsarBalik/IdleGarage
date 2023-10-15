using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taskmanager : MonoBehaviour {

    public static taskmanager taskmanagersc;

    [SerializeField] GameObject[] tasks;
    [SerializeField] GameObject[] taskobjects;

    private void Awake()
    {
        taskmanagersc = this;
    }

    public void updatetaskselector()
    {
        taskselector(PlayerPrefs.GetInt("gorev"), PlayerPrefs.GetInt("gorev2"));
    }

    void Start()
    {
        if (PlayerPrefs.GetFloat("taskcompleted") != 1f)
            updatetaskselector();
        else
            tasks[tasks.Length - 1].gameObject.SetActive(true);
    }

    public void taskselector(int whichtask, int twotask)
    {
        foreach (var item in tasks)
        {
            item.SetActive(false);
        }

        if (whichtask != -1f)
            tasks[whichtask].gameObject.SetActive(true);

        if (twotask != 0f)
            tasks[twotask].gameObject.SetActive(true);
    }

    public void sellareaplacechek()
    {
        if (PlayerPrefs.GetInt("gorev") >= 1)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("CollectArea1");

            if(objects.Length > 2)
            {
                PlayerPrefs.SetInt("gorev", 2);
                updatetaskselector();
            }
        }
    }

    public void objectactiver(int a, int b)
    {
        foreach (var item in taskobjects)
        {
            item.gameObject.SetActive(false);
        }

        if (a != -1f)
            taskobjects[a].gameObject.SetActive(true);

        if (b != 0f)
            taskobjects[b].gameObject.SetActive(true);
    }

    private void Update()
    {
        switch (PlayerPrefs.GetInt("gorev"))
        {
            case 0:
                objectactiver(0, 1);
                break;

            case 1:
                objectactiver(-1, 2);
                break;

            case 2:
                objectactiver(3, 0);
                break;

            case 3:
                objectactiver(-1, 0);
                break;

            default:
                break;
        }

        if(PlayerPrefs.GetInt("gorev2") >= 2f)
        {
            PlayerPrefs.GetFloat("taskcompleted", 1f);
            taskselector(-1, 0);
            tasks[tasks.Length - 1].gameObject.SetActive(true);
        }
    }
}
