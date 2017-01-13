using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    private int[] m_CardArray;
    public int PlayerId { get; private set; }
    public bool IsTing { get; private set; }
    public bool IsDealer { get; private set; }

    private readonly Dictionary<ECardType, List<WinSeq>> m_TingDict = new Dictionary<ECardType, List<WinSeq>>();

    public void InitPlayer(int playerId, bool isDealer = false)
    {
        this.PlayerId = playerId;
        this.IsDealer = isDealer;
        this.m_CardArray = new int[34];
    }

    public void StartPlay(List<ECardType> cardList)
    {
        for(int i = 0; i < cardList.Count; ++i)
        {
            this.DrawCard(cardList[i]);
        }
    }

    public void DrawCard(ECardType card)
    {
        if(this.CanWin(card))
        {
            Debug.LogError("Player Id : "+this.PlayerId+" Win !");
        }
        else
        {
            int currentCount = this.m_CardArray[(int)card];
            if (currentCount >= 4)
            {
                Debug.LogError("Max is 4 !");
            }
            else
            {
                this.m_CardArray[(int)card] = currentCount + 1;
            }
        }
    }

    public void DiscardCard(ECardType card)
    {
        int currentCount = this.m_CardArray[(int)card];
        if(currentCount <= 0)
        {
            Debug.LogError("Min is 0 !");
        }
        else
        {
            this.m_CardArray[(int)card] = currentCount-1;
            this.CheckTing();
        }
    }

    public bool CanWin(ECardType card)
    {
        if(this.IsTing && this.m_TingDict.ContainsKey(card))
        {
            return true;
        }
        return false;
    }

    private int GetCardCount(int[] cards)
    {
        int sum = 0;
        for(int i = 0; i < cards.Length; ++i)
        {
            sum += cards[i];
        }
        return sum;
    }

    private void CheckTing()
    {
        //3*n+1
        int cardCount = this.GetCardCount(this.m_CardArray);
        if((cardCount-1)%3 == 0)
        {
            this.m_TingDict.Clear();
            this.IsTing = false;
            foreach(ECardType cardType in Enum.GetValues(typeof(ECardType)))
            {
                int[] copy = new int[this.m_CardArray.Length];
                this.m_CardArray.CopyTo(copy, 0);
                copy[(int)cardType] = copy[(int)cardType]+1;
                List<WinSeq> results = this.CalWinResult(copy);
                if(results.Count > 0)
                {
                    this.m_TingDict.Add(cardType, results);
                    this.IsTing = true;
                }
                //if (results.Count > 0)
                //{
                //    Debug.LogError(cardType);
                //    for (int i = 0; i < results.Count; ++i)
                //    {
                //        Debug.LogError(results[i].ToString());
                //    }
                //}
            }
        }
        else
        {
            Debug.LogError("Player Id : " + this.PlayerId + "，你相公了！！！");
        }
    }

    private List<WinSeq> CalWinResult(int[] cards)
    {
        List<WinSeq> results = new List<WinSeq>();
        int cardCount = this.GetCardCount(cards);
        //3*n+2
        if ((cardCount - 2) % 3 == 0)
        {
            for (int i = 0; i < cards.Length; ++i)
            {
                if (cards[i] >= 2)
                {
                    int[] copy = new int[cards.Length];
                    cards.CopyTo(copy, 0);
                    copy[i] = copy[i] - 2;
                    this.IsAllMelds(copy, new int[4,3], 0, (ECardType)i, ref results);
                }
            }
        }
        return results;
    }

    private void IsAllMelds(int[] cards, int[,] result, int index, ECardType pair, ref List<WinSeq> results)
    {
        if (index >= 4)
        {
            WinSeq seq = new WinSeq
            {
                Pair = new[] {(int)pair, (int)pair},
                Seq = result
            };
            results.Add(seq);
            return;
        }

        for (int i = 0; i < cards.Length; ++i)
        {
            if (cards[i] > 0 && cards[i] <= 4)
            {
                if(cards[i] == 3)
                {
                    int[] copy = new int[cards.Length];
                    cards.CopyTo(copy, 0);
                    copy[i] = copy[i] - 3;
                    int[,] resultCopy = new int[4, 3];
                    Array.Copy(result, resultCopy, result.Length);

                    resultCopy[index, 0] = i;
                    resultCopy[index, 1] = i;
                    resultCopy[index, 2] = i;
                    this.IsAllMelds(copy, resultCopy, index + 1, pair, ref results);
                }
                if (this.IsTiao((ECardType)i) || this.IsTong((ECardType)i) || this.IsWan((ECardType)i))
                {
                    if (i < cards.Length - 2 && cards[i + 1] > 0 && cards[i + 2] > 0)
                    {
                        if (this.IsMeld((ECardType)i, (ECardType)(i + 1), (ECardType)(i + 2)))
                        {
                            int[] copy = new int[cards.Length];
                            cards.CopyTo(copy, 0);

                            copy[i] = copy[i] - 1;
                            copy[i + 1] = copy[i + 1] - 1;
                            copy[i + 2] = copy[i + 2] - 1;

                            int[,] resultCopy = new int[4, 3];
                            Array.Copy(result, resultCopy, result.Length);

                            resultCopy[index, 0] = i;
                            resultCopy[index, 1] = i + 1;
                            resultCopy[index, 2] = i + 2;
                            this.IsAllMelds(copy, resultCopy, index + 1, pair, ref results);
                        }
                    }
                }
                return;
            }
        }
    }

    private bool IsMeld(ECardType card1, ECardType card2, ECardType card3)
    {
        if(card1 == card2 && card2 == card3)
        {
            return true;
        }
        if(this.IsTiao(card1) && this.IsTiao(card2) && this.IsTiao(card3)
            || this.IsTong(card1) && this.IsTong(card2) && this.IsTong(card3)
            || this.IsWan(card1) && this.IsWan(card2) && this.IsWan(card3))
        {
            ECardType[] cards = {card1, card2, card3};
            Array.Sort(cards);
            if(card2-card1 == 1 && card3-card2 == 1)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsTiao(ECardType card)
    {
        return card >= ECardType.OneTiao && card <= ECardType.NineTiao;
    }

    private bool IsTong(ECardType card)
    {
        return card >= ECardType.OneTong && card <= ECardType.NineTong;
    }

    private bool IsWan(ECardType card)
    {
        return card >= ECardType.OneWan && card <= ECardType.NineWan;
    }
}

public struct WinSeq
{
    public int[,] Seq;
    public int[] Pair;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("Result : Pair - ");
        sb.Append((ECardType)Pair[0]);
        sb.Append("，");
        for (int i = 0; i < Seq.GetLength(0); ++i)
        {
            for (int j = 0; j < Seq.GetLength(1); ++j)
            {
                sb.Append((ECardType)Seq[i, j]);
            }
            sb.Append("，");
        }
        return sb.ToString();
    }
}
