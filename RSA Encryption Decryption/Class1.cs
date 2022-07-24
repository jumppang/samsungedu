using CommonUtils;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Tsp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSA_Encryption_Decryption
{
    public class Class1
    {
        TextBox txtPrivateKey = new TextBox();
        TextBox txtToSignStr = new TextBox();
        TextBox txtSign = new TextBox();
        TextBox txtPubKey = new TextBox();
        Label lblValidRst = new Label();

        private void btnSign_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPrivateKey.Text) || string.IsNullOrWhiteSpace(txtToSignStr.Text))
                {
                    MessageBox.Show(" Private key and string cannot be empty ");
                    return;
                }

                //SHA256withRSA
                string fnstr = txtToSignStr.Text;// String to be signed 

                //1. Convert the private key string to RSACryptoServiceProvider object 
                RSACryptoServiceProvider rsaP = TspUtil.LoadPrivateKey(txtPrivateKey.Text, "PKCS8");
                byte[] data = Encoding.UTF8.GetBytes(fnstr);// The string to be signed is converted to byte Array ,UTF8
                byte[] byteSign = rsaP.SignData(data, "SHA256");// Corresponding JAVA Of RSAwithSHA256
                string sign = Convert.ToBase64String(byteSign);// Signature byte Array to BASE64 character string 
                txtSign.Text = sign;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       // Public key verification signature ：

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPubKey.Text) || string.IsNullOrWhiteSpace(txtToSignStr.Text) || string.IsNullOrWhiteSpace(txtSign.Text))
                {
                    MessageBox.Show(" Public key , Signature string , Signature   Can't be empty ");
                    return;
                }

                byte[] signature = Convert.FromBase64String(txtSign.Text);// The signature value is changed to byte Array 

                //SHA256withRSA
                string fnstr = txtToSignStr.Text;

                //1. Convert the private key string to RSACryptoServiceProvider object 
                RSACryptoServiceProvider rsaP = RsaUtil.LoadPublicKey(txtPubKey.Text);
                byte[] data = Encoding.UTF8.GetBytes(fnstr);// The string to be signed is converted to byte Array ,UTF8
                bool validSign = rsaP.VerifyData(data, "SHA256", signature);// Corresponding JAVA Of RSAwithSHA256

                if (validSign)
                    lblValidRst.Text = " Verify signature passed ：" + DateTime.Now.ToString();
                else
                    lblValidRst.Text = " Verify the signature   Not through ：" + DateTime.Now.ToString();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
