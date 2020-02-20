using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenPrizeController : MonoBehaviour {

    public static ScreenPrizeController instance;

    public Image logo;
    public Image prize;
    public Text code;

    public Prize BaseballPrize;
    public Prize BoxeoPrize;
    public Prize CiclismoPrize;
    public Prize F1Prize;
    public Prize FutbolPrize;
    public Prize MotoGPPrize;
    public Prize NFLPrize;
    public Prize RallyPrize;
    public Prize TenisPrize;
    public Prize UFCPrize;


    private void Awake()
    {
        instance = this;
    }

    public void LoadScreen ()
    {
        Canal currentChannel = ScreenQuestionController.instance.currentQuestion.canal;

        switch (currentChannel)
        {
            case Canal.Baseball:
                LoadPrize(BaseballPrize);
                break;
            case Canal.Boxeo:
                LoadPrize(BoxeoPrize);
                break;
            case Canal.Ciclismo:
                LoadPrize(CiclismoPrize);
                break;
            case Canal.F1:
                LoadPrize(F1Prize);
                break;
            case Canal.Futbol:
                LoadPrize(FutbolPrize);
                break;
            case Canal.MotoGP:
                LoadPrize(MotoGPPrize);
                break;
            case Canal.NFL:
                LoadPrize(NFLPrize);
                break;
            case Canal.Rally:
                LoadPrize(RallyPrize);
                break;
            case Canal.Tenis:
                LoadPrize(TenisPrize);
                break;
            case Canal.UFC:
                LoadPrize(UFCPrize);
                break;
            default:
                LoadPrize(FutbolPrize);
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
