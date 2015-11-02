using System;
using System.Collections.Generic;
using System.Xml;
using HmrcTpvsProxy.Domain.Models;

namespace HmrcTpvsProxy.Domain.TestDataTransformer
{
    public class StudentLoanNoticeTransformer : IStudentLoanNoticeTransformer
    {
        private const string SL1TagName = "StudentLoanStart";
        private const string SL2TagName = "StudentLoanEnd";

        public IEnumerable<StudentLoanNotice> Transform(XmlDocument studentLoanMessage)
        {
            if (studentLoanMessage == null)
                throw new ArgumentNullException(nameof(studentLoanMessage));

            var translatedNotices = new List<StudentLoanNotice>();
            var xmlNotices = studentLoanMessage.GetElementsByTagName(SL1TagName);

            var sl1Notices = Transform(studentLoanMessage.GetElementsByTagName(SL1TagName));
            var sl2Notices = Transform(studentLoanMessage.GetElementsByTagName(SL2TagName));

            translatedNotices.AddRange(sl1Notices);
            translatedNotices.AddRange(sl2Notices);

            return translatedNotices;
        }

        private IEnumerable<StudentLoanNotice> Transform(XmlNodeList xmlNotices)
        {
            var translatedNotices = new List<StudentLoanNotice>();

            foreach (XmlNode xmlNotice in xmlNotices)
            {
                var translatedNotice = new StudentLoanNotice();

                if (xmlNotice.Name == SL1TagName)
                    translatedNotice.MessageType = "SL1";
                else
                    translatedNotice.MessageType = "SL2";

                foreach (XmlAttribute attribute in xmlNotice.Attributes)
                {
                    if (attribute.Name == "SequenceNumber")
                        translatedNotice.SequenceNumber = int.Parse(attribute.Value);
                    if (attribute.Name == "IssueDate")
                        translatedNotice.IssueDate = Convert.ToDateTime(attribute.Value);
                }

                foreach (XmlNode noticeNode in xmlNotice.ChildNodes)
                {
                    if (noticeNode.Name == "NINO")
                        translatedNotice.NationalInsuranceNo = noticeNode.InnerText;
                    else if (noticeNode.Name == "WorksNumber")
                        translatedNotice.WorksNumber = noticeNode.InnerText;
                    else if ((noticeNode.Name == "LoanStartDate") || (noticeNode.Name == "StopDate"))
                        translatedNotice.EffectiveDate = Convert.ToDateTime(noticeNode.InnerText);
                    else if (noticeNode.Name == "PlanType")
                        translatedNotice.PlanType = noticeNode.InnerText;
                    else if (noticeNode.Name == "Name")
                    {
                        var forename = string.Empty;
                        var surname = string.Empty;
                        foreach(XmlNode nameNode in noticeNode.ChildNodes)
                        {
                            if (nameNode.Name == "Forename")
                                forename = nameNode.InnerText;
                            else if (nameNode.Name == "Surname")
                                surname = nameNode.InnerText;
                        }

                        translatedNotice.Name = string.Format("{0} {1}", forename, surname);
                    }                        
                }

                translatedNotices.Add(translatedNotice);
            }

            return translatedNotices;
        }
    }
}