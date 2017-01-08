public class Utility
{
    public static string GetCardNameByType(ECardType cardType)
    {
        switch (cardType)
        {
            case ECardType.OneWan:
                return "一万";
            case ECardType.TwoWan:
                return "二万";
            case ECardType.ThreeWan:
                return "三万";
            case ECardType.FourWan:
                return "四万";
            case ECardType.FiveWan:
                return "五万";
            case ECardType.SixWan:
                return "六万";
            case ECardType.SevenWan:
                return "七万";
            case ECardType.EightWan:
                return "八万";
            case ECardType.NineWan:
                return "九万";
            case ECardType.OneTiao:
                return "一条";
            case ECardType.TwoTiao:
                return "二条";
            case ECardType.ThreeTiao:
                return "三条";
            case ECardType.FourTiao:
                return "四条";
            case ECardType.FiveTiao:
                return "五条";
            case ECardType.SixTiao:
                return "六条";
            case ECardType.SevenTiao:
                return "七条";
            case ECardType.EightTiao:
                return "八条";
            case ECardType.NineTiao:
                return "九条";
            case ECardType.OneTong:
                return "一筒";
            case ECardType.TwoTong:
                return "二筒";
            case ECardType.ThreeTong:
                return "三筒";
            case ECardType.FourTong:
                return "四筒";
            case ECardType.FiveTong:
                return "五筒";
            case ECardType.SixTong:
                return "六筒";
            case ECardType.SevenTong:
                return "七筒";
            case ECardType.EightTong:
                return "八筒";
            case ECardType.NineTong:
                return "九筒";
            case ECardType.Zhong:
                return "中";
            case ECardType.Fa:
                return "发";
            case ECardType.East:
                return "东";
            case ECardType.West:
                return "西";
            case ECardType.South:
                return "南";
            case ECardType.North:
                return "北";
            case ECardType.White:
                return "白板";
            default:
                return string.Empty;
        }
    }
}