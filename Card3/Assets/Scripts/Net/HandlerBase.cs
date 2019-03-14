using System;
using System.Collections.Generic;

/// <summary>
/// 客户端处理基类
/// </summary>
public abstract class HandlerBase
{
    public abstract void OnReceive(int subCode, object value);

    /// <summary>
    /// 为了方便发消息
    /// </summary>
    //给  《哪个模块》  发送  《什么类型》  的   《什么内容》   的消息
    protected void Dispatch(int areaCode, int eventCode, object message)
    {
        MsgCenter.Instance.Dispatch(areaCode, eventCode, message);
    }
}
