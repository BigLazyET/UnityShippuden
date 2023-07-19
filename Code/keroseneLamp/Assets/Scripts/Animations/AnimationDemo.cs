using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Animations
{
    /// <summary>
    /// 四种动画：数学API、DOTween、Animation/Animator、Playable
    /// </summary>
    public class AnimationDemo : MonoBehaviour
    {
        public Animation animation;
        public Animator animator;

        public CanvasGroup canvasGroup; // UGUI 组件
        public float fadeDuration;

        private void Start()
        {
            StartCoroutine(Fade(1));
        }

        private IEnumerator Fade(float targetAlpha)
        {
            // Use DOTween
            yield return canvasGroup.DOFade(targetAlpha, fadeDuration).WaitForCompletion();

            // Use 数学API
            var speed = Mathf.Abs(targetAlpha - canvasGroup.alpha) / fadeDuration;
            while (!Mathf.Approximately(targetAlpha, canvasGroup.alpha))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            }
        }
    }
}
