using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int hiddenpoint = 0;
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public P_MOVE player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIHiddenPoint;
    public Text UIstage;

    public Text questText;

    public TypeEffect talk;
    public int talkIndex;
    public bool isAction;
    public Animator talkPanel;
    public TalkManager talkManager;
    public QuestManager questManager;
    public Image portraitImg;

    public GameObject menuset;
    public GameObject playerr;



    public GameObject scanObject;
    public GameObject UIRestartBtn;
    // Start is called before the fi
    // rst frame update


    void Start()
    {
        questText.text = questManager.CheckQuest();
    }

    void Update()
    {


        UIPoint.text = (totalPoint + stagePoint + stagePoint).ToString();
        UIHiddenPoint.text = hiddenpoint + " / 1";

        if (Input.GetButtonDown("Cancel"))
        {
            if (menuset.activeSelf)
            menuset.SetActive(false);
            else 
                menuset.SetActive(true);    
            
        }

        }





    public void Action(GameObject scanObj)
    {

            scanObject = scanObj;
            ObjData objData = scanObject.GetComponent<ObjData>();
            Talk(objData.id, objData.isNPC);


        talkPanel.SetBool("isShow", isAction);
 
    }



    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";

        //set talk Data
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {

            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        }

        //End Talk
        if (talkData == null)
        { 
            isAction = false;
            talkIndex = 0;
            questText.text = questManager.CheckQuest(id);   
            return;
        }


        //Continue Talk
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]);

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);

        }
        else
        {
            talk.SetMsg(talkData);
            portraitImg.color = new Color(1, 1, 1, 0);
            
        }

       
        isAction = true;
        talkIndex++;
    }

    public void Nextstage()
    {
        //ChangeStage
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].gameObject.SetActive(false);
            stageIndex++;
            Stages[stageIndex].gameObject.SetActive(true);
            PlayerReposition();

            if (stageIndex == 1)
                UIstage.text = "Green Forest";
            else if (stageIndex == 2)
                UIstage.text = "Slime Hive";
            else if (stageIndex == 3)
                UIstage.text = "BOSS - Mushroom";
        }
        else
        { //game clear
            //player control lock
            Time.timeScale = 0;
            //Result UI
            Debug.Log("게임 클리어!");
            //Restatr Button UI
            UIRestartBtn.SetActive(true);
            Text btnText = UIRestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear!";
            UIRestartBtn.SetActive(true);
        }
        //calculate
        totalPoint += stagePoint;
        stagePoint = 0;
        hiddenpoint = 0;

    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {

            //All Health UI off
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            //Player Die Efeect
            player.OnDie();

            Debug.Log("죽었습니다!");

            //retry Button UI
            UIRestartBtn.SetActive(true);
        }
    }

    public void HealthUp()
    {
        if (health < 3)
        {
            UIhealth[health].color = new Color(1, 1, 1, 1);
            health++;

        }
    }

    // Update is called once per frame

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (health > 1)
            {
                PlayerReposition();
            }


            HealthDown();
        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void GmaeSave()
    {
        PlayerPrefs.SetFloat("PlayerX", playerr.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", playerr.transform.position.y);
        PlayerPrefs.SetInt("QustId", questManager.questId);
        PlayerPrefs.SetInt("QustActionIndex", questManager.questActionIndex);
        PlayerPrefs.SetInt("Health", health);
        PlayerPrefs.SetInt("StageIndex", stageIndex);
        PlayerPrefs.Save();

        menuset.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        float xx = PlayerPrefs.GetFloat("PlayerX");
        float yy = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QustId");
        int questActionIndex = PlayerPrefs.GetInt("QustActionIndex");
        int healthh = PlayerPrefs.GetInt("Health");
        int stageIndexx = PlayerPrefs.GetInt("StageIndex");

        playerr.transform.position = new Vector3(xx, yy, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        health = healthh;
        stageIndex = stageIndexx;
        questManager.ControlObject();



        for (int i = 0; i < Stages.Length; i++)
            Stages[i].gameObject.SetActive(false);


        Stages[stageIndex].gameObject.SetActive(true);


    }


    public void GameExit()
    {
        Application.Quit();
    }


}
