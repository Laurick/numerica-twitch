
using UnityEngine;

public class NextPositiveInt : Rule
{
    private int next;
    public NextPositiveInt() : base()
    {
        description = "¿Sabéis contar? No lo creo.";
    }
    
    public override int ExecutePreConditions(AnswerInfo previous)
    {
        next = previous.answer + 1;
        return next - 1;
    }
    
    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        // Debug.Log($"NextPositive: {answerInfo.current} - {answerInfo.answer} - {next}");
        return answerInfo.answer == next;
    }

    public override int getNextNumber()
    {
        return next;
    }
}
