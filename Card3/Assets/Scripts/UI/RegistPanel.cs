using Protocol.Code;
using Protocol.Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistPanel : UIBase
{
    //public static RegistPanel instance;
    //public static  RegistPanel Instance
    //{
    //    get
    //    {
    //        if (_instance == null) 
    //        {
    //            _instance = new RegistPanel();
    //        }
    //        return _instance;
    //    }
        
    //}
    private void Awake()
    {
        Bind(UIEvent.REGIST_PANEL_ACTIVE);
        //instance = this;
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.REGIST_PANEL_ACTIVE:
                setPanelActive((bool)message);
                break;
            default:
                break;
        }
    }
    private Button btnReigist;
    private Button btnClose;
    private InputField inputAccount;
    private InputField inputPassword;
    private InputField inputRepeat;
    
    private PromptMsg promptMsg;
    private SocketMsg socketMsg;
    
    // Use this for initialization
    void Start()
    {
        btnReigist = transform.Find("btnReigist").GetComponent<Button>();
        btnClose = transform.Find("btnClose").GetComponent<Button>();
        inputAccount = transform.Find("inputAccount").GetComponent<InputField>();
        inputPassword = transform.Find("inputPassword").GetComponent<InputField>();
        inputRepeat = transform.Find("inputRepeat").GetComponent<InputField>();

        btnClose.onClick.AddListener(closeClick);
        btnReigist.onClick.AddListener(registClick);

        promptMsg = new PromptMsg();
        socketMsg = new SocketMsg();

        setPanelActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        btnClose.onClick.RemoveListener(closeClick);
        btnReigist.onClick.RemoveListener(registClick);
    }

    //AccountDto dto1=new AccountDto();
    //SocketMsg socketMsg = new SocketMsg();

    /// <summary>
    /// 注册按钮的点击事件处理
    /// </summary>
    private void registClick()
    {
        if (string.IsNullOrEmpty(inputAccount.text))
        {
            promptMsg.Change("注册的用户名不能为空！", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }
        if (string.IsNullOrEmpty(inputPassword.text)
            || inputPassword.text.Length < 4
            || inputPassword.text.Length > 16)
        {
            promptMsg.Change("注册的密码不合法！", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }
        if (string.IsNullOrEmpty(inputRepeat.text)
            || inputRepeat.text != inputPassword.text)
        {
            promptMsg.Change("请确保两次输入的密码一致！", Color.red);
            Dispatch(AreaCode.UI, UIEvent.PROMPT_MSG, promptMsg);
            return;
        }
        AccountDto dto = new AccountDto(inputAccount.text, inputPassword.text);
        socketMsg.Change(OpCode.ACCOUNT, AccountCode.REGIST_CREQ, dto);
        Dispatch(AreaCode.NET, 0, socketMsg);
        //注册成功后  直接登录游戏
        //socketMsg.Change(OpCode.ACCOUNT, AccountCode.LOGIN, dto);
        //Dispatch(AreaCode.NET, 0, socketMsg);
    }
    private void closeClick()
    {
        setPanelActive(false);
    }
    //public void Loginhaha()
    //{
    //    socketMsg.Change(OpCode.ACCOUNT, AccountCode.LOGIN, dto1);
    //    Dispatch(AreaCode.NET, 0, socketMsg);
    //}
}
