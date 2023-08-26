using UnityEngine;

public class StreamerRule : Rule
{
    private int next;
    
    public StreamerRule() : base()
    {
        description = "Exacto, el siguiente quiero que lo escriba el 'streamer'. Mandale unos bits o un !hey o.. no hagas nada. Sabes que te ignorará.";
    }

    public override int ExecutePreConditions(AnswerInfo previous)
    {
        next = previous.answer + 1;
        return next - 1;
    }

    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        Debug.Log($"Streamer: {answerInfo.current} - {answerInfo.answer} - {next}");
        foreach (var badge in answerInfo.chatter.tags.badges)
        {
            Debug.Log($"badges: {badge.id}");
        }
        return answerInfo.answer == next && answerInfo.chatter.HasBadge("broadcaster");
    }

    public override int getNextNumber()
    {
        return next;
    }
}
