using UnityEngine;

public class SlowRule : Rule
{
    private int next;
    
    public SlowRule() : base()
    {
        description = "Con tranquilidad. No pongais el siguiente en... 4.16 microfortnights";
    }

    public override int ExecutePreConditions(AnswerInfo previous)
    {
        next = previous.answer + 1;
        return next - 1;
    }

    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        Debug.Log($"Slow: {answerInfo.current} - {answerInfo.answer} - {next}");
        Debug.Log($"Time: {answerInfo.time}");
        return answerInfo.answer == next && answerInfo.time > 5f;
    }

    public override int getNextNumber()
    {
        return next;
    }
}
