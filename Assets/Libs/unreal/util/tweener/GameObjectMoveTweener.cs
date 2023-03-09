
using cambrian.common;
using UnityEngine;
using XLua;


[Hotfix]
public class GameObjectMoveTweener : MonoBehaviour
{

    public enum Direction
    {
        Left,
        Right,
        Top,
        Bottom
    }

    public Direction direction=Direction.Bottom;
    /// <summary>
    /// 距离
    /// </summary>
    public float length;
    /// <summary>
    /// 延迟时间()
    /// </summary>
    public long delayTime;
    /// <summary>
    /// 每帧移动的距离
    /// </summary>
    public float moveInterval=2;
    /// <summary>
    /// 帧频
    /// </summary>
    public int FPS=5;
    /// <summary>
    /// 初始位置
    /// </summary>
    private Vector2 initpos;

    private RectTransform rect;

    void Awake()
    {
        rect = this.GetComponent<RectTransform>();
        initpos = this.rect.anchoredPosition;
        setMovePostion();
    }

    private void setMovePostion()
    {
        Vector2 movepos = this.initpos;
        switch (direction)
        {
            case Direction.Bottom:
                movepos.y -= length;
                break;
            case Direction.Left:
                movepos.x -= length;
                break;
            case Direction.Right:
                movepos.x += length;
                break;
            case Direction.Top:
                movepos.y += length;
                break;
        }
        this.rect.anchoredPosition = movepos;
    }

    private long curtime;

    private bool isMoveOver;
    void OnEnable()
    {
        setMovePostion();
        curtime = TimeKit.currentTimeMillis + delayTime;
        isMoveOver = false;
    }

    private float mDelta = 0;

    /// <summary>
    /// 移动
    /// </summary>
    private void move()
    {
        switch (direction)
        {
            case Direction.Bottom:
                move(false,true);
                break;
            case Direction.Left:
                move(true, true);
                break;
            case Direction.Right:
                move(true,false);
                break;
            case Direction.Top:
                move(false,false);
                break;
        }
    }

    private void move(bool isX, bool isAdd)
    {
        Vector2 pos = this.rect.anchoredPosition;
        bool isOver = false;
        if (isX)
        {
            if (isAdd)
            {
                pos.x += moveInterval;
                if (pos.x > initpos.x)
                {
                    pos.x = initpos.x;
                    isOver = true;
                }
            }
            else
            {
                pos.x -= moveInterval;
                if (pos.x < initpos.x)
                {
                    pos.x = initpos.x;
                    isOver = true;
                }
            }
        }
        else
        {
            if (isAdd)
            {
                pos.y += moveInterval;
                if (pos.y > initpos.y)
                {
                    pos.y = initpos.y;
                    isOver = true;
                }
            }
            else
            {
                pos.y -= moveInterval;
                if (pos.y < initpos.y)
                {
                    pos.y = initpos.y;
                    isOver = true;
                }
            }
        }
        this.rect.anchoredPosition = pos;
        if (isOver)
        {
            this.isMoveOver = true;
            mDelta = 0;
        }
    }

    void Update()
    {
        if (curtime > TimeKit.currentTimeMillis) return;
        if(isMoveOver) return;

        mDelta += Time.deltaTime;
        if (mDelta > 1/FPS)
        {
            mDelta = 0;
            move();
        }
    }
}

