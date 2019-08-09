using System;
using System.Drawing.Imaging;
using System.IO;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace TzuChiClassLibrary.Utils
{
    public class QrCodeHelper
    {
        private static bool GetQRCode(string strContent, MemoryStream ms)
        {
            ErrorCorrectionLevel Ecl = ErrorCorrectionLevel.M; //誤差校正水平 (模糊或清晰)
            string Content = strContent;  //待編碼內容 (網址) 
            QuietZoneModules QuietZones = QuietZoneModules.Two;  //空白區域
            int ModuleSize = 12;    //大小  
            var encoder = new QrEncoder(Ecl);
            QrCode qr;
            if (encoder.TryEncode(Content, out qr))//對內容進行編碼，並保存生成的矩陣  
            {
                var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones));
                render.WriteToStream(qr.Matrix, ImageFormat.Png, ms);
            }
            else
            {
                return false;
            }
            return true;
        }
        public static string GetQRCodeBase64String(string strContent)
        {
            try
            {
                string result = "data:image/png;base64,";
                using (var ms = new MemoryStream())
                {
                    GetQRCode(strContent, ms);
                    result += Convert.ToBase64String(ms.GetBuffer());
                    return result;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
