using SevenZip;
using System;

public class CodeProgress:ICodeProgress
{
	public delegate void ProgressDelegate(Int64 fileSize,Int64 processSize);
	
	public ProgressDelegate progressDelegate=null;
	
	public CodeProgress(ProgressDelegate del)
	{
		progressDelegate=del;
	}
	
	public void SetProgress(Int64 inSize, Int64 outSize)
	{
		
	}
	
	public void SetProgressPercent(Int64 fileSize,Int64 processSize)
	{
		progressDelegate(fileSize,processSize);
	}
}