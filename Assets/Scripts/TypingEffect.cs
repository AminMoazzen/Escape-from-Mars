using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class TypingEffect : MonoBehaviour
{
    [TextArea] [SerializeField] private string text;
    [SerializeField] private int charPerSecond = 5;

    private TextMesh _textMesh;
    private float _delayBetween;

    private void Start()
    {
        _textMesh = GetComponent<TextMesh>();
        _delayBetween = 1 / (float)charPerSecond;

        StartCoroutine(Typing());
    }

    private IEnumerator Typing()
    {
        WaitForSeconds wait = new WaitForSeconds(_delayBetween);

        for (int i = 0; i < text.Length; i++)
        {
            _textMesh.text += text[i];
            yield return wait;
        }
    }
}