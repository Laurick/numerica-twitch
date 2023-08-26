
using UnityEngine;

public class FirstRule : Rule
{
    private int next;
    
    public FirstRule() : base()
    {
        description = "¿Sabéis empezar a contar? No lo creo.";
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
