﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    public struct Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;

        public Question(string cur_questionText, string[] cur_answers, int cur_correctAnswerIndex)
        {
            this.questionText = cur_questionText;
            this.answers = cur_answers;
            this.correctAnswerIndex = cur_correctAnswerIndex;

        }




    }

    public Question currentQuestion = new Question("What is your favorite color?", new string[] { "red", "Green", "Blue", "Yellow", "Magenta" }, 0);


    public Button[] answerButtons;
    public Text questionDisplayText;
    public GameObject[] triviaPanels;
    public GameObject resultsPanel;
    public Text resultsText;



    private Question[] questionBank = new Question[5];
    private int currentQuestionIndex;
    private int[] questionNumbersChosen = new int[5];
    private int questionsFinished;
    private int numberOfCorrectAnswers = 0;
    

    void AssignQuestion(int questionNum)
    {
        currentQuestion = questionBank[questionNum];
        questionDisplayText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
        }
    }

    public void CheckAnswer(int buttonNum)
    {
        if(buttonNum == currentQuestion.correctAnswerIndex)
        {
            print("correct");
            numberOfCorrectAnswers++;
        }
        else
        {
            print("Incorrect");
        }

        if(questionsFinished < questionNumbersChosen.Length - 1)
        {
            MoveToNextQuestion();
            questionsFinished++;

        }
        else
        {
            foreach(GameObject panel in triviaPanels)
            {
                panel.SetActive(false);
            }
            resultsPanel.SetActive(true);
            DisplayResults();

        }

    }



    // Start is called before the first frame update
    void Start()
    {
        questionBank[0] = new Question("Who is considered the \"father of video games\" because of this pioneering effort in video game hardware and software?", new string[] { "Steve Jobs", "Ralph Henry Baer", "Gabe Newell", "Nolan Bushnell", "Notch" }, 1);
        questionBank[1] = new Question("What does ESRB stand for?", new string[] { "Electronic System Resistance Backup", "Electronic Software Rating Board", "Entertainment System Ranking Board", "Entertainment Software Rating Board", "Electronic Stance Removal Boss" }, 3);
        questionBank[2] = new Question("Which of the following involves in-house testing", new string[] { "Alpha Testing", "Beta Testing", "Gamma Testing", "Delta Testing", "Omega Testing" }, 0);
        questionBank[3] = new Question("What was the first commercially successful video game?", new string[] { "Donkey Kong", "Space Invaders", "Pong", "Pacman", "Super Mario Bros." }, 2);
        questionBank[4] = new Question("Which of the following is not a well known game engine?", new string[] { "Construct 3", "Unity", "Blue Steel", "Unreal", "Godot" }, 2);
        questionBank[5] = new Question("In which phase of the design production cycle is the \"Pitch Document\" created?", new string[] { "Post-production", "Production", "Pre-production", "Concept", "Pre-concept" }, 3);
        questionBank[6] = new Question("In what year did the North American video game crash occur?", new string[] { "1993", "1983", "1980", "1990", "2020" }, 1);
        questionBank[7] = new Question("Which activity does not belong in the Post-Production phase?", new string[] { "Beta testing", "Perform Maintenance", "Repair Errors", "Alpha testing", "Sketch and plan characters" }, 4);
        questionBank[8] = new Question("What term refers to the overall blueprint for a game?", new string[] { "Game Design Document", "Game Manual", "Players Guide", "Flow Chart", "Venn Diagram" }, 0);
        questionBank[9] = new Question("What phase of the design production cycle are that rules of the game created", new string[] { "Gameplay", "Concept", "Post-production", "Production", "Pre-production" }, 4);

        ChooseQuestion();



        AssignQuestion(questionNumbersChosen[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChooseQuestion()
    {
        for(int i = 0; i < questionNumbersChosen.Length;)
        {
            int questionNum = Random.Range(0, questionBank.Length);

            if(NumberNotContained(questionNumbersChosen, questionNum))
            {
                questionNumbersChosen[i] = questionNum;
            }
            else
            {
                i--;
            }
        }

        currentQuestionIndex = Random.Range(0, questionBank.Length);
    }

    bool NumberNotContained(int[] numbers, int num)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            if(num == numbers[i])
            {
                return false;
            }
            
            
        }
        return true;
    }

    public void MoveToNextQuestion()
    {
        AssignQuestion(questionNumbersChosen[questionNumbersChosen.Length - 1 - questionsFinished]);
    }

    void DisplayResults()
    {
        switch (numberOfCorrectAnswers)
        {
            case 5:
                resultsText.text = "5 of 5 Correct. Great Job!";
                break;
            case 4:
                resultsText.text = "4 of 5 Correct. Very Good!";
                break;
            case 3:
                resultsText.text = "3 of 5 Correct. Well Done.";
                break;
            case 2:
                resultsText.text = "2 of 5 Correct. Better luck next time.";
                break;
            case 1:
                resultsText.text = "1 of 5 Correct. You can do better...";
                break;
            case 0:
                resultsText.text = "0 of 5 Correct. Are you even trying?";
                break;
            default:
                print("Not a correct number");
                break;
        }
    }



}


