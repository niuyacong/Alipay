using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Com.Alipay;

/// <summary>
/// 功能：即时到账交易接口接入页
/// 版本：3.3
/// 日期：2012-07-05
/// 说明：
/// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
/// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
/// 
/// /////////////////注意///////////////////////////////////////////////////////////////
/// 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
/// 1、商户服务中心（https://b.alipay.com/support/helperApply.htm?action=consultationApply），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决
/// 2、商户帮助中心（http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9）
/// 3、支付宝论坛（http://club.alipay.com/read-htm-tid-8681712.html）
/// 
/// 如果不想使用扩展功能请把扩展功能参数赋空值。
/// </summary>
public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //支付类型
        string payment_type = "1";
        //必填，不能修改
        //服务器异步通知页面路径
        string notify_url = ConfigurationManager.AppSettings["WebPath"] + "/payDeal/alipayNotify.aspx";
        //需http://格式的完整路径，不能加?id=123这类自定义参数
        //页面跳转同步通知页面路径
        string return_url = ConfigurationManager.AppSettings["WebPath"] + "/payDeal/return_url.aspx";
        //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/
        //商户订单号 System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString()
        string out_trade_no = Request["mainId"];
        //商户网站订单系统中唯一订单号，必填
        string payType = Request["payType"]; //1 余额充值 2 储值卡购买 3 订单结算
        string UserId = Request["UserId"]; //用户编号
        string CzMoney = Request["CzMoney"]; //用户编号
        if (payType == "1" || payType == "2")
        {
            out_trade_no = payType + "," +UserId+","+CzMoney+","+ System.DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        else 
        {
            out_trade_no = payType + "," + UserId + "," + CzMoney + "," + out_trade_no;
        }


        //订单名称
        string subject = "即时到帐支付";
        //必填
        //付款金额
        string total_fee = Request["money"]; //"0.01"
        //必填
        //订单描述
        string body = "订单描述";
        //商品展示地址
        string show_url = ConfigurationManager.AppSettings["WebPath"] + "/area/homepage/index";
        //需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html
        //防钓鱼时间戳
        string anti_phishing_key = "";
        //若要使用请调用类文件submit中的query_timestamp函数
        //客户端的IP地址
        string exter_invoke_ip = "";
        //非局域网的外网IP地址，如：221.0.0.1


        ////////////////////////////////////////////////////////////////////////////////////////////////

        //把请求参数打包成数组
        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("partner", Com.Alipay.Config.partner);
        sParaTemp.Add("seller_email", Com.Alipay.Config.seller_id);
        sParaTemp.Add("_input_charset", Com.Alipay.Config.input_charset.ToLower());
        sParaTemp.Add("service", "create_direct_pay_by_user");
        sParaTemp.Add("payment_type", payment_type);
        sParaTemp.Add("notify_url", notify_url);
        sParaTemp.Add("return_url", return_url);
        sParaTemp.Add("out_trade_no", out_trade_no);
        sParaTemp.Add("subject", subject);
        sParaTemp.Add("total_fee", total_fee);
        sParaTemp.Add("body", body);
        sParaTemp.Add("show_url", show_url);
        sParaTemp.Add("anti_phishing_key", anti_phishing_key);
        sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

        //建立请求
        string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
        Response.Write(sHtmlText);
    }

    protected void BtnAlipay_Click(object sender, EventArgs e)
    {
        ////////////////////////////////////////////请求参数////////////////////////////////////////////

        //支付类型
        string payment_type = "1";
        //必填，不能修改
        //服务器异步通知页面路径
        string notify_url = "http://222.223.251.109:8060/payDeal/notify_url.aspx";
        //需http://格式的完整路径，不能加?id=123这类自定义参数
        //页面跳转同步通知页面路径
        string return_url = "http://222.223.251.109:8060/payDeal/return_url.aspx";
        //需http://格式的完整路径，不能加?id=123这类自定义参数，不能写成http://localhost/
        //商户订单号 System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString()
        string out_trade_no =Request["mainId"];
        //商户网站订单系统中唯一订单号，必填
        //订单名称
        string subject = "即时到帐支付";
        //必填
        //付款金额
        string total_fee = Request["money"];
        //必填
        //订单描述
        string body = "订单描述";
        //商品展示地址
        string show_url = "http://192.168.29.71:8060/area/homepage/index";
        //需以http://开头的完整路径，例如：http://www.商户网址.com/myorder.html
        //防钓鱼时间戳
        string anti_phishing_key = "";
        //若要使用请调用类文件submit中的query_timestamp函数
        //客户端的IP地址
        string exter_invoke_ip = "";
        //非局域网的外网IP地址，如：221.0.0.1


        ////////////////////////////////////////////////////////////////////////////////////////////////

        //把请求参数打包成数组
        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("partner", Com.Alipay.Config.partner);
        sParaTemp.Add("seller_email", Com.Alipay.Config.seller_id);
        sParaTemp.Add("_input_charset", Com.Alipay.Config.input_charset.ToLower());
        sParaTemp.Add("service", "create_direct_pay_by_user");
        sParaTemp.Add("payment_type", payment_type);
        sParaTemp.Add("notify_url", notify_url);
        sParaTemp.Add("return_url", return_url);
        sParaTemp.Add("out_trade_no", out_trade_no);
        sParaTemp.Add("subject", subject);
        sParaTemp.Add("total_fee", total_fee);
        sParaTemp.Add("body", body);
        sParaTemp.Add("show_url", show_url);
        sParaTemp.Add("anti_phishing_key", anti_phishing_key);
        sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);

        //建立请求
        string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
        Response.Write(sHtmlText);
        
    }
}
