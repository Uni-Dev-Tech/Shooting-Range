using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour, IPointerClickHandler
{
    public Enemy enemy;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.rawPointerPress.CompareTag("Enemy"))
        {
            SoundManager.instance.PlaySound(SoundManager.instance.explosion);
            enemy = eventData.rawPointerPress.GetComponent<Enemy>();
            enemy.animator.SetTrigger("Explosion");
            StartCoroutine(ExplosionDelay());
            UIManager.instance.birdsPoints++;
            UIManager.instance.bonus.value += 0.2f;
        }
        if(eventData.rawPointerPress.CompareTag("Bonus"))
        {
            SoundManager.instance.PlaySound(SoundManager.instance.explosion);
            Destroy(eventData.rawPointerPress.gameObject);
            UIManager.instance.timer += 3;
        }
        if (eventData.rawPointerPress.CompareTag("Background"))
            UIManager.instance.bonus.value *= 0f;
    }
    IEnumerator ExplosionDelay()
    {
        yield return new WaitForSeconds(0.2f);
        enemy.explosion.Play();
        yield return new WaitForSeconds(0.25f);
        enemy.DisableMyself();
    }
}
