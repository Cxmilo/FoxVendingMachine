using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenQuestionController : MonoBehaviour {


    public Question currentQuestion;

    public Image background;
    public Text questionText;

    public Transform answerContainer;
    public Transform optionsContainer;

    public void LoadQuestion (Question question)
    {
        currentQuestion = question;

        background.sprite = currentQuestion.backGround;
        questionText.text = question.question;
    }
}
