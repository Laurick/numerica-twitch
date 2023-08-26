using UnityEngine;

public class QuickRule : Rule
{
    private int next;
    
    public QuickRule() : base()
    {
        description = "Rápido, el siguiente número... Solo teneís... ¿300 Jiffys? ¿En serio?";
    }

    public override int ExecutePreConditions(AnswerInfo previous)
    {
        next = previous.answer + 1;
        return next - 1;
    }

    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        Debug.Log($"Quick: {answerInfo.current} - {answerInfo.answer} - {next}");
        Debug.Log($"Time: {answerInfo.time}");
        return answerInfo.answer == next && answerInfo.time < 3f;
    }

    public override int getNextNumber()
    {
        return next;
    }
}
