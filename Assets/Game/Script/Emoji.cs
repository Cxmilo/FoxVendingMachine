﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoji : MonoBehaviour
{
    
    public void OnEmojiPressed()
    {
        ScreenQuestionController.instance.AnswerClicked(this);
    }

}
