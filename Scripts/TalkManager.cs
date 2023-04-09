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
        talkData.Add(100, new string[] { "자네 지금 쉬는건가?:0"});

        talkData.Add(200, new string[] { "커피 한잔 하실래요?\n저희 회사 커피는 아시다시피 맛있답니다.:0"});

        talkData.Add(500, new string[] { "커피를 물대신 마신다는 소문이 있는 이대리님의 커피다." });

        talkData.Add(600, new string[] { "왜 여기있는지 모르겠지만 서류가 보인다.\n...그런데 이런 곳에 문이 있었던가?" });

        talkData.Add(1000, new string[] { "...푹신한 버섯이다.\n이게 쿠션 역할을 해주지 않았다면 다쳤을지도 모른다." });


        talkData.Add(1100, new string[] { "...여긴 어디야?\n....숲?\n저 초록색 생물체는 뭐고!?:0",
            "(처음 보는 문을 열었을 뿐인데 풍경이 뒤바뀌어 있었다.)\n(보이는건 숲과 괴물들, ...어째서 있는지 모를 우리 회사의 커피 뿐이었다.):0" });


        talkData.Add(1200, new string[] { "...탄력이 좋아 보이는 수풀이다.\n이걸 밟고 뛰면 높게 점프할 수 있을거 같다." });

        talkData.Add(1300, new string[] { "...저 초록색 생물한테 닿은 피부가 따가워.\n멍청하게 생겨서 방심했는데:0",
            "...아픈걸 보니 꿈은 아니네.:1", "(앞에 또다른 괴물이 보인다)\n(되도록이면 밟고 지나가자):0" });


        talkData.Add(1400, new string[] { "...사무실에서 본적있는 문이다.\n열면 다시 돌아갈 수 있을지도 모른다", "설마 스스로 회사로 돌아가고 싶어지는 날이 올줄은..." });

        talkData.Add(2000, new string[] { "...나무 틈 사이로 난 자그마한 샛길이다.\n'뛰어들면' 전혀 다른곳으로 갈 수 있을지도 모른다." });
        talkData.Add(2100, new string[] { "...시야 끝에 또다른 문이 잡히지만, 괴물들이 드글댄다\n다른 길을 찾아보는게 현명할지도 모른다." });
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

            "지금 뭐하고 있나?:0", "저 사장님. 저 67시간 째 출근중입니다...\n안자고 더 일을 할 수 없ㅇ... :1",
            "멍청하긴!!\n그러면 커피를 마시고 일하면 되잖나!:2","닥치고, 이대리한테 서류나 받아와!!!:3"}
            
            );

        talkData.Add(11 + 200, new string[]
    {
            "서류 가지러 왔어요?:0", "네... 그런데 그전에 죽을거 같아요.:1",
            "저런...\n옆에 커피라도 드시고 가세요.:2","대리님 저 진짜 졸려 죽을거 같...:1", "드시고 계시는 동안 서류를 찾아볼게요.:2"}
    );


        talkData.Add(20 + 200, new string[] 
        {
             "몸에 힘이 없을 때는 커피가 [치료약]이죠.:2",
        });

        talkData.Add(20 + 500, new string[]
        {
             "증오스러운 카페인, 커피를 마셨다.\n커피를 제작하는 회사다보니 카페인은 널려있다.",
             "휴식 시간은 없지만...\n(커피는 체력을 회복시켜줍니다.)\n(에너지 드링크는 15초간 힘을 키워줍니다.)"
    });


        talkData.Add(21 + 200, new string[]
{
             "...서류를 분명 여기에 뒀는데:0", "무슨 문제라도 있나요?:1", "죄송하지만 서류가 안보이네요.\n혹시 모르니 우측 사무실 좀 확인해 주실래요?\n저는 여기 좀 더 뒤져볼게요.:0",
});


    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                //기본 대사를 가지고 온다.
                  return GetTalk(id - id % 100, talkIndex);  
            }
            else 
            {
                //퀘스트 맨 처음 대사를 가지고 온다
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
