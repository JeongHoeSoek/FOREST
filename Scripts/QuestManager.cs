using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;
    public GameManager gameManager;

    Dictionary<int, QuestData> questList;

    // Start is called before the first frame update
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }


    void GenerateData()
    { 
        questList.Add(10, new QuestData("회사 사람과 대화하기",new int[] {100, 200} ));
        
        questList.Add(20, new QuestData("커피 마시기", new int[] { 500, 200 }));

        questList.Add(30, new QuestData("서류찾기", new int[] { 0 }));

    }
    // Update is called once per frame
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        //next Talk target
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        //control Quest object
        ControlObject();


        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        //Quest Name
        return questList[questId].questName;
    }

    public string CheckQuest()
    {

        //Quest Name
        return questList[questId].questName;
    }


    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }


    public void ControlObject()
    {
        switch (questId)
        {
            case 10:
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;

            case 20:
                if (questActionIndex == 0)
                    questObject[0].SetActive(true);
                else if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                    break;
                
        }
    }
    }
