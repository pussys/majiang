using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTeXiao : UIBase 
{


    public GameObject zhadan;
    public GameObject wangzha;
    public GameObject shunzi;
    public GameObject liandui;
    public GameObject feiji;


    private void Awake()
    {
        Bind(UIEvent.ZHA_DAN);
        Bind(UIEvent.LIAN_DUI);
        Bind(UIEvent.WANG_ZHA);
        Bind(UIEvent.SHUN_ZI);
        Bind(UIEvent.FEI_JI);


    }
   
    public override void Execute(int eventCode, object message)
    {      
        switch (eventCode)
        {
            case UIEvent.ZHA_DAN:
                {
                    zhadan.SetActive(true);
                    Invoke("Close1", 0.8f);                                
                }
                break;
            case UIEvent.FEI_JI:
                {
                    feiji.SetActive(true);
                    Invoke("Close5", 0.8f);
                }
                break;
            case UIEvent.LIAN_DUI:
                {
                    liandui.SetActive(true);
                    Invoke("Close4", 0.8f);
                }
                break;
            case UIEvent.WANG_ZHA:
                {
                    wangzha.SetActive(true);
                    Invoke("Close2", 0.8f);
                }
                break;
            case UIEvent.SHUN_ZI:
                {
                    shunzi.SetActive(true);
                    Invoke("Close3", 0.8f);
                }
                break;
            default:
                break;
        }
    }
    private void Close1()
    {
        zhadan.SetActive(false);
    }
    private void Close2()
    {
        wangzha.SetActive(false);
    }
    private void Close3()
    {
        shunzi.SetActive(false);
    }
    private void Close4()
    {
        liandui.SetActive(false);
    }
    private void Close5()
    {
        feiji.SetActive(false);
    }

}
  


    


    

