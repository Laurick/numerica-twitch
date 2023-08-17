using UnityEngine;

public class SuscriptorRule : Rule
{
    private int next;
    
    public SuscriptorRule() : base()
    {
        description = "Subscribers only. Subscribe now for play :P";
    }

    public override int ExecutePreConditions(AnswerInfo previous)
    {
        next = previous.answer + 1;
        return next - 1;
    }

    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        // Debug.Log($"Suscriptor: {answerInfo.current} - {answerInfo.answer} - {next}");
        foreach (var badge in answerInfo.chatter.tags.badges)
        {
            Debug.Log($"badges: {badge.id}");
        }
        return answerInfo.answer == next && answerInfo.chatter.HasBadge("subscriber");
    }

    public override int getNextNumber()
    {
        return next;
    }
}
