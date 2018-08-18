using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance; 

    public EasyTween mainScreen;
    public EasyTween screenStart;
    public EasyTween screenInstructions;
    public ScreenQuestionController questionScreen;
    public EasyTween screenCongratulation;
    public EasyTween screenTryAgain;
    public EasyTween prizeInformation;

    [Header("Question List")]
    public List<Question> questions = new List<Question>();

    [Header("Blocker Panel Manager")]
    public GameObject blockPanel;

    private void Awake()
    {
        instance = this;
    }

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

    public void ShowLoseScreen ()
    {
        BlockScreen();
        screenTryAgain.OpenCloseObjectAnimation();
        questionScreen.Hide();
        Invoke("RestartGame", 10);
    }

    public void ShowWinScreen()
    {
        BlockScreen();
        screenCongratulation.OpenCloseObjectAnimation();
        questionScreen.Hide();
        Invoke("ShowPrizeInformation", 4);
    }

    public void ShowPrizeInformation ()
    {
        BlockScreen();
        prizeInformation.OpenCloseObjectAnimation();
        screenCongratulation.OpenCloseObjectAnimation();
        Invoke("RestartGame", 10);
    }

    public void BlockScreen ()
    {
        blockPanel.SetActive(true);
        Invoke("TurnOffBlockPanel", 1.5f);
    }

    private void TurnOffBlockPanel ()
    {
        blockPanel.SetActive(false);
    }

    public void RestartGame ()
    {
        SceneManager.LoadScene("Main");
    }
}


[System.Serializable]
public struct Question
{
    public Canal canal;
    public string question;
    public string categoria;
    public Sprite backGround;
    public List<Emoji> answers;
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





