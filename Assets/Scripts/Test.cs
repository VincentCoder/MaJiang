﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Test : MonoBehaviour
{
    //private void Start()
    //{
    //    Stopwatch sw = new Stopwatch();
    //    sw.Start();
    //    GamePlayer gamePlayer = GameObject.FindObjectOfType<GamePlayer>();
    //    int[] cards = {3,1,1,1,1,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    //    foreach (ECardType cardType in Enum.GetValues(typeof(ECardType)))
    //    {
    //        int[] copy = new int[cards.Length];
    //        cards.CopyTo(copy, 0);
    //        copy[(int)cardType] = copy[(int)cardType]+1;
    //        List<WinSeq> results = gamePlayer.CanWin(copy);
    //        if(results.Count > 0)
    //        {
    //            Debug.LogError(cardType);
    //            for(int i = 0; i < results.Count; ++i)
    //            {
    //                Debug.LogError(results[i].ToString());
    //            }
    //        }
    //    }
    //    sw.Stop();
    //    Debug.LogError("Time : " + sw.ElapsedMilliseconds);
    //}
}
