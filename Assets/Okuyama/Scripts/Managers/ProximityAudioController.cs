using UnityEngine;

public class ProximityAudioController : MonoBehaviour
{
    [SerializeField] Transform player;           // プレイヤーの位置
    [SerializeField] AudioSource audioSource;    // 効果音のAudioSource
    [SerializeField] float maxDistance = 20f;    // 音が完全に消える最大距離
    [SerializeField] LayerMask enemyLayer;       // 敵のレイヤーマスク
    [SerializeField] float maxVolume = 0.5f;     // 最大音量

    void Update()
    {
        UpdateAudioVolumeBasedOnProximity();
    }

    // 最も近い敵との距離に基づいて音量を調整
    void UpdateAudioVolumeBasedOnProximity()
    {
        Transform closestEnemy = GetClosestEnemyInRange();
        if (closestEnemy != null)
        {
            float distance = Vector3.Distance(player.position, closestEnemy.position);
            float volume = CalculateVolumeBasedOnDistance(distance);
            audioSource.volume = volume;
        }
        else
        {
            // 敵が見つからない場合は音を小さくする
            audioSource.volume = 0f;
        }
    }

    // プレイヤー周囲の一定範囲内で最も近い敵を取得
    Transform GetClosestEnemyInRange()
    {
        Transform closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 playerPosition = player.position;

        // プレイヤー周囲の maxDistance の範囲内にいる敵を取得
        Collider[] nearbyEnemies = Physics.OverlapSphere(playerPosition, maxDistance, enemyLayer);

        // 各敵との距離をチェック
        foreach (Collider enemyCollider in nearbyEnemies)
        {
            Vector3 directionToEnemy = enemyCollider.transform.position - playerPosition;
            float distanceSqr = directionToEnemy.sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestEnemy = enemyCollider.transform;
            }
        }

        return closestEnemy;
    }

    // 距離に基づいて音量を計算
    float CalculateVolumeBasedOnDistance(float distance)
    {
        // 最大距離を超えたら音量を0にする
        if (distance > maxDistance)
        {
            return 0f;
        }

        // 距離に応じて音量をリニアに調整
        return (1f - (distance / maxDistance)) * maxVolume;
    }

    // GizmosでOverlapSphereの範囲を可視化（デバッグ用）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(player.position, maxDistance);
    }
}
