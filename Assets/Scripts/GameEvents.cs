using UnityEngine;
using System;
using System.Collections.Generic;


/// 中央事件管理器 - 管理游戏中所有事件的触发和订阅

public static class GameEvents
{
    // 玩家状态相关事件
    public static event Action<PlayerState> OnPlayerStateChanged;


    // 绳索相关事件
    public static event Action OnRopeShoot;
    public static event Action HookFail;
    public delegate void CanShootRopeEvent(bool canShoot);
    public delegate void SetPlayerBurningEvent(bool isBurning);
    public static event CanShootRopeEvent OnCanShootRopeChanged;
    public static event SetPlayerBurningEvent OnPlayerBurningStateChanged;
    public static event Action<Vector2> OnRopeHooked; // 包含钩点位置
    public static event Action OnRopeReleased;
    public static event Action<float> OnRopeLengthChanged; // 包含新长度
    public static event Action<RopeState> OnRopeStateChanged;

    // 绳索切断事件
    public static event System.Action<Vector2> OnRopeCut;

    // 碰撞相关事件
    public static event System.Action<int> OnPlayerDamaged;

    // 玩家移动相关事件
    public static event Action OnPlayerJump;
    public static event Action<bool> OnPlayerGroundedStateChanged; // true=着地，false=离地
    

    // 相机缩放相关事件
    public static event Action OnCameraZoomIn;
    public static event Action OnCameraZoomOut;
    public static event Action OnCameraZoomReset;
    public static event Action<float> OnCameraZoomTo; // 包含目标缩放大小

    // 相机震动相关事件
    public static event Action<float> OnCameraShake; // 包含trauma值

    // 相机兴趣点相关事件
    public static event Action<List<PointOfInterest>, string> OnFollowPointsOfInterest; // 包含兴趣点列表和序列ID
    public static event Action OnStopFollowingPointsOfInterest; // 停止跟随兴趣点
    public static event Action<string> OnPointsOfInterestSequenceCompleted; // 兴趣点序列完成事件，包含序列ID


    // 玩家生命值相关事件
    public static event Action<int, int> OnPlayerHealthChanged; // 当前生命值, 最大生命值
    public static event Action OnPlayerDied;
    public static event Action OnPlayerRespawn;
    public static event Action OnPlayerRespawnCompleted;

    /// <summary>
    /// 当玩家到达关卡终点时触发
    /// </summary>
    public static event System.Action<Transform> OnEndpointReached;
    public static event System.Action<string> OnLevelLoaded; // 关卡加载完成事件，包含场景名称
    public static event System.Action OnProgressManagerInitialized;
    public static event System.Action<string> OnSceneFullyLoaded;

    // 定义玩家状态枚举（可以移到单独的文件中）
    public enum PlayerState
    {
        Normal,
        Swinging,
        Frozen,
        Burning,
        Electrified,
        Paralyzed
    }

    public enum RopeState
    {
        Normal,
        Burning,
        Electrified,
        Frozen
    }

    // 触发事件的方法

    // 玩家状态相关
    public static void TriggerPlayerStateChanged(PlayerState newState)
    {
        OnPlayerStateChanged?.Invoke(newState);
    }

    // 绳索相关
    public static void TriggerRopeShoot()
    {
        OnRopeShoot?.Invoke();
    }

    public static void TriggerHookFail()
    {
        HookFail?.Invoke();
    }

    public static void TriggerRopeHooked(Vector2 hookPosition)
    {
        OnRopeHooked?.Invoke(hookPosition);
    }

    public static void TriggerRopeReleased()
    {
        OnRopeReleased?.Invoke();
    }

    public static void TriggerRopeLengthChanged(float newLength)
    {
        OnRopeLengthChanged?.Invoke(newLength);
    }

    // 玩家移动相关
    public static void TriggerPlayerJump()
    {
        OnPlayerJump?.Invoke();
    }

    public static void TriggerPlayerGroundedStateChanged(bool isGrounded)
    {
        OnPlayerGroundedStateChanged?.Invoke(isGrounded);
    }

    public static void TriggerPlayerDamaged(int damage)
    {
        OnPlayerDamaged?.Invoke(damage);
    }

    public static void TriggerCanShootRopeChanged(bool canShoot)
    {
        OnCanShootRopeChanged?.Invoke(canShoot);
    }
    public static void TriggerSetPlayerBurning(bool isBurning)
    {
        OnPlayerBurningStateChanged?.Invoke(isBurning);
    }

    // 相机缩放相关
    public static void TriggerCameraZoomIn()
    {
        OnCameraZoomIn?.Invoke();
    }

    public static void TriggerCameraZoomOut()
    {
        OnCameraZoomOut?.Invoke();
    }

