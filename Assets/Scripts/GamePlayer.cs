using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    private readonly List<ECardType> m_CardList = new List<ECardType>();
    public int PlayerId { get; private set; }

    public void InitPlayer(int playerId)
    {
        this.PlayerId = playerId;
    }

    public void StartPlay(List<ECardType> cardList)
    {
        this.m_CardList.AddRange(cardList);
        this.SortCard();
    }

    public void DrawCard(ECardType card)
    {
        this.m_CardList.Add(card);
        this.SortCard();
    }

    public void DiscardCard(ECardType card)
    {
        this.m_CardList.Remove(card);

    }

    private void SortCard()
    {
        this.m_CardList.Sort((card1, card2) => card1.CompareTo(card2));
    }

    private bool CheckValid()
    {
        int cardCount = this.m_CardList.Count;
        if(cardCount == 1 || cardCount == 4 || cardCount == 7 || cardCount == 10 || cardCount == 13)
        {
            return true;
        }
        else
        {
            Debug.LogError("Player Id : " + this.PlayerId + "，你相公了！！！");
            return false;
        }
    }
}