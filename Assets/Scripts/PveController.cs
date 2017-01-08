using System;
using System.Collections.Generic;
using UnityEngine;

public class PveController : UIWindow
{
    [SerializeField] private GameObject m_CardPreb;
    [SerializeField] private GameObject m_StartGameBtn;

    private readonly List<Card> m_CardList = new List<Card>();

    protected override void PrepareToShow(object uiData)
    {
        base.PrepareToShow(uiData);
        this.InitCards();
    }

    public void StartGame()
    {
        this.m_StartGameBtn.SetActive(false);

    }

    private void InitCards()
    {
        foreach (ECardType cardType in Enum.GetValues(typeof(ECardType)))
        {
            GameObject cardObj = Instantiate(this.m_CardPreb);
            Card card = cardObj.GetComponent<Card>();
            card.CardType = cardType;
            this.m_CardList.Add(card);
        }
    }
}
