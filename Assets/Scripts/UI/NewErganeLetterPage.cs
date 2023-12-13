using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NewErganeLetterPage : MonoBehaviour
    {
        [SerializeField] private RawImage image;
        [SerializeField] private TMP_Text letterName;

        public void SetLetter(NewErganeLetterObj letter)
        {
            this.image.texture = letter.texture;
            this.letterName.text = letter.letterName;
        }
    }
}