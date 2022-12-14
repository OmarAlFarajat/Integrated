public class ORGate : LogicGate
{
    protected override void Evaluate()
    {
        bool accumulator = false;
        foreach(bool input in _inputs)
            accumulator = accumulator || input;

        for(int i = 0; i < _outputs.Count; i++)
            _outputs[i] = accumulator;

        base.Evaluate();
    }
}
