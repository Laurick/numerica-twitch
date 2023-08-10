using UnityEngine;

public class SameInt : Rule
{
    private int next;
    public SameInt() : base()
    {
        description = "Dont count up. Don't count down";
    }

    public override int ExecutePreConditions(AnswerInfo previous)
    {
        next = previous.answer;
        return next;
    }

    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        Debug.Log($"Same: {answerInfo.current} - {answerInfo.answer} - {next}");
        return answerInfo.answer == next;
    }

    public override int getNextNumber()
    {
        return next;
    }
}
