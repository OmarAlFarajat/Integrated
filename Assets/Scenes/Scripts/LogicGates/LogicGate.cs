using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class LogicGate : MonoBehaviour
{
    private const float TOTAL_WIDTH = 2.75f;
    [SerializeField] private Material _onMaterial;
    [SerializeField] private Material _offMaterial;
    [SerializeField] private GameObject _pinPrefab;
    [SerializeField] private Transform _outputRoot;
    [SerializeField] private Transform _inputRoot;

    [SerializeField] private TMP_Text _name;
    [SerializeField] protected List<bool> _inputs;
    [SerializeField] protected List<bool> _outputs;

    protected virtual void Evaluate() 
    {
        ColorPins(_inputs, _inputRoot);
        ColorPins(_outputs, _outputRoot);
    }

    private void ColorPins(List<bool> pins, Transform root)
    {
        for (int i = 0; i < root.childCount; i++)
            root.GetChild(i).gameObject.GetComponent<Renderer>().material = pins[i] ? _onMaterial : _offMaterial;
    }

    private void Awake()
    {
        _name.text = GetType().Name;
        InitPins();
    }

    private void InitPins()
    {
        FitPins(_inputs, _inputRoot);
        FitPins(_outputs, _outputRoot);
    }

    private void FitPins(List<bool> pins, Transform root)
    {
        float pinWidth = Math.Min(TOTAL_WIDTH / pins.Count, 0.25f);
        Vector3 rootOffset = Vector3.zero;
        Vector3 offsetIncrement = new Vector3(pinWidth, 0f, 0f);
        for (int i = 0; i < pins.Count; i++)
        {
            GameObject obj = Instantiate(_pinPrefab, root);
            obj.transform.localScale = new Vector3(pinWidth, obj.transform.localScale.y, pinWidth);
            obj.transform.position += rootOffset;
            rootOffset += offsetIncrement;
        }
    }

    protected void Update()
    {
        try
        {
            Evaluate();
        }
        catch(AssertionException e)
        {
            Debug.LogError(e);
            Debug.LogError("NOT Gates cannot have more than 1 input!");
            gameObject.SetActive(false);
        }
    }
}
