using Dynamsoft;
using Dynamsoft.DBR;

namespace BarcodeQRCode.Services
{
    public partial class BarcodeQRCodeService
    {
        BarcodeReader? reader = null;
        
        public partial void InitSDK(string license)
        {

            string errorMsg;
            EnumErrorCode errorCode = BarcodeReader.InitLicense("DLS2eyJoYW5kc2hha2VDb2RlIjoiMjAwMDAxLTE2NDk4Mjk3OTI2MzUiLCJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSIsInNlc3Npb25QYXNzd29yZCI6IndTcGR6Vm05WDJrcEQ5YUoifQ==", out errorMsg); // Get a license key from https://www.dynamsoft.com/customer/license/trialLicense?product=dbr

            if (errorCode != EnumErrorCode.DBR_SUCCESS)
            {
                Console.WriteLine(errorMsg);
            }

            try
            {
                reader = new BarcodeReader();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public partial string DecodeFile(string filePath)
        {
            if (reader == null)
                return "";

            string decodingResult = "";            
            try
            {

                TextResult[]? results = reader.DecodeFile(filePath, "");
                if (results != null && results.Length > 0)
                {
                    int i = 1;
                    foreach (TextResult result in results)
                    {
                        string barcodeFormat = result.BarcodeFormatString;
                        string message = "Barcode " + i + ": " + barcodeFormat + ", " + result.BarcodeText;
                        Console.WriteLine(message);
                        decodingResult += message;
                        i++;
                    }
                }
                else
                {
                    decodingResult = "No barcode found.";
                }
            }
            catch (Exception e)
            {
                decodingResult = e.Message;
            }

            return decodingResult;
        }
    }
}