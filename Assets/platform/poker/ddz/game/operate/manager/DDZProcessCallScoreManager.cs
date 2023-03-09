namespace platform.poker
{
    /// <summary> 叫分按钮管理 </summary>
    public class DDZProcessCallScoreManager : UnrealLuaSpaceObject
    {
        int nowScore;

        bool isPass;

        public const int MaxScore = 3;

        DDZCallScoreProcess getProcess(int path)
        {
            return transform.Find(path.ToString()).GetComponent<DDZCallScoreProcess>();
        }

        protected override void xrefresh()
        {
            for (int i = 1; i <= MaxScore; i++)
            {
                getProcess(i).enabled = (nowScore < i);
                getProcess(i).transform.Find("mask").gameObject.SetActive((nowScore >= i));
            }
            transform.Find("pass").GetComponent<PKOperateEvent>().enabled = isPass;
            transform.Find("pass").Find("mask").gameObject.SetActive(!isPass);
        }

        public void setData(int nowScore, bool isPass)
        {
            this.nowScore = nowScore;
            this.isPass = isPass;
            xrefresh();
        }
    }
}
