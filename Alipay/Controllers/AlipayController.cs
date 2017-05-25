using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Com.Alipay;

namespace Alipay.Controllers
{
    public class AlipayController : Controller
    {
        // GET: Alipay
        public ActionResult Index()
        {
            return View();
        }
        //app支付，后台下单
        public JsonResult AliPay_APPPay(string total_fee = "1")
        {
            try
            {
                //商户订单号 System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString()

                //商户网站订单系统中唯一订单号，必填

                string out_trade_no = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();
                //把请求参数打包成数组
                SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();


                sParaTemp.Add("_input_charset", Com.Alipay.Config.input_charset.ToLower());
                sParaTemp.Add("body", "1");
                sParaTemp.Add("notify_url", Com.Alipay.Config.notify_url);
                sParaTemp.Add("out_trade_no", out_trade_no);
                sParaTemp.Add("partner", Com.Alipay.Config.partner);
                sParaTemp.Add("payment_type", Com.Alipay.Config.payment_type);
                sParaTemp.Add("seller_id", Com.Alipay.Config.seller_id);
                sParaTemp.Add("service", "mobile.securitypay.pay");
                sParaTemp.Add("subject", Com.Alipay.Config.subject);
                sParaTemp.Add("total_fee", total_fee.ToString());
                sParaTemp.Add("it_b_pay", "30m");
                sParaTemp.Add("return_url", "m.alipay.com");
                Encoding code = Encoding.GetEncoding(Com.Alipay.Config.input_charset);

                //待请求参数数组字符串
                string strRequestData = Submit.BuildRequestParaToString(sParaTemp, code);
                strRequestData = HttpUtility.UrlDecode(strRequestData, code);

                return Json(new 
                {
                    Data = strRequestData,
                    ResultCode = 0,
                    Msg = "下单成功"
                });
            }
            catch (Exception ex)
            {

                return Json(new 
                {
                    Data = "",
                    ResultCode = 1,
                    Msg = "下单异常：" + ex.Message
                });
            }

        }
    }
}