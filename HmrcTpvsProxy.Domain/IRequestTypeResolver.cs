﻿using System.Xml;

namespace HmrcTpvsProxy.Domain
{
    public interface IRequestTypeResolver
    {
        RequestType GetRequestType(XmlDocument requestXml);
    }
}