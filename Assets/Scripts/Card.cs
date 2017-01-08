using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Text m_Text;

    public ECardType CardType { get; set; }

    public void Show()
    {
        this.m_Text.text = Utility.GetCardNameByType(this.CardType);
        this.SetVisible(true);
    }

    public void Hide()
    {
        this.SetVisible(false);
    }

    private void SetVisible(bool flag)
    {
        this.gameObject.SetActive(flag);
    }
}