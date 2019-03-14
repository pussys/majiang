using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataPanel : MonoBehaviour {

    private Button set_Button;
    private Button dataMission_Button;
    private Button Email_Button;
    private Button help_Button;

    private Transform setPanel;
    private Transform dataMissionPanel;
    private Transform EmailPanel;
    private Transform helpPanel;
	// Use this for initialization
	void Start () {
        set_Button = transform.Find("Set_Button").GetComponent<Button>();
        dataMission_Button = transform.Find("Datamission_Button").GetComponent<Button>();
        Email_Button = transform.Find("Email_Button").GetComponent<Button>();
        help_Button = transform.Find("Help_Button").GetComponent<Button>();

        setPanel = transform.parent.Find("SetPanel");
        dataMissionPanel = transform.parent.Find("DataMissionPanel");
        EmailPanel = transform.parent.Find("E_mail");
        helpPanel = transform.parent.Find("HelpPanel");

        //默认将四个界面关闭
        setPanel.gameObject.SetActive(false);
        dataMissionPanel.gameObject.SetActive(false);
        EmailPanel.gameObject.SetActive(false);
        helpPanel.gameObject.SetActive(false);
        //给四个按钮绑定方法
        set_Button.onClick.AddListener(() => { OpenThePanel(set_Button.name); });
        dataMission_Button.onClick.AddListener(() => { OpenThePanel(dataMission_Button.name); });
        Email_Button.onClick.AddListener(() => { OpenThePanel(Email_Button.name); });
        help_Button.onClick.AddListener(() => { OpenThePanel(help_Button.name); });
    }
    private void OpenThePanel(string button)
    {
        switch (button)
        {
            case "Set_Button":
                setPanel.gameObject.SetActive(true);
                break;
            case "Datamission_Button":
                dataMissionPanel.gameObject.SetActive(true);
                break;
            case "Email_Button":
                EmailPanel.gameObject.SetActive(true);
                break;
            case "Help_Button":
                helpPanel.gameObject.SetActive(true);
                break;
            default:
                Debug.Log("没有这项操作！");
                break;
        }
    }

}
