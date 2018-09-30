using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NEO_test_tools.lib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NEO_test_tools
{
    public partial class Form1 : Form
    {
        mongoHelper mh = new mongoHelper();
        neoHelper nh = new neoHelper();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOldUTXO.Text = string.Empty;

            JArray JAoldUTXOs = new JArray();
            if (comboBox1.SelectedIndex == 0)
            {
                JAoldUTXOs = mh.GetData(mh.mongodbConnStr_testnet, mh.mongodbDatabase_testnet, "utxo", "{addr:'" + txtAddr.Text + "',asset:'0x602c79718b16e442de58778e148d0b1084e3b2dffd5de6b7b16cee7969282de7',used:''}", "{value:-1}");
            }
            else
            {
                JAoldUTXOs = mh.GetData(mh.mongodbConnStr_mainnet, mh.mongodbDatabase_mainnet, "utxo", "{addr:'" + txtAddr.Text + "',asset:'0x602c79718b16e442de58778e148d0b1084e3b2dffd5de6b7b16cee7969282de7',used:''}", "{value:-1}");
            }
            

            //foreach (JObject j in JAoldUTXOs)
            //{
            listoldUTXOs.DataSource = JAoldUTXOs;
            listoldUTXOs.DisplayMember = "value";

            //listoldUTXOs.Items.Add((string)j["value"]);
            //}
        }

        private void listoldUTXOs_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOldUTXO.Text = listoldUTXOs.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JObject oldUtxoJ = JObject.Parse(txtOldUTXO.Text);
            string addr = (string)oldUtxoJ["addr"];
            string asset = (string)oldUtxoJ["asset"];
            //decimal value = (decimal)oldUtxoJ["value"];

            
            string TransferTxHex = nh.getTransferTxHex(new JArray(oldUtxoJ), addr, addr, asset,decimal.Parse(txtAmount.Text), int.Parse(txtSplit.Text));

            string txSign = nh.Sign(TransferTxHex, txtSign.Text);

            if (comboBox1.SelectedIndex == 0)
            {
                txtResult.Text = JsonConvert.SerializeObject(nh.sendTxPlusSign(mh.neoCliJsonRPCUrl_testnet, TransferTxHex, txSign, txtPublicKey.Text));
            }
            else
            {
                txtResult.Text = JsonConvert.SerializeObject(nh.sendTxPlusSign(mh.neoCliJsonRPCUrl_mainnet, TransferTxHex, txSign, txtPublicKey.Text));
            }
        }
    }
}
