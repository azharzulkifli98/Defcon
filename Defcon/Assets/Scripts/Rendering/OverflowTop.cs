using UnityEngine;
using UnityEngine.UI;

public class OverflowTop : MonoBehaviour
{
    [SerializeField]
    Text text = null;

    void Update()
    {
        if (LayoutUtility.GetPreferredHeight(text.rectTransform)
            > text.gameObject.GetComponent<RectTransform>().rect.height)
            text.alignment = TextAnchor.LowerLeft;
        else
            text.alignment = TextAnchor.UpperLeft;
    }
}
