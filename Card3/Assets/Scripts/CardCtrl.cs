using Protocol.Dto.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 卡牌控制类
/// </summary>
public class CardCtrl : MonoBehaviour
{
    //数据
    public CardDto CardDto { get; private set; }
    //卡牌是否被选中
    public bool Selected { get; set; }

    private Image spriteRenderer;
    private bool isMine;

    /// <summary>
    /// 初始化卡牌数据
    /// </summary>
    /// <param name="card"></param>
    /// <param name="index"></param>
    /// <param name="isMine"></param>
    public void Init(CardDto card, int index, bool isMine)
    {
        spriteRenderer = GetComponent<Image>();
        this.CardDto = card;
        this.isMine = isMine;
        //为了重用
        if (Selected == true)
        {
            Selected = false;
        }
        string resPath = string.Empty;
        if (isMine)
        {
            resPath = "Poker/" + card.Name;
        }
        else
        {
            resPath = "Poker/CardBack";
        }
        Sprite sp = Resources.Load<Sprite>(resPath);
        spriteRenderer.sprite = sp;
        transform.SetSiblingIndex(index);
    }
  

    private Button thisButton;
    private Vector3 loadPosition;
    private void Start()
    {
        loadPosition = transform.position+new Vector3(1,0,0);
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnThisButtonClick);
    }
    /// <summary>
    /// 当鼠标点击时候调用
    /// </summary>
    public void OnThisButtonClick()
    {

        this.Selected = !Selected;
        if (Selected == true)
        {
            transform.localPosition += new Vector3(transform.position.x, transform.position.y + 35f, 0);
        }
        else
        {
            transform.localPosition-= new Vector3(transform.position.x, transform.position.y + 35f, 0);
        }
    }
    public void HaveCard()
    {
        if (spriteRenderer.sprite == null)
        {
            thisButton.interactable = false;
        }
        else
        {
            thisButton.interactable = true;
        }
    }
}
