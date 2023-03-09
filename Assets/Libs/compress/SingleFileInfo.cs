/// <summary>
/// 单个文件信息
/// </summary>
public class SingleFileInfo
{
    /// <summary>
    /// 文件数量
    /// </summary>
	public int file_id=0;

	public int file_StartPos=0;
    /// <summary>
    /// 文件大小
    /// </summary>
	public int file_size=0;
    /// <summary>
    /// 文件路径长度
    /// </summary>
	public int file_PathLength = 0;
    /// <summary>
    /// 文件路径
    /// </summary>
	public string file_Path="";
    /// <summary>
    /// 文件二进制数据
    /// </summary>
	public byte[] file_data = null;
};