using System.Linq;
using UnityEngine;

public class ButtonicaRule : Rule
{
    private static readonly string[] validStrings = { "click", "clock", "tap", "pat", "press" ,"enter", "push" };

    private int next;
    
    public ButtonicaRule() : base()
    {
        description = "¿Has jugado a Buttonica? Jueguemos a Buttonica";
        type = TEXT;
    }

    public override int ExecutePreConditions(AnswerInfo previous)
    {
        next = previous.answer;
        return next;
    }

    public override bool isCorrectAnswer(AnswerInfo answerInfo)
    {
        // Debug.Log($"Butonica: {answerInfo.current} - {answerInfo.answer} - {next}");
        return validStrings.Contains(answerInfo.chatter.message);
    }
    
    public override int getNextNumber()
    {
        return next;
    }
}
