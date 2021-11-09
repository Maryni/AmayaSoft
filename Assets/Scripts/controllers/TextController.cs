using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    #region private variables

    [SerializeField]
    private Text textItemFind;

    #endregion private variables

    #region public void

    public void ChangeText(string text)
    {
        textItemFind.text = text;
    }

    #endregion public void
}