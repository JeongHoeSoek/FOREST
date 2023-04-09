using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public GameManager gameManager;

    public Sprite[] portraitArr;


    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();

    }

    void GenerateData()
    {
        talkData.Add(100, new string[] { "�ڳ� ���� ���°ǰ�?:0"});

        talkData.Add(200, new string[] { "Ŀ�� ���� �ϽǷ���?\n���� ȸ�� Ŀ�Ǵ� �ƽôٽ��� ���ִ�ϴ�.:0"});

        talkData.Add(500, new string[] { "Ŀ�Ǹ� ����� ���Ŵٴ� �ҹ��� �ִ� �̴븮���� Ŀ�Ǵ�." });

        talkData.Add(600, new string[] { "�� �����ִ��� �𸣰����� ������ ���δ�.\n...�׷��� �̷� ���� ���� �־�����?" });

        talkData.Add(1000, new string[] { "...ǫ���� �����̴�.\n�̰� ��� ������ ������ �ʾҴٸ� ���������� �𸥴�." });


        talkData.Add(1100, new string[] { "...���� ����?\n....��?\n�� �ʷϻ� ����ü�� ����!?:0",
            "(ó�� ���� ���� ������ ���ε� ǳ���� �ڹٲ�� �־���.)\n(���̴°� ���� ������, ...��°�� �ִ��� �� �츮 ȸ���� Ŀ�� ���̾���.):0" });


        talkData.Add(1200, new string[] { "...ź���� ���� ���̴� ��Ǯ�̴�.\n�̰� ��� �ٸ� ���� ������ �� ������ ����." });

        talkData.Add(1300, new string[] { "...�� �ʷϻ� �������� ���� �Ǻΰ� ������.\n��û�ϰ� ���ܼ� ����ߴµ�:0",
            "...���°� ���� ���� �ƴϳ�.:1", "(�տ� �Ǵٸ� ������ ���δ�)\n(�ǵ����̸� ��� ��������):0" });


        talkData.Add(1400, new string[] { "...�繫�ǿ��� �����ִ� ���̴�.\n���� �ٽ� ���ư� �� �������� �𸥴�", "���� ������ ȸ��� ���ư��� �;����� ���� ������..." });

        talkData.Add(2000, new string[] { "...���� ƴ ���̷� �� �ڱ׸��� �����̴�.\n'�پ���' ���� �ٸ������� �� �� �������� �𸥴�." });
        talkData.Add(2100, new string[] { "...�þ� ���� �Ǵٸ� ���� ��������, �������� ��۴��\n�ٸ� ���� ã�ƺ��°� ���������� �𸥴�." });
        portraitData.Add(100 + 0, portraitArr[0]);
        portraitData.Add(100 + 1, portraitArr[1]);
        portraitData.Add(100 + 2, portraitArr[2]);
        portraitData.Add(100 + 3, portraitArr[3]);
        portraitData.Add(200 + 0, portraitArr[4]);
        portraitData.Add(200 + 1, portraitArr[5]);
        portraitData.Add(200 + 2, portraitArr[6]);
        portraitData.Add(200 + 3, portraitArr[7]);

        portraitData.Add(1100 + 0, portraitArr[8]);

        portraitData.Add(1300 + 0, portraitArr[8]);
        portraitData.Add(1300 + 1, portraitArr[5]);

        //Quest Talk
        talkData.Add(10 + 100, new string[]
            {

            "���� ���ϰ� �ֳ�?:0", "�� �����. �� 67�ð� ° ������Դϴ�...\n���ڰ� �� ���� �� �� ����... :1",
            "��û�ϱ�!!\n�׷��� Ŀ�Ǹ� ���ð� ���ϸ� ���ݳ�!:2","��ġ��, �̴븮���� ������ �޾ƿ�!!!:3"}
            
            );

        talkData.Add(11 + 200, new string[]
    {
            "���� ������ �Ծ��?:0", "��... �׷��� ������ ������ ���ƿ�.:1",
            "����...\n���� Ŀ�Ƕ� ��ð� ������.:2","�븮�� �� ��¥ ���� ������ ��...:1", "��ð� ��ô� ���� ������ ã�ƺ��Կ�.:2"}
    );


        talkData.Add(20 + 200, new string[] 
        {
             "���� ���� ���� ���� Ŀ�ǰ� [ġ���]����.:2",
        });

        talkData.Add(20 + 500, new string[]
        {
             "���������� ī����, Ŀ�Ǹ� ���̴�.\nĿ�Ǹ� �����ϴ� ȸ��ٺ��� ī������ �η��ִ�.",
             "�޽� �ð��� ������...\n(Ŀ�Ǵ� ü���� ȸ�������ݴϴ�.)\n(������ �帵ũ�� 15�ʰ� ���� Ű���ݴϴ�.)"
    });


        talkData.Add(21 + 200, new string[]
{
             "...������ �и� ���⿡ �״µ�:0", "���� ������ �ֳ���?:1", "�˼������� ������ �Ⱥ��̳׿�.\nȤ�� �𸣴� ���� �繫�� �� Ȯ���� �ֽǷ���?\n���� ���� �� �� �������Կ�.:0",
});


    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                //�⺻ ��縦 ������ �´�.
                  return GetTalk(id - id % 100, talkIndex);  
            }
            else 
            {
                //����Ʈ �� ó�� ��縦 ������ �´�
                return GetTalk(id - id % 10, talkIndex);
            }
        
        
        }


        
        if (talkIndex == talkData[id].Length)
            return null;
        else
        return talkData[id][talkIndex];
    }


    public Sprite GetPortrait(int id, int portraitindex)
    {
        return portraitData[id + portraitindex];
    }

}