    public static void TriggerCameraZoomReset()
    {
        OnCameraZoomReset?.Invoke();
    }

    public static void TriggerCameraZoomTo(float targetSize)
    {
        OnCameraZoomTo?.Invoke(targetSize);
    }

    // 相机震动相关
    public static void TriggerCameraShake(float traumaAmount)
    {
        OnCameraShake?.Invoke(traumaAmount);
    }

    // 相机兴趣点相关
    public static void TriggerFollowPointsOfInterest(List<PointOfInterest> points, string sequenceId)
    {
        OnFollowPointsOfInterest?.Invoke(points, sequenceId);
    }

    public static void TriggerStopFollowingPointsOfInterest()
    {
        OnStopFollowingPointsOfInterest?.Invoke();
    }

    public static void TriggerPointsOfInterestSequenceCompleted(string sequenceId)
    {
        OnPointsOfInterestSequenceCompleted?.Invoke(sequenceId);
    }
    // 触发绳索切断事件
    public static void TriggerRopeCut(Vector2 cutPosition)
    {
        if (OnRopeCut != null)
            OnRopeCut(cutPosition);
    }
    // 存档点相关事件
    public static event Action<Transform> OnCheckpointActivated;

    // 触发方法
    public static void TriggerPlayerHealthChanged(int currentHealth, int maxHealth)
    {
        OnPlayerHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public static void TriggerPlayerDied()
    {
        OnPlayerDied?.Invoke();
    }

    public static void TriggerPlayerRespawn()
    {
        OnPlayerRespawn?.Invoke();
    }

    public static void TriggerPlayerRespawnCompleted()
    {
        OnPlayerRespawnCompleted?.Invoke();
    }

    public static void TriggerCheckpointActivated(Transform checkpoint)
    {
        OnCheckpointActivated?.Invoke(checkpoint);
    }

    public static void TriggerEndpointReached(Transform endpointTransform)
    {
        OnEndpointReached?.Invoke(endpointTransform);
    }
    public static void TriggerLevelLoaded(string sceneName)
    {
        OnLevelLoaded?.Invoke(sceneName);
    }
#region 交互相关事件
    // 交互相关事件
    public static event Action<bool, InteractionType> OnPlayerInInteractiveZoneChanged; // 玩家进入/离开交互区域，以及交互类型
        public enum InteractionType
    {
        Merchant,        // 商人交互
        Environmental,  // 环境交互（如开关、门等）
        Item,           // 道具使用
        NPC            // NPC对话
    }

    // 交互事件委托，返回布尔值表示是否已处理此交互
    public delegate bool PlayerInteractHandler(InteractionType interactionType);
    public static event PlayerInteractHandler OnPlayerInteract;

    // 触发交互事件，返回是否有任何监听器处理了此交互
    public static bool TriggerPlayerInteract(InteractionType interactionType = InteractionType.Item)
    {
        if (OnPlayerInteract != null)
        {
            // 使用Delegate.GetInvocationList获取所有订阅者
            foreach (PlayerInteractHandler handler in OnPlayerInteract.GetInvocationList())
            {
                // 如果任何处理器返回true，表示交互已被处理
                if (handler(interactionType))
                {
                    return true;
                }
            }
        }
        return false; // 没有处理器处理此交互
    }

    // 在触发方法部分添加以下内容
    public static void TriggerPlayerInInteractiveZoneChanged(bool inZone, InteractionType interactionType = InteractionType.Environmental)
    {
        OnPlayerInInteractiveZoneChanged?.Invoke(inZone, interactionType);
    }

    #endregion

    public static event Action<Transform> OnPlayerReachedEndpointCenter;

    public static void TriggerPlayerReachedEndpointCenter(Transform endpoint)
    {
        OnPlayerReachedEndpointCenter?.Invoke(endpoint);
    }

    public static void TriggerOnProgressManagerInitialized()
    {
        OnProgressManagerInitialized?.Invoke();
    }

    public static void TriggerOnSceneFullyLoaded(string sceneName)
    {
        OnSceneFullyLoaded?.Invoke(sceneName);
    }

    public static void TriggerRopeStateChanged(RopeState newState)
    {
        OnRopeStateChanged?.Invoke(newState);
    }

    public static event Action<List<Vector3>> OnRopeElectrifiedStart; // 绳索开始电击，传递路径点
    public static event Action OnRopeElectrifiedEnd; // 绳索电击结束

    // 对应的触发方法
    public static void TriggerRopeElectrifiedStart(List<Vector3> ropePoints)
    {
        OnRopeElectrifiedStart?.Invoke(ropePoints);
    }

    public static void TriggerRopeElectrifiedEnd()
    {
        OnRopeElectrifiedEnd?.Invoke();
    }
}