
using UnityEngine;

public class FirstRule : Rule
{
    private int next;
    
    public FirstRule() : base()
    {
        description = "Do you know how to count? I don't think so.";
        next = 1;
    }

    public override int ExecutePreConditions(AnswerInfo previous)
    {
        return next - 1;
    }
    
    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        // Debug.Log($"First: {answerInfo.current} - {answerInfo.answer} - {next}");
        return answerInfo.answer == next;
    }

    public override int getNextNumber()
    {
        return next;
    }
}
