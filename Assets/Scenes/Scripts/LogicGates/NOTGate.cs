using UnityEngine.Assertions;

public class NOTGate : LogicGate
{
    protected override void Evaluate()
    {
        Assert.IsTrue(_inputs.Count == 1);
        for(int i=0; i < _outputs.Count; i++)
            _outputs[i] = !_inputs[0];

        base.Evaluate();
    }
}
