using Dynamsoft;

namespace BarcodeQRCode.Services
{
    public partial class BarcodeQRCodeService
    {
        BarcodeQRCodeReader? reader = null;
        
        public partial void InitSDK(string license)
        {
            BarcodeQRCodeReader.InitLicense(license); // Get a license key from https://www.dynamsoft.com/customer/license/trialLicense?product=dbr

            try
            {
                Console.WriteLine("GetVersionInfo(): " + BarcodeQRCodeReader.GetVersionInfo());
                reader = BarcodeQRCodeReader.Create();
                // Refer to https://www.dynamsoft.com/barcode-reader/parameters/structure-and-interfaces-of-parameters.html?ver=latest
                reader.SetParameters("{\"Version\":\"3.0\", \"ImageParameter\":{\"Name\":\"IP1\", \"BarcodeFormatIds\":[\"BF_QR_CODE\", \"BF_ONED\"], \"ExpectedBarcodesCount\":20}}");
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
                
                string[]? results = reader.DecodeFile(filePath);
                if (results != null)
                {
                    foreach (string result in results)
                    {
                        decodingResult += result + "\n";
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