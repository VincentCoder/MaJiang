using System;
using System.Collections.Generic;
using UnityEngine;

public class PveController : UIWindow
{
    [SerializeField] private GameObject m_CardPreb;
    [SerializeField] private GameObject m_StartGameBtn;

    private readonly List<ECardType> m_CardList = new List<ECardType>();

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
            for(int i = 0; i < 4; ++i)
            {
                this.m_CardList.Add(cardType);
            }
        }
    }
}
