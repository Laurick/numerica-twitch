
using System;
using UnityEngine;

public class FiveMutipleRule : Rule
{
    private int next;
    
    public FiveMutipleRule() : base()
    {
        description = "Next number is the next multiple of 5";
    }

    public override int ExecutePreConditions(AnswerInfo previous)
    {
        int answer = (previous.answer+5) / 5;
        next = answer*5;
        return Int32.MinValue;
    }

    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        Debug.Log($"Five: {answerInfo.current} - {answerInfo.answer} - {next}");
        return answerInfo.answer == next;
    }

    public override int getNextNumber()
    {
        return next;
    }
}