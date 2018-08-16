using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public EasyTween mainScreen;
    public EasyTween screenStart;
    public EasyTween screenInstructions;
    public EasyTween screenCongratulation;
    public EasyTween screenTryAgain;

    [Header("Blocker Panel Manager")]
    public GameObject blockPanel;

    public void OnMainScreenTouched ()
    {
        blockPanel.SetActive(true);
        mainScreen.OpenCloseObjectAnimation();
        screenStart.OpenCloseObjectAnimation();
        Invoke("TurnOffBlockPanel", 1.5f);
    }

    public void OnStartScreenTouched()
    {
        blockPanel.SetActive(true);
        screenStart.OpenCloseObjectAnimation();
        screenInstructions.OpenCloseObjectAnimation();
        Invoke("TurnOffBlockPanel", 1.5f);
    }


    private void TurnOffBlockPanel ()
    {
        blockPanel.SetActive(false);
    }
}
