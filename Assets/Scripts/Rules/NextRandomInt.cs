
using UnityEngine;

public class NextRandomInt : Rule
{
    private int next;
    
    public NextRandomInt() : base()
    {
        description = "Opsie, that was the next number? >:)";
        
    }
    
    public override int ExecutePreConditions(AnswerInfo previous)
    {
        int n = previous.answer;
        next = Random.Range(n+10, n+20);
        next += 1;
        return next - 1;
    }
    
    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        Debug.Log($"NextRandom: {answerInfo.current} - {answerInfo.answer} - {next}");
        return answerInfo.answer == next;
    }

    public override int getNextNumber()
    {
        return next;
    }
}
