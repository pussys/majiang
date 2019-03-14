using Protocol.Code;
using Protocol.Dto;
using Protocol.Dto.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyStatePanel : StatePanel
{
    //private void Awake()
    protected override void Awake()
    {
        base.Awake();
        Bind(UIEvent.SHOW_GRAB_BUTTON,
            UIEvent.SHOW_DEAL_BUTTON,
            //fixbug923
            //UIEvent.SET_MY_PLAYER_DATA,
            UIEvent.PLAYER_HIDE_READY_BUTTON,
            UIEvent.SHOW_HU_CARD,
            UIEvent.SHOW_GANG_CARD,
            UIEvent.SHOW_PENG_CARD);
    }

    public override void Execute(int eventCode, object message)
    {
        base.Execute(eventCode, message);

        switch (eventCode)
        {
            //fixbug923
            //case UIEvent.SET_MY_PLAYER_DATA:
            //    {
            //        this.userDto = message as UserDto;
            //        break;
            //    }
            case UIEvent.SHOW_GRAB_BUTTON://开始抢地主 显示抢地主的按钮
                {
                    bool active = (bool)message;

                    StartCoroutine("Timer");

                    //btnGrab.gameObject.SetActive(active);
                    //btnNGrab.gameObject.SetActive(active);
                    break;
                }
            case UIEvent.SHOW_DEAL_BUTTON://开始出牌 显示出牌按钮
                {
                    bool active = (bool)message;


                    StartCoroutine("Timer1");

                    btnDeal.gameObject.SetActive(active);
                    btnNDeal.gameObject.SetActive(active);
                    break;
                }
            case UIEvent.PLAYER_HIDE_READY_BUTTON://玩家准备 隐藏准备按钮
                {
                    btnReady.gameObject.SetActive(false);
                    break;
                }
            case UIEvent.SHOW_HU_CARD:
                {
                    userId = (DealDto)message;
                    StartCoroutine("Timer2");
                    Hu.gameObject.SetActive(true);
                    Guo.gameObject.SetActive(true);
                    break;
                }
            case UIEvent.SHOW_GANG_CARD:
                {
                    userId = (DealDto)message;
                    StartCoroutine("Timer2");
                    Gang.gameObject.SetActive(true);
                    Guo.gameObject.SetActive(true);
                    break;
                }
            case UIEvent.SHOW_PENG_CARD:
                {
                    userId = (DealDto)message;
                    StartCoroutine("Timer2");
                    Peng.gameObject.SetActive(true);
                    Guo.gameObject.SetActive(true);
                    break;
                }
            default:
                break;
        }
    }

    private Button btnDeal;
    private Button btnNDeal;
    private Button btnGrab;
    private Button btnNGrab;
    private Button btnReady;
    private Button Hu;
    private Button Gang;
    private Button Peng;
    private Button Guo;
    private SocketMsg socketMsg;
    private DealDto userId;
    protected override void Start()
    {
        base.Start();

        btnDeal = transform.Find("btnDeal").GetComponent<Button>();//出牌
        btnNDeal = transform.Find("btnNDeal").GetComponent<Button>();
        btnGrab = transform.Find("btnGrab").GetComponent<Button>();
        btnNGrab = transform.Find("btnNGrab").GetComponent<Button>();
        btnReady = transform.Find("btnReady").GetComponent<Button>();

        Hu = transform.Find("HuPaiPanel/hu").GetComponent<Button>();
        Gang = transform.Find("HuPaiPanel/gang").GetComponent<Button>();
        Peng = transform.Find("HuPaiPanel/peng").GetComponent<Button>();
        Guo = transform.Find("HuPaiPanel/guo").GetComponent<Button>();

        miaobiao = transform.Find("MIAOBIAO").GetComponent<Image>();
        shijian = transform.Find("MIAOBIAO/shijian").GetComponent<Text>();



        btnDeal.onClick.AddListener(dealClick);
        btnNDeal.onClick.AddListener(nDealClick);

        Hu.onClick.AddListener(HuPai);
        Gang.onClick.AddListener(GangPai);
        Peng.onClick.AddListener(PengPai);
        Guo.onClick.AddListener(For);
        //btnGrab.gameObject.SetActive(false);
        //btnNGrab.gameObject.SetActive(false);
        //btnGrab.onClick.AddListener(
        //    delegate ()
        //    {
        //        grabClick(true);
        //    }
        //    );
        //btnNGrab.onClick.AddListener(
        //    () =>
        //    {
        //        grabClick(false);
        //    });

        btnReady.onClick.AddListener(readyClick);

        socketMsg = new SocketMsg();


        //默认状态
        btnGrab.gameObject.SetActive(false);
        btnNGrab.gameObject.SetActive(false);
        btnDeal.gameObject.SetActive(false);
        btnNDeal.gameObject.SetActive(false);

        Hu.gameObject.SetActive(false);
        Gang.gameObject.SetActive(false);
        Peng.gameObject.SetActive(false);
        Guo.gameObject.SetActive(false);


        //fixbug923 
        UserDto myUserDto = Models.GameModel.MatchRoomDto.UIdUserDict[Models.GameModel.UserDto.Id];
        this.userDto = myUserDto;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        btnDeal.onClick.RemoveAllListeners();
        btnNDeal.onClick.RemoveAllListeners();
        btnGrab.onClick.RemoveAllListeners();
        btnNGrab.onClick.RemoveAllListeners();
        btnReady.onClick.RemoveAllListeners();

        Hu.onClick.RemoveAllListeners();
        Gang.onClick.RemoveAllListeners();
        Guo.onClick.RemoveAllListeners();
        Peng.onClick.RemoveAllListeners();

    }

    protected override void readyState()
    {
        base.readyState();
        btnReady.gameObject.SetActive(false);
    }

    private void dealClick()
    {
        btnDeal.gameObject.SetActive(false);
        btnNDeal.gameObject.SetActive(false);
        
        //通知角色模块出牌
        Dispatch(AreaCode.CHARACTER, CharacterEvent.DEAL_CARD, null);

        StopCoroutine("Timer1");
        miaobiao.gameObject.SetActive(false);
        
    }
    
    private void nDealClick()
    {
        //发送不出牌
        Dispatch(AreaCode.CHARACTER, CharacterEvent.PASS_CREQ, null);
        //socketMsg.Change(OpCode.FIGHT, FightCode.PASS_CREQ, null);
        //Dispatch(AreaCode.NET, 0, socketMsg);
        btnDeal.gameObject.SetActive(false);
        btnNDeal.gameObject.SetActive(false);

        StopCoroutine("Timer1");
        miaobiao.gameObject.SetActive(false);
    }

    public void HuPai()
    {
        socketMsg.Change(OpCode.FIGHT, FightCode.HU_CARD, userId);
        Dispatch(AreaCode.NET, 0, socketMsg);

        miaobiao.gameObject.SetActive(false);
        StopCoroutine("Timer2");

        Hu.gameObject.SetActive(false);
        Gang.gameObject.SetActive(false);
        Peng.gameObject.SetActive(false);
        Guo.gameObject.SetActive(false);
    }
    public void GangPai()
    {
        socketMsg.Change(OpCode.FIGHT, FightCode.GANG_CARD, userId);
        Dispatch(AreaCode.NET, 0, socketMsg);

        miaobiao.gameObject.SetActive(false);
        StopCoroutine("Timer2");

        Hu.gameObject.SetActive(false);
        Gang.gameObject.SetActive(false);
        Peng.gameObject.SetActive(false);
        Guo.gameObject.SetActive(false);
    }
    public void PengPai()
    {
        socketMsg.Change(OpCode.FIGHT, FightCode.PENG_CARD, userId);
        Dispatch(AreaCode.NET, 0, socketMsg);

        miaobiao.gameObject.SetActive(false);
        StopCoroutine("Timer2");

        Hu.gameObject.SetActive(false);
        Gang.gameObject.SetActive(false);
        Peng.gameObject.SetActive(false);
        Guo.gameObject.SetActive(false);
    }
    //private void grabClick(bool result)
    //{
    //    //socketMsg.Change(OpCode.FIGHT, FightCode.GRAB_LANDLORD_CREQ, result);
    //    //Dispatch(AreaCode.NET, 0, socketMsg);

    //    if (result == true)
    //    {
    //        //抢地主
    //        socketMsg.Change(OpCode.FIGHT, FightCode.GRAB_LANDLORD_CREQ, true);
    //        Dispatch(AreaCode.NET, 0, socketMsg);

    //        StopCoroutine("Timer");
    //        miaobiao.gameObject.SetActive(false);

    //    }
    //    else
    //    {
    //        //不抢地主
    //        socketMsg.Change(OpCode.FIGHT, FightCode.GRAB_LANDLORD_CREQ, false);
    //        Dispatch(AreaCode.NET, 0, socketMsg);

    //        StopCoroutine("Timer");
    //        miaobiao.gameObject.SetActive(false);

    //    }

    //    //点击之后隐藏两个按钮
    //    btnGrab.gameObject.SetActive(false);
    //    btnNGrab.gameObject.SetActive(false);
    //}
    private void readyClick()
    {
        //向服务器发送准备
        socketMsg.Change(OpCode.MATCH, MatchCode.READY_CREQ, null);
        Dispatch(AreaCode.NET, 0, socketMsg);
    //  btnReady.gameObject.SetActive(false);//--------------------------------------------------------------------------------------------------------------------------------------
    }

    private Image miaobiao;
    private Text shijian;
    private int consideratingTime = 15; //玩家考虑时间



    //public IEnumerator Timer()
    //{
    //    miaobiao.gameObject.SetActive(true);
    //    var time = consideratingTime;
    //    while (time > 0)
    //    {
    //        shijian.text = time.ToString();
    //        yield return new WaitForSeconds(1);
    //        time--;
    //    }
    //    NotBid();
    //}
    public IEnumerator Timer1()
    {
        miaobiao.gameObject.SetActive(true);
        var time = consideratingTime;
        while (time > 0)
        {
            shijian.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }
        ForBid();
    }
    public IEnumerator Timer2()
    {
        miaobiao.gameObject.SetActive(true);
        var time = 10;
        while (time > 0)
        {
            shijian.text = time.ToString();
            yield return new WaitForSeconds(1);
            time--;
        }
        For();
    }
    //public void NotBid()
    //{
    //    miaobiao.gameObject.SetActive(false);
    //    StopCoroutine("Timer");
    //    socketMsg.Change(OpCode.FIGHT, FightCode.GRAB_LANDLORD_CREQ, false);
    //    Dispatch(AreaCode.NET, 0, socketMsg);
    //    btnGrab.gameObject.SetActive(false);
    //    btnNGrab.gameObject.SetActive(false);
    //}
    public void ForBid()
    {
        miaobiao.gameObject.SetActive(false);
        StopCoroutine("Timer1");

        //socketMsg.Change(OpCode.FIGHT, FightCode.PASS_CREQ, null);
        //Dispatch(AreaCode.NET, 0, socketMsg);
        Dispatch(AreaCode.CHARACTER, CharacterEvent.PASS_CREQ, null);
        btnDeal.gameObject.SetActive(false);
        btnNDeal.gameObject.SetActive(false);
    }
    public void For()
    {
        miaobiao.gameObject.SetActive(false);
        StopCoroutine("Timer2");

        socketMsg.Change(OpCode.FIGHT, FightCode.GUO_CARD, userId);
        Dispatch(AreaCode.NET, 0, socketMsg);

        Hu.gameObject.SetActive(false);
        Gang.gameObject.SetActive(false);
        Peng.gameObject.SetActive(false);
        Guo.gameObject.SetActive(false);
    }
}
