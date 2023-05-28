using DG.Tweening;
using UnityEngine;

public class DropController : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _timeToTarget;
    [SerializeField] private float _timeToStart;

    private DropType _dropType;

    public void Setup(Drop drop)
    {
        _spriteRenderer.sprite = drop.Sprite;
        _dropType = drop.Type;
        
        DOTween.Sequence()
            .AppendInterval(_timeToStart)
            .Append(transform.DOMove(_gameConfig.DropTarget.position, _timeToTarget).SetEase(Ease.InBack))
            .AppendCallback(() =>
            {
                if (!_playerData.Instance.Drops.ContainsKey(_dropType))
                {
                    _playerData.Instance.Drops.Add(_dropType, 0);
                }

                _playerData.Instance.Drops[_dropType]++;
            })
            .OnComplete(() =>
            {
                Destroy(gameObject);
            })
            .Play();
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }
    
}
