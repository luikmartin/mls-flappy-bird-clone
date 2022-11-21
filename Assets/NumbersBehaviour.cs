using System.Collections.Generic;
using UnityEngine;

public class NumbersBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _numbers;

    public void SetValue(int value)
    {
        ClearCurrentValue();

        foreach (char c in value.ToString().ToCharArray())
        {
            var number = Instantiate(_numbers.Find(n => n.name == c.ToString()), Vector3.zero, Quaternion.identity);
            number.name = c.ToString();
            number.transform.SetParent(transform);
            number.transform.localScale = Vector3.one;
        }
    }

    private void ClearCurrentValue()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
