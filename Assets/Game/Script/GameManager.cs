using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public EasyTween mainScreen;
    public EasyTween screenStart;
    public EasyTween screenInstructions;
    public ScreenQuestionController questionScreen;
    public EasyTween screenCongratulation;
    public EasyTween screenTryAgain;

    [Header("Question List")]
    public List<Question> questions = new List<Question>();

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

    public void OnSTartQuestions ()
    {
        blockPanel.SetActive(true);
        screenInstructions.OpenCloseObjectAnimation();
        questionScreen.Show();
        questionScreen.LoadQuestion(questions[0]);
        Invoke("TurnOffBlockPanel", 1.5f);
    }


    private void TurnOffBlockPanel ()
    {
        blockPanel.SetActive(false);
    }
}


[System.Serializable]
public struct Question
{
    public Canal canal;
    public string question;
    public string categoria;
    public Sprite backGround;
    public Emoji[] answers;
}

public enum Canal
{
    FOX,
    FX,
    FOX_LIFE,
    FXM,
    CINE_CANAL,
    NATGEO,
    NATGEOKIDS,
    NATGEOWILD
}





