using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailPanel : MonoBehaviour {

    private Button Close_Button;
    // Use this for initialization
    void Start()
    {
        Close_Button = transform.Find("Close_Button").GetComponent<Button>();

        Close_Button.onClick.AddListener(ClosePanel);
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
