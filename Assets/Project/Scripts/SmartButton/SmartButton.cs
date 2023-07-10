using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SmartButton : MonoBehaviour
{

    [SerializeField] public Button _button;
    [SerializeField] public float _punchScale = 1.1f;
    [SerializeField] public float _punchDuration = 1.0f;

    private bool wasClicked = false;
    private bool wasHiddenWhenClicked = false;

    private void Awake()
    {
        _button.onClick.AddListener(ClickedPunch);
    }

    public void Show()
    {
        _button.gameObject.SetActive(true);
        Grow();        
    }

    public void Hide()
    {
        if (wasClicked)
        {
            wasHiddenWhenClicked = true;
        }
        else
        {
            _button.transform.DOComplete();
            _button.gameObject.SetActive(false);
        }

        _button.transform.localScale = Vector3.one;
    }


    private void Grow()
    {
        _button.transform.DOScale(Vector3.one * _punchScale, _punchDuration).OnComplete(() => Shrink());
    }
    private void Shrink()
    {
        _button.transform.DOScale(Vector3.one, _punchDuration).OnComplete(() => Grow());
    }

    public void ClickedPunch()
    {
        if (wasClicked) return;

        wasClicked = true;
        _button.enabled = false;

        _button.transform.DOComplete();
        _button.transform.DOPunchScale(0.4f * _button.transform.localScale, 0.5f, 6).OnComplete(
            ()=> {
                wasClicked = false;
                _button.enabled = true;

                if (wasHiddenWhenClicked)
                {
                    wasHiddenWhenClicked = false;
                    Hide();
                }
            }
            );
    }

    public void PlayStartSound()
    {
        AudioManager.Instance.PlaySounds("Start");
    }

    public void PlayPauseSound()
    {
        AudioManager.Instance.PlaySounds("Pause");
    }

    public void PlayResetSound()
    {
        AudioManager.Instance.PlaySounds("RestartPoints");
    }
}
