using Protocol.Dto.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftPlayerCtrl : CharacterBase
{
    public Text game;
    private void Awake()
    {
        Bind(CharacterEvent.INIT_LEFT_CARD,
            CharacterEvent.ADD_LEFT_CARD,
            CharacterEvent.REMOVE_LEFT_CARD);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case CharacterEvent.INIT_LEFT_CARD:
                StartCoroutine(initCardList());
                break;
            case CharacterEvent.ADD_LEFT_CARD:
                addTableCard();
                break;
            case CharacterEvent.REMOVE_LEFT_CARD:
                removeCard((message as List<CardDto>).Count);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 移除卡牌的游戏物体
    /// </summary>
    private void removeCard(int cardCount)
    {
        Debug.Log("这是LcardCount"+ cardCount.ToString());
        a = cardCount;
        // ***** ******
        for (int i = cardCount; i < cardObjectList.Count; i++)
        {
            
            cardObjectList[i].SetActive(false);
        }
    }
    private void Update()
    {
        game.text = a.ToString();
    }
    /// <summary>
    /// 添加底牌的
    /// </summary>
    /// <param name="cardList"></param>
    private void addTableCard()
    {
        //再创建新的三张卡牌
        GameObject cardPrefab = Resources.Load<GameObject>("Card/OtherCard");
        for (int i = 0; i < 3; i++)
        {
            a += 1;
            //createGo(i, cardPrefab);
        }
    }

   public List<GameObject> cardObjectList;

    /// <summary>
    /// 卡牌的父物体
    /// </summary>
    private Transform cardParent;

    // Use this for initialization
    void Start()
    {
        cardParent = transform.Find("CardPoint");
        cardObjectList = new List<GameObject>();

    }

    private int a;

    /// <summary>
    /// 初始化显示卡牌
    /// </summary>
    private IEnumerator initCardList()
    {
        GameObject cardPrefab = Resources.Load<GameObject>("Card/OtherCard");
        //for (int i = 0; i < 17; i++)
        //{
            a = 17;
            //createGo(1, cardPrefab);
            yield return new WaitForSeconds(0.1f);
        //}
        
    }

    /// <summary>
    /// 创建卡牌游戏物体
    /// </summary>
    /// <param name="card"></param>
    /// <param name="index"></param>
    private void createGo(int index, GameObject cardPrefab)
    {
        GameObject cardGo = Object.Instantiate(cardPrefab, cardParent) as GameObject;
        cardGo.transform.localPosition = new Vector2((0.17f * index), 0);
        cardGo.GetComponent<SpriteRenderer>().sortingOrder = index;
        this.cardObjectList.Add(cardGo);
    }
}
