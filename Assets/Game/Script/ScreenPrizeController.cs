using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenPrizeController : MonoBehaviour {

    public static ScreenPrizeController instance;

    public Image logo;
    public Image prize;
    public Text code;

    public Prize foxPrize;
    public Prize fxPrize;
    public Prize foxLife;
    public Prize fxmLifePrize;
    public Prize CineCanalPrize;
    public Prize NatGeoPrize;
    public Prize NatGeoKidsPrize;
    public Prize NatGeoWildPrize;


    private void Awake()
    {
        instance = this;
    }

    public void LoadScreen ()
    {
        Canal currentChannel = ScreenQuestionController.instance.currentQuestion.canal;

        switch (currentChannel)
        {
            case Canal.FOX:
                LoadPrize(foxPrize);
                break;
            case Canal.FX:
                LoadPrize(fxPrize);
                break;
            case Canal.FOX_LIFE:
                LoadPrize(foxLife);
                break;
            case Canal.FXM:
                LoadPrize(fxmLifePrize);
                break;
            case Canal.CINE_CANAL:
                LoadPrize(CineCanalPrize);
                break;
            case Canal.NATGEO:
                LoadPrize(NatGeoPrize);
                break;
            case Canal.NATGEOKIDS:
                LoadPrize(NatGeoKidsPrize);
                break;
            case Canal.NATGEOWILD:
                LoadPrize(NatGeoWildPrize);
                break;
            default:
                LoadPrize(foxPrize);
                break;
        }

    }

    private void LoadPrize (Prize prize)
    {
        code.text = prize.code;
        this.prize.sprite = prize.prize;
    }

}

[System.Serializable]
public struct Prize
{
    public string code;
    public Sprite prize;
}
