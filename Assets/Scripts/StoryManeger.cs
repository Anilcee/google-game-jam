using UnityEngine;
using UnityEngine.UI;

public class StoryManeger : MonoBehaviour
{
    public Image slideImage;
    public Text slideText;
    public Button nextButton;

    public Slide[] slides = new Slide[4];
    
     
    private int currentSlideIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowSlide(0);
        nextButton.onClick.AddListener(NextSlide);
    }

    void ShowSlide(int index)
    {
        if (index >= 0 && index < slides.Length)
        {
            slideImage.sprite = slides[index].image;
            slideText.text = slides[index].text;
        }
    }

    void NextSlide()
    {
        currentSlideIndex++;
        if (currentSlideIndex < slides.Length)
        {
            ShowSlide(currentSlideIndex);
        }
        else
        {
            Debug.Log("Hikaye bitti!"); // burada sahne geçişi yapılabilir
        }
    }
}
