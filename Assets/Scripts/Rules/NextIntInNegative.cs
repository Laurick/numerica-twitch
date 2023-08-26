
using UnityEngine;

public class NextIntInNegative : Rule
{
    int next;
    public NextIntInNegative() : base()
    {
        description = "El siguiente número escribidlo como su opuesto.";
    }
    
    public override int ExecutePreConditions(AnswerInfo previous)
    {
        next = previous.answer + 1;
        return next - 1;
    }
    
    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        // Debug.Log($"NextIntNegative: {answerInfo.current} - {answerInfo.answer} - {next}");
        return answerInfo.answer == (next*-1);
    }

    public override int getNextNumber()
    {
        return next;
    }
}
