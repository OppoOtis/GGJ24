using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlackBoard
{
    public static PuppetMaster otto;
    public static CardManager manager;
    public static Clock clock;
    public static CurrencyManager currency;
    public static AudioManager audioM;
    public static EventManager events;

    public static bool playerTurn;
    public static int eventCounter;
    public static bool funny;

    public static Card selectedCard;
}
