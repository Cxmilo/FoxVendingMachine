using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

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
        //PlayerPrefs.DeleteAll();
        instance = this;
        questions = questions.OrderBy(a => System.Guid.NewGuid()).ToList();
    }

    public void OnMainScreenTouched()
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

    public void OnSTartQuestions()
    {
        blockPanel.SetActive(true);
        screenInstructions.OpenCloseObjectAnimation();
        questionScreen.Show();

        int currentQuestion = PlayerPrefs.GetInt("CurrentQuestion",0);

        if (currentQuestion < questions.Count-1)
        {
           
            currentQuestion++;

        }
        else
        {
            currentQuestion = 0;
        }

        var questionRan = questions[currentQuestion];
        questionScreen.LoadQuestion(questionRan);


        PlayerPrefs.SetInt("CurrentQuestion", currentQuestion);
        Debug.Log(PlayerPrefs.GetString("usedQ"));

        Invoke("TurnOffBlockPanel", 1.5f);
    }

    public void ShowLoseScreen()
    {
        BlockScreen();
        screenTryAgain.OpenCloseObjectAnimation();
        questionScreen.Hide();
        Invoke("RestartGame", 10);
    }

    public void SwitchBlockScreen ()
    {
        blockPanel.SetActive(!blockPanel.activeInHierarchy);
    }

    public void ShowWinScreen()
    {
        BlockScreen();
        screenCongratulation.OpenCloseObjectAnimation();
        questionScreen.Hide();
        Invoke("ShowPrizeInformation", 4);
    }

    public void ShowPrizeInformation()
    {
        BlockScreen();
        prizeInformation.OpenCloseObjectAnimation();
        ScreenPrizeController.instance.LoadScreen();
        ComManager.Instance.Send("a");
        screenCongratulation.OpenCloseObjectAnimation();

        Invoke("RestartGame", 10);
    }

    public void BlockScreen()
    {
        blockPanel.SetActive(true);
        Invoke("TurnOffBlockPanel", 1.5f);
    }

    private void TurnOffBlockPanel()
    {
        blockPanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
}


[System.Serializable]
public struct Question
{
    public Canal canal;
    [TextArea]
    public string question;
    public Sprite QuestionBackground;
    public List<Emoji> answers;
}

public enum Canal
{
    Baseball,
    Boxeo,
    Ciclismo,
    F1,
    Futbol,
    MotoGP,
    NFL,
    Rally,
    Tenis,
    UFC
}





