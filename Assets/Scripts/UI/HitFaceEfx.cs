using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFaceEfx : MonoBehaviour
{
    [SerializeField] private Animation m_anim;
    [SerializeField] private List<string> m_animNames;
    private int m_faceHitCount = 0;
    private static string SAVE_KEY = "faceHitCount";

    void Start()
    {
        m_faceHitCount = PlayerPrefs.GetInt(SAVE_KEY, m_faceHitCount);
        FaceHit();
    }

    public void FaceHit()
    {
        m_faceHitCount++;
        m_faceHitCount %= m_animNames.Count;
        PlayerPrefs.SetInt(SAVE_KEY, m_faceHitCount);
        m_anim.Play(m_animNames[m_faceHitCount]);
        StartCoroutine("OnCompleteAnimation");
        EventBus.Instance.Raise(new SoundEvents.SoundEvent { type = SoundEvents.SoundType.Face });
    }

    IEnumerator OnCompleteAnimation()
    {
        while (m_anim.IsPlaying(m_animNames[m_faceHitCount]))
            yield return null;

        gameObject.SetActive(false);
    }
}
