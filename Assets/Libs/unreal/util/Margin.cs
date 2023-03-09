
using XLua;
/// <summary>
/// 留边,用于UI布局
/// </summary>
[Hotfix]
public class Margin
{
    public const int LEFT = 1;
    public const int TOP = 2;
    public const int RIGHT = 4;
    public const int BOTTOM = 5;

    public const bool HORIZONTAL = true;
    public const bool VERTICAL = false;

    public float left;
    public float top;
    public float right;
    public float bottom;

    public Margin()
    {
    }
    public Margin(int direction, float value)
    {
        if (direction == LEFT) this.left = value;
        else if (direction == TOP) this.top = value;
        else if (direction == RIGHT) this.right = value;
        else if (direction == BOTTOM) this.bottom = value;
    }
    public Margin(bool layout, float v1, float v2)
    {
        if (layout == HORIZONTAL)
        {
            this.left = v1;
            this.right = v2;
        }
        else
        {
            this.top = v1;
            this.bottom = v2;
        }
    }
    public Margin(float left, float top, float right, float bottom)
    {
        this.left = left;
        this.top = top;
        this.right = right;
        this.bottom = bottom;
    }
}