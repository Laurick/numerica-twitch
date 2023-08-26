
using UnityEngine;

public class PreviousPositiveInt : Rule
{
    private int next;
    public PreviousPositiveInt() : base()
    {
        description = "Contad ahora hacia atrás";
    }

    public override int ExecutePreConditions(AnswerInfo previous)
    {
        next = previous.answer - 1;
        return next+1;
    }
    
    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        // Debug.Log($"PreviousPositive: {answerInfo.current} - {answerInfo.answer} - {next}");
        return answerInfo.answer == next;
    }

    public override int getNextNumber()
    {
        return next;
    }
}
