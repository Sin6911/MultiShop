using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

/// <summary>
/// Thư viện tiện ích gửi email thông qua tài khoản gmail
/// </summary>
public class XMail
{
    /// <summary>
    /// Gửi email từ hệ thống thông qua tài khoản gmail
    /// </summary>
    /// <param name="to">Email người nhận</param>
    /// <param name="subject">Tiêu đề mail</param>
    /// <param name="body">Nội dung mail</param>
    public static void Send(String to, String subject, String body)
    {
        var from = "Web Master <kentphp1@gmail.com>";
        Send(from, to, subject, body);
    }

    /// <summary>
    /// Gửi email đơn giản thông qua tài khoản gmail
    /// </summary>
    /// <param name="from">Email người gửi</param>
    /// <param name="to">Email người nhận</param>
    /// <param name="subject">Tiêu đề mail</param>
    /// <param name="body">Nội dung mail</param>
    public static void Send(String from, String to, String subject, String body)
    {
        String cc = "";
        String bcc = "";
        String attachments = "";
        Send(from, to, cc, bcc, subject, body, attachments);
    }

    /// <summary>
    /// Gửi email thông qua tài khoản gmail
    /// </summary>
    /// <param name="from">Email người gửi</param>
    /// <param name="to">Email người nhận</param>
    /// <param name="cc">Danh sách email những người cùng nhận phân cách bởi dấu phẩy</param>
    /// <param name="bcc">Danh sách email những người cùng nhận phân cách bởi dấu phẩy</param>
    /// <param name="subject">Tiêu đề mail</param>
    /// <param name="body">Nội dung mail</param>
    /// <param name="attachments">Danh sách file định kèm phân cách bởi phẩy hoặc chấm phẩy</param>
    public static void Send(String from, String to, String cc, String bcc, String subject, String body, String attachments)
    {
        var message = new MailMessage();
        message.IsBodyHtml = true;
        message.From = new MailAddress(from);
        message.To.Add(new MailAddress(to));
        message.Subject = subject;
        message.Body = body;
        message.ReplyToList.Add(from);
        if (cc.Length > 0)
        {
            message.CC.Add(cc);
        }
        if (bcc.Length > 0)
        {
            message.Bcc.Add(bcc);
        }
        if (attachments.Length > 0)
        {
            String[] fileNames = attachments.Split(';', ',');
            foreach (var fileName in fileNames)
            {
                message.Attachments.Add(new Attachment(fileName));
            }
        }

        // Kết nối GMail
        var client = new SmtpClient("smtp.gmail.com", 25)
        {
            Credentials = new NetworkCredential("tranngochoang2012@gmail.com", "songlong"),
            EnableSsl = true
        };
        // Gởi mail
        client.Send(message);
    }
}