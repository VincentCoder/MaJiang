using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private readonly Dictionary<ESeat, GamePlayer> m_PlayerDict = new Dictionary<ESeat, GamePlayer>();
    private int[] m_CardArray = new int[34];

    private void Start()
    {
        this.InitPlayers();
        this.StartGame();
    }

    public void InitPlayers()
    {
        GameObject playerObj = new GameObject("EastPlayer");
        GamePlayer gamePlayer = playerObj.AddComponent<GamePlayer>();
        gamePlayer.InitPlayer(0);
        this.m_PlayerDict.Add(ESeat.East, gamePlayer);

        playerObj = new GameObject("WestPlayer");
        gamePlayer = playerObj.AddComponent<GamePlayer>();
        gamePlayer.InitPlayer(1);
        this.m_PlayerDict.Add(ESeat.West, gamePlayer);

        playerObj = new GameObject("SouthPlayer");
        gamePlayer = playerObj.AddComponent<GamePlayer>();
        gamePlayer.InitPlayer(2, true);
        this.m_PlayerDict.Add(ESeat.South, gamePlayer);

        playerObj = new GameObject("NorthPlayer");
        gamePlayer = playerObj.AddComponent<GamePlayer>();
        gamePlayer.InitPlayer(3);
        this.m_PlayerDict.Add(ESeat.North, gamePlayer);
    }

    public void StartGame()
    {
        this.InitCards();
        this.m_PlayerDict[ESeat.East].StartPlay(this.GetCardsByCount(13));
        this.m_PlayerDict[ESeat.West].StartPlay(this.GetCardsByCount(13));
        this.m_PlayerDict[ESeat.South].StartPlay(this.GetCardsByCount(13));
        this.m_PlayerDict[ESeat.North].StartPlay(this.GetCardsByCount(13));
    }

    private List<ECardType> GetCardsByCount(int count)
    {
        List<ECardType> cards = new List<ECardType>();
        int total = this.GetCardCount();
        if(total >= count)
        {
            for (int i = 0; i < count; ++i)
            {
                total = this.GetCardCount();
                int random = Random.Range(1, total + 1);
                int sum = 0;
                for(int j = 0; j < this.m_CardArray.Length; ++j)
                {
                    sum += this.m_CardArray[j];
                    if(sum >= random)
                    {
                        this.m_CardArray[j] = this.m_CardArray[j]-1;
                        cards.Add((ECardType)j);
                        break;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Not enough cards !");
        }
        return cards;
    }

    private int GetCardCount()
    {
        int sum = 0;
        for(int i = 0; i < this.m_CardArray.Length; ++i)
        {
            sum += this.m_CardArray[i];
        }
        return sum;
    }

    private void InitCards()
    {
        for(int i = 0; i < this.m_CardArray.Length; ++i)
        {
            this.m_CardArray[i] = 4;
        }
    }

    private void ClearCards()
    {
        for (int i = 0; i < this.m_CardArray.Length; ++i)
        {
            this.m_CardArray[i] = 0;
        }
    }
}