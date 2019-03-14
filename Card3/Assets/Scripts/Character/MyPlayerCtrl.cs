using Protocol.Code;
using Protocol.Dto.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerCtrl : CharacterBase
{
    private void Awake()
    {
        Bind(CharacterEvent.INIT_MY_CARD,
            CharacterEvent.ADD_MY_CARD,
            CharacterEvent.DEAL_CARD,
            CharacterEvent.REMOVE_MY_CARD,
            CharacterEvent.ADD_MY_CARDD,
            CharacterEvent.PASS_CREQ,
            CharacterEvent.JUDGE_CARD);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case CharacterEvent.INIT_MY_CARD:
                StartCoroutine(initCardList(message as List<CardDto>));
                break;
            case CharacterEvent.ADD_MY_CARD:
                addTableCard(message as GrabDto);
                break;
            case CharacterEvent.DEAL_CARD:
                dealSelectCard();
                break;
            case CharacterEvent.REMOVE_MY_CARD:
                removeCard(message as List<CardDto>);
                break;
            case CharacterEvent.ADD_MY_CARDD:
                StartCoroutine(addCard(message as CardDto));
                break;
            case CharacterEvent.PASS_CREQ:
                passdealSelectCard();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 自身管理的卡牌列表
    /// </summary>
    private List<CardCtrl> cardCtrlList;

    /// <summary>
    /// 卡牌的父物体
    /// </summary>
    private Transform cardParent;
    private Transform hupaiParent;
    private PromptMsg promptMsg;
    private SocketMsg socketMsg;

    // Use this for initialization
    void Start()
    {
        cardParent = GameObject.Find("MyCardPanel").transform;
        hupaiParent = GameObject.Find("MyCardPanel").transform;
        cardCtrlList = new List<CardCtrl>();

        promptMsg = new PromptMsg();
        socketMsg = new SocketMsg();
    }

    /// <summary>
    /// 出牌 出选中的牌
    /// </summary>
    private void dealSelectCard()
    {
        List<CardDto> selectCardList = getSelectCard();
        DealDto dto = new DealDto(selectCardList, Models.GameModel.Id);
        
    
        //进行合法判断
        if (dto.IsRegular == false)
        {
            //如果出牌不合法  2 4
            promptMsg.Change("请选择合理的手牌！", Color.red);

            Dispatch(AreaCode.UI, UIEvent.SHOW_DEAL_BUTTON, true);
            //    出牌不合法    不用发送消息   直接
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }
        else
        {
            //可以出牌
            //向服务器发送出牌命令
            socketMsg.Change(OpCode.FIGHT, FightCode.DEAL_CREQ, dto);
            Dispatch(AreaCode.NET, 0, socketMsg);
           // Dispatch(AreaCode.UI, UIEvent.SHOW_DEAL_BUTTON, false);
        }
    }

    /// <summary>
    /// 获取选中的牌
    /// </summary>
    private List<CardDto> getSelectCard()
    {
        List<CardDto> selectCardList = new List<CardDto>();
        foreach (var cardCtrl in cardCtrlList)
        {
            if (cardCtrl.Selected == true)
            {
                selectCardList.Add(cardCtrl.CardDto);
            }
        }
        return selectCardList;
    }

    /// <summary>
    /// 移除卡牌的游戏物体
    /// </summary>
    private void removeCard(List<CardDto> remainCardList)
    {
        int index = 0;
        foreach (var cc in cardCtrlList)
        {
            if (remainCardList.Count == 0)
                break;
            else
            {
                cc.gameObject.SetActive(true);
                cc.Init(remainCardList[index], index, true);
                index++;
                //没有牌了
                if (index == remainCardList.Count)
                {
                    break;
                }
            }
        }
        //把index之后的牌 都隐藏掉
        for (int i = index; i < cardCtrlList.Count; i++)
        {
            cardCtrlList[i].Selected = false;
            //Destroy(cardCtrlList[i].gameObject);
            cardCtrlList[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 添加底牌的
    /// </summary>
    /// <param name="cardList"></param>
    private void addTableCard(GrabDto dto)
    {
        List<CardDto> tableCards = dto.TableCardList;
        List<CardDto> playerCards = dto.PlayerCardList;

        //复用之前创建的卡牌
        int index = 0;
        foreach (var cardCtrl in cardCtrlList)
        {
            cardCtrl.gameObject.SetActive(true);
            cardCtrl.Init(playerCards[index], index, true);
            //if (tableCards.Contains())
            //    cardCtrl.SelectState();
            index++;
        }
        //再创建新的三张卡牌
        GameObject cardPrefab = Resources.Load<GameObject>("Card/MyCard2");
        for (int i = index; i < playerCards.Count; i++)
        {
            createGo(playerCards[i], i, cardPrefab);
        }
    }


    /// <summary>
    /// 初始化显示卡牌
    /// </summary>
    private IEnumerator initCardList(List<CardDto> cardList)
    {
        GameObject cardPrefab = Resources.Load<GameObject>("Card/MyCard2");

        for (int i = 0; i < cardList.Count; i++)
        {
            createGo(cardList[i], i, cardPrefab);
            yield return new WaitForSeconds(0.1f);
        }
    }
    /// <summary>
    /// 初始化显示卡牌
    /// </summary>
    private IEnumerator addCard(CardDto cardList)
    {
        GameObject cardPrefab = Resources.Load<GameObject>("Card/MyCard2");
        createGo(cardList, 14, cardPrefab);
        yield return new WaitForSeconds(0.1f);
    }
    /// <summary>
    /// 创建卡牌游戏物体
    /// </summary>
    /// <param name="card"></param>
    /// <param name="index"></param>
    private void createGo(CardDto card, int index, GameObject cardPrefab)
    {
        GameObject cardGo = Object.Instantiate(cardPrefab, cardParent) as GameObject;
        cardGo.name = card.Name;
        cardGo.transform.localPosition = new Vector2((0.2f * index), 0);
        CardCtrl cardCtrl = cardGo.GetComponent<CardCtrl>();
        cardCtrl.Init(card, index, true);

        //存储本地
        this.cardCtrlList.Add(cardCtrl);
    }
    /// <summary>
    /// 出牌 出选中的牌
    /// </summary>
    private void passdealSelectCard()
    {
        List<CardDto> selectCardList = passgetSelectCard();
        DealDto dto = new DealDto(selectCardList, Models.GameModel.Id);


        //进行合法判断
        if (dto.IsRegular == false)
        {
            //如果出牌不合法  2 4
            promptMsg.Change("请选择合理的手牌！", Color.red);

            Dispatch(AreaCode.UI, UIEvent.SHOW_DEAL_BUTTON, true);
            //    出牌不合法    不用发送消息   直接
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }
        else
        {
            //可以出牌
            //向服务器发送出牌命令
            socketMsg.Change(OpCode.FIGHT, FightCode.DEAL_CREQ, dto);
            Dispatch(AreaCode.NET, 0, socketMsg);
            // Dispatch(AreaCode.UI, UIEvent.SHOW_DEAL_BUTTON, false);
        }
    }

    /// <summary>
    /// 获取选中的牌
    /// </summary>
    private List<CardDto> passgetSelectCard()
    {
        List<CardDto> selectCardList = new List<CardDto>();
        selectCardList.Add(cardCtrlList[cardCtrlList.Count - 1].CardDto);
        return selectCardList;
    }

   
}
