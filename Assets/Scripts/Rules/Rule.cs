
public abstract class Rule
{
    public const int NUMBER = 0;
    public const int TEXT = 1;
    public string description;
    public int type;

    protected Rule()
    {
        type = NUMBER;
    }

    public abstract int ExecutePreConditions(AnswerInfo previous);

    public abstract bool isCorrectAnswer(AnswerInfo answerInfo);

    public abstract int getNextNumber();

}
